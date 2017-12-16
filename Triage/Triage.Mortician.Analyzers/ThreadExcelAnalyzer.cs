﻿using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using DocumentFormat.OpenXml.ExtendedProperties;
using SpreadsheetLight;

namespace Triage.Mortician.Analyzers
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents an object that is capable of reporting on the threads from the memory dump
    /// </summary>
    /// <seealso cref="T:Triage.Mortician.Analyzers.IExcelAnalyzer" />
    [Export(typeof(IExcelAnalyzer))]
    public class ThreadExcelAnalyzer : IExcelAnalyzer
    {
        /// <summary>
        ///     The log
        /// </summary>
        protected ILog Log = LogManager.GetLogger(typeof(ThreadExcelAnalyzer));

        /// <summary>
        ///     Gets or sets the dump thread repository.
        /// </summary>
        /// <value>
        ///     The dump thread repository.
        /// </value>
        [Import]
        public DumpThreadRepository DumpThreadRepository { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Performs any required setup like number crunching etc.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///     A Task, that when complete will signal the setup completion
        /// </returns>
        public Task Setup(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Contributes the specified shared document.
        /// </summary>
        /// <param name="sharedDocument">The shared document.</param>
        public void Contribute(SLDocument sharedDocument)
        {
            PopulateUniqueStacks(sharedDocument);
            PopulateManagedStackFrames(sharedDocument);
        }

        private void PopulateManagedStackFrames(SLDocument sharedDocument)
        {
            sharedDocument.SelectWorksheet("Stack Frames");
            int row = 2;
            foreach (var grouping in DumpThreadRepository.Get().Where(t => t.ManagedStackFrames != null)
                .SelectMany(t => t.ManagedStackFrames)
                .GroupBy(f => f.DisplayString))
            {
                sharedDocument.SetCellValue(row, 1, grouping.Key);
                sharedDocument.SetCellValue(row, 2, grouping.First().ModuleName);
                sharedDocument.SetCellValue(row, 3, grouping.Count());
                row++;
            }
        }

        private void PopulateUniqueStacks(SLDocument sharedDocument)
        {
            sharedDocument.SelectWorksheet("Unique Stacks");
            var groups = DumpThreadRepository.Get()
                .GroupBy(t => string.Join("\n", t.EEStackFrames))
                .Where(g => !string.IsNullOrWhiteSpace(g.Key))
                .OrderByDescending(g => g.Count())
                .ThenByDescending(g => g.Key.Length);

            var curStackRow = 1;
            var minNumLinesPerStack = 2;
            var threadsColumn = 12;
            foreach (var group in groups)
            {
                var max = minNumLinesPerStack;
                sharedDocument.SetCellValue(curStackRow, 1, "Stack:");
                sharedDocument.SetCellValue(curStackRow, threadsColumn, "Threads:");

                var stackIndex = 0;
                foreach (var line in group.First().EEStackFrames)
                {
                    sharedDocument.SetCellValue(curStackRow + 1 + stackIndex, 1, line);
                    stackIndex++;
                }

                if (stackIndex - 1 > max)
                    max = stackIndex - 1;

                var threadIndex = 0;
                foreach (var thread in @group.OrderByDescending(t => t.KernelModeTime + t.UserModeTime))
                {
                    sharedDocument.SetCellValue(curStackRow + 1 + threadIndex, threadsColumn,
                        $"{thread.DebuggerIndex}:{thread.OsId:x}");
                    threadIndex++;
                }

                if (threadIndex - 1 > max)
                    max = threadIndex - 1;

                curStackRow += max + 5;
            }
        }
    }
}
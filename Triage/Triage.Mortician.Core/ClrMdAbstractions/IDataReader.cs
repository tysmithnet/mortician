﻿// ***********************************************************************
// Assembly         : Triage.Mortician.Core
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IDataReader.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;

namespace Triage.Mortician.Core.ClrMdAbstractions
{
    /// <summary>
    ///     An interface for reading data out of the target process.
    /// </summary>
    public interface IDataReader
    {
        /// <summary>
        ///     Called when the DataTarget is closing (Disposing).  Used to clean up resources.
        /// </summary>
        void Close();

        /// <summary>
        ///     Enumerates the OS thread ID of all threads in the process.
        /// </summary>
        /// <returns>An enumeration of all threads in the target process.</returns>
        IEnumerable<uint> EnumerateAllThreads();

        /// <summary>
        ///     Enumerates modules in the target process.
        /// </summary>
        /// <returns>A list of the modules in the target process.</returns>
        IList<IModuleInfo> EnumerateModules();

        /// <summary>
        ///     Informs the data reader that the user has requested all data be flushed.
        /// </summary>
        void Flush();

        /// <summary>
        ///     Gets the architecture of the target.
        /// </summary>
        /// <returns>The architecture of the target.</returns>
        Architecture GetArchitecture();

        /// <summary>
        ///     Gets the size of a pointer in the target process.
        /// </summary>
        /// <returns>The pointer size of the target process.</returns>
        uint GetPointerSize();

        /// <summary>
        ///     Gets the thread context for the given thread.
        /// </summary>
        /// <param name="threadID">The OS thread ID to read the context from.</param>
        /// <param name="contextFlags">The requested context flags, or 0 for default flags.</param>
        /// <param name="contextSize">The size (in bytes) of the context parameter.</param>
        /// <param name="context">A pointer to the buffer to write to.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool GetThreadContext(uint threadID, uint contextFlags, uint contextSize, IntPtr context);

        /// <summary>
        ///     Gets the thread context for the given thread.
        /// </summary>
        /// <param name="threadID">The OS thread ID to read the context from.</param>
        /// <param name="contextFlags">The requested context flags, or 0 for default flags.</param>
        /// <param name="contextSize">The size (in bytes) of the context parameter.</param>
        /// <param name="context">A pointer to the buffer to write to.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool GetThreadContext(uint threadID, uint contextFlags, uint contextSize, byte[] context);

        /// <summary>
        ///     Gets the TEB of the specified thread.
        /// </summary>
        /// <param name="thread">The OS thread ID to get the TEB for.</param>
        /// <returns>The address of the thread's teb.</returns>
        ulong GetThreadTeb(uint thread);

        /// <summary>
        ///     Gets the version information for a given module (given by the base address of the module).
        /// </summary>
        /// <param name="baseAddress">The base address of the module to look up.</param>
        /// <param name="version">The version info for the given module.</param>
        void GetVersionInfo(ulong baseAddress, out VersionInfo version);

        /// <summary>
        ///     Read an int out of the target process.
        /// </summary>
        /// <param name="addr">The addr.</param>
        /// <returns>
        ///     The int at the give address, or 0 if that pointer doesn't exist in
        ///     the data target.
        /// </returns>
        uint ReadDwordUnsafe(ulong addr);

        /// <summary>
        ///     Read memory out of the target process.
        /// </summary>
        /// <param name="address">The address of memory to read.</param>
        /// <param name="buffer">The buffer to write to.</param>
        /// <param name="bytesRequested">The number of bytes to read.</param>
        /// <param name="bytesRead">The number of bytes actually read out of the target process.</param>
        /// <returns>True if any bytes were read at all, false if the read failed (and no bytes were read).</returns>
        bool ReadMemory(ulong address, byte[] buffer, int bytesRequested, out int bytesRead);

        /// <summary>
        ///     Read memory out of the target process.
        /// </summary>
        /// <param name="address">The address of memory to read.</param>
        /// <param name="buffer">The buffer to write to.</param>
        /// <param name="bytesRequested">The number of bytes to read.</param>
        /// <param name="bytesRead">The number of bytes actually read out of the target process.</param>
        /// <returns>True if any bytes were read at all, false if the read failed (and no bytes were read).</returns>
        bool ReadMemory(ulong address, IntPtr buffer, int bytesRequested, out int bytesRead);

        /// <summary>
        ///     Read a pointer out of the target process.
        /// </summary>
        /// <param name="addr">The addr.</param>
        /// <returns>
        ///     The pointer at the give address, or 0 if that pointer doesn't exist in
        ///     the data target.
        /// </returns>
        ulong ReadPointerUnsafe(ulong addr);

        /// <summary>
        ///     Gets information about the given memory range.
        /// </summary>
        /// <param name="addr">An arbitrary address in the target process.</param>
        /// <param name="virtualQuery">The base address and size of the allocation.</param>
        /// <returns>True if the address was found and vq was filled, false if the address is not valid memory.</returns>
        bool VirtualQuery(ulong addr, out VirtualQueryData virtualQuery);

        /// <summary>
        ///     Returns true if the data target is a minidump (or otherwise may not contain full heap data).
        /// </summary>
        /// <value><c>true</c> if this instance is minidump; otherwise, <c>false</c>.</value>
        bool IsMinidump { get; }
    }
}
﻿// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-12-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 12-19-2017
// ***********************************************************************
// <copyright file="StringExtractor.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Core;
using Triage.Mortician.Core.ClrMdAbstractions;
using Triage.Mortician.Domain;

namespace Triage.Mortician
{
    /// <summary>
    ///     DumpObjectExtractor capable of parsing System.String objects
    /// </summary>
    /// <seealso cref="IDumpObjectExtractor" />
    /// <inheritdoc />
    /// <seealso cref="T:Triage.Mortician.IDumpObjectExtractor" />
    [Export(typeof(IDumpObjectExtractor))]
    public class StringExtractor : IDumpObjectExtractor
    {
        /// <summary>
        ///     Determines whether this instance can extract from the provided object
        /// </summary>
        /// <param name="clrObject">The object to try to get values from</param>
        /// <param name="clrRuntime">The clr runtime being used</param>
        /// <returns><c>true</c> if this instance can extract from the object; otherwise, <c>false</c>.</returns>
        /// <inheritdoc />
        public bool CanExtract(IClrObject clrObject, IClrRuntime clrRuntime)
        {
            return clrObject.Type?.Name == "System.String";
        }

        /// <summary>
        ///     Extracts data from the provided object
        /// </summary>
        /// <param name="clrObject">The object.</param>
        /// <param name="clrRuntime">The runtime.</param>
        /// <returns>Extracted dump object</returns>
        /// <inheritdoc />
        public DumpObject Extract(IClrObject clrObject, IClrRuntime clrRuntime)
        {
            var value = (string) clrObject.Type.GetValue(clrObject.Address);
            var preview = new string(value.Take(100).ToArray());
            var heapObject = new StringDumpObject(clrObject.Address, "System.String", clrObject.Size, preview,
                clrRuntime.Heap.GetGeneration(clrObject.Address));

            return heapObject;
        }
    }
}
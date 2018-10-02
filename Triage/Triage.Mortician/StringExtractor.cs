﻿// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 12-12-2017
//
// Last Modified By : @tysmithnet
// Last Modified On : 10-01-2018
// ***********************************************************************
// <copyright file="StringExtractor.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel.Composition;
using Triage.Mortician.Core;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician
{
    /// <summary>
    ///     DumpObjectExtractor capable of parsing System.String objects
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.IDumpObjectExtractor" />
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
        public bool CanExtract(IClrObject clrObject, IClrRuntime clrRuntime) => clrObject.Type?.Name == "System.String";

        /// <summary>
        ///     Determines whether this instance can extract the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="clrRuntime">The color runtime.</param>
        /// <returns><c>true</c> if this instance can extract the specified address; otherwise, <c>false</c>.</returns>
        /// <inheritdoc />
        public bool CanExtract(ulong address, IClrRuntime clrRuntime)
        {
            var type = clrRuntime.Heap.GetObjectType(address);
            if (type == null) return false;
            return type.Name == "System.String";
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
            var heapObject = new StringDumpObject(clrObject.Address, "System.String", clrObject.Size, value,
                clrRuntime.Heap.GetGeneration(clrObject.Address));

            return heapObject;
        }

        /// <summary>
        ///     Extracts the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="clrRuntime">The color runtime.</param>
        /// <returns>DumpObject.</returns>
        /// <inheritdoc />
        public DumpObject Extract(ulong address, IClrRuntime clrRuntime)
        {
            var type = clrRuntime.Heap.GetObjectType(address);
            var val = (string) type.GetValue(address);
            var gen = clrRuntime.Heap.GetGeneration(address);
            return new StringDumpObject(address, "System.String", type.GetSize(address), val, gen);
        }
    }
}
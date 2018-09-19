﻿// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="ManagedWorkItemAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.ComponentModel.Composition;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    /// <summary>
    ///     Class ManagedWorkItemAdapter.
    /// </summary>
    /// <seealso cref="Triage.Mortician.Core.ClrMdAbstractions.IManagedWorkItem" />
    internal class ManagedWorkItemAdapter : IManagedWorkItem
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ManagedWorkItemAdapter" /> class.
        /// </summary>
        /// <param name="workItem">The work item.</param>
        /// <exception cref="ArgumentNullException">workItem</exception>
        /// <inheritdoc />
        public ManagedWorkItemAdapter(ManagedWorkItem workItem)
        {
            WorkItem = workItem ?? throw new ArgumentNullException(nameof(workItem));
            Type = Converter.Convert(workItem.Type);
        }

        /// <summary>
        ///     The work item
        /// </summary>
        internal ManagedWorkItem WorkItem;

        /// <summary>
        ///     The object address of this entry.
        /// </summary>
        /// <value>The object.</value>
        /// <inheritdoc />
        public ulong Object => WorkItem.Object;

        /// <summary>
        ///     The type of Object.
        /// </summary>
        /// <value>The type.</value>
        /// <inheritdoc />
        public IClrType Type { get; }

        /// <summary>
        ///     Gets or sets the converter.
        /// </summary>
        /// <value>The converter.</value>
        [Import]
        internal IConverter Converter { get; set; }
    }
}
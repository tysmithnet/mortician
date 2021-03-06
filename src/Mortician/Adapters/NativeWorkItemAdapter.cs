﻿// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-20-2018
// ***********************************************************************
// <copyright file="NativeWorkItemAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Microsoft.Diagnostics.Runtime;
using Mortician.Core.ClrMdAbstractions;
using WorkItemKind = Mortician.Core.ClrMdAbstractions.WorkItemKind;

namespace Mortician.Adapters
{
    /// <summary>
    ///     Class NativeWorkItemAdapter.
    /// </summary>
    /// <seealso cref="Mortician.Adapters.BaseAdapter" />
    /// <seealso cref="Mortician.Core.ClrMdAbstractions.INativeWorkItem" />
    internal class NativeWorkItemAdapter : BaseAdapter, INativeWorkItem
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NativeWorkItemAdapter" /> class.
        /// </summary>
        /// <param name="converter">The converter.</param>
        /// <param name="nativeWorkItem">The native work item.</param>
        /// <exception cref="ArgumentNullException">nativeWorkItem</exception>
        /// <inheritdoc />
        public NativeWorkItemAdapter(IConverter converter, NativeWorkItem nativeWorkItem) : base(converter)
        {
            NativeWorkItem = nativeWorkItem ?? throw new ArgumentNullException(nameof(nativeWorkItem));
            Callback = NativeWorkItem.Callback;
            Data = NativeWorkItem.Data;
        }

        /// <summary>
        ///     The native work item
        /// </summary>
        internal NativeWorkItem NativeWorkItem;

        /// <summary>
        ///     Setups this instance.
        /// </summary>
        public override void Setup()
        {
            Kind = Converter.Convert(NativeWorkItem.Kind);
        }

        /// <summary>
        ///     Returns the callback's address.
        /// </summary>
        /// <value>The callback.</value>
        /// <inheritdoc />
        public ulong Callback { get; internal set; }

        /// <summary>
        ///     Returns the pointer to the user's data.
        /// </summary>
        /// <value>The data.</value>
        /// <inheritdoc />
        public ulong Data { get; internal set; }

        /// <summary>
        ///     The type of work item this is.
        /// </summary>
        /// <value>The kind.</value>
        /// <inheritdoc />
        public WorkItemKind Kind { get; internal set; }
    }
}
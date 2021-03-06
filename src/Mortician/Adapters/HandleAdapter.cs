﻿// ***********************************************************************
// Assembly         : Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-19-2018
// ***********************************************************************
// <copyright file="HandleAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Microsoft.Diagnostics.Runtime;
using Mortician.Core.ClrMdAbstractions;
using HandleType = Mortician.Core.ClrMdAbstractions.HandleType;

namespace Mortician.Adapters
{
    /// <summary>
    ///     Class HandleAdapter.
    /// </summary>
    /// <seealso cref="Mortician.Core.ClrMdAbstractions.IClrHandle" />
    internal class HandleAdapter : BaseAdapter, IClrHandle
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HandleAdapter" /> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <exception cref="ArgumentNullException">handle</exception>
        /// <inheritdoc />
        public HandleAdapter(IConverter converter, ClrHandle handle) : base(converter)
        {
            Handle = handle ?? throw new ArgumentNullException(nameof(handle));
            Address = Handle.Address;
            DependentTarget = Handle.DependentTarget;
            IsPinned = Handle.IsPinned;
            IsStrong = Handle.IsStrong;
            Object = Handle.Object;
            RefCount = Handle.RefCount;
        }

        /// <summary>
        ///     The handle
        /// </summary>
        internal ClrHandle Handle;

        public override void Setup()
        {
            AppDomain = Converter.Convert(Handle.AppDomain);
            DependentType = Converter.Convert(Handle.DependentType);
            HandleType = Converter.Convert(Handle.HandleType);
            Type = Converter.Convert(Handle.Type);
        }

        /// <summary>
        ///     The address of the handle itself.  That is, *ulong == Object.
        /// </summary>
        /// <value>The address.</value>
        /// <inheritdoc />
        public ulong Address { get; internal set; }

        /// <summary>
        ///     The AppDomain the handle resides in.
        /// </summary>
        /// <value>The application domain.</value>
        /// <inheritdoc />
        public IClrAppDomain AppDomain { get; set; }

        /// <summary>
        ///     Set only if the handle type is a DependentHandle.  Dependent handles add
        ///     an extra edge to the object graph.  Meaning, this.Object now roots the
        ///     dependent target, but only if this.Object is alive itself.
        ///     NOTE: CLRs prior to v4.5 cannot obtain the dependent target.  This field will
        ///     be 0 for any CLR prior to v4.5.
        /// </summary>
        /// <value>The dependent target.</value>
        /// <inheritdoc />
        public ulong DependentTarget { get; internal set; }

        /// <summary>
        ///     The type of the dependent target, if non 0.
        /// </summary>
        /// <value>The type of the dependent.</value>
        /// <inheritdoc />
        public IClrType DependentType { get; set; }

        /// <summary>
        ///     Gets the type of handle.
        /// </summary>
        /// <value>The type of the handle.</value>
        /// <inheritdoc />
        public HandleType HandleType { get; set; }

        /// <summary>
        ///     Whether or not the handle pins the object (doesn't allow the GC to
        ///     relocate it) or not.
        /// </summary>
        /// <value><c>true</c> if this instance is pinned; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsPinned { get; internal set; }

        /// <summary>
        ///     Whether the handle is strong (roots the object) or not.
        /// </summary>
        /// <value><c>true</c> if this instance is strong; otherwise, <c>false</c>.</value>
        /// <inheritdoc />
        public bool IsStrong { get; internal set; }

        /// <summary>
        ///     The Object the handle roots.
        /// </summary>
        /// <value>The object.</value>
        /// <inheritdoc />
        public ulong Object { get; internal set; }

        /// <summary>
        ///     If this handle is a RefCount handle, this returns the reference count.
        ///     RefCount handles with a RefCount &gt; 0 are strong.
        ///     NOTE: v2 CLR CANNOT determine the RefCount.  We always set the RefCount
        ///     to 1 in a v2 query since a strong RefCount handle is the common case.
        /// </summary>
        /// <value>The reference count.</value>
        /// <inheritdoc />
        public uint RefCount { get; internal set; }

        /// <summary>
        ///     The the type of the Object.
        /// </summary>
        /// <value>The type.</value>
        /// <inheritdoc />
        public IClrType Type { get; set; }
    }
}
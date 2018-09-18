﻿namespace Triage.Mortician.Core
{
    public interface IManagedWorkItem
    {
        /// <summary>
        /// The object address of this entry.
        /// </summary>
        ulong Object { get; }

        /// <summary>
        /// The type of Object.
        /// </summary>
        IClrType Type { get; }
    }
}
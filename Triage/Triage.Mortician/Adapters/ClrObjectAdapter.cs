﻿using System;
using System.Collections.Generic;
using System.Linq;
using Triage.Mortician.Core.ClrMdAbstractions;

namespace Triage.Mortician.Adapters
{
    internal class ClrObjectAdapter : IClrObject
    {
        /// <inheritdoc />
        public ClrObjectAdapter(Microsoft.Diagnostics.Runtime.ClrObject o)
        {
            Object = o;
            Type = Converter.Convert(o.Type);
        }

        internal Microsoft.Diagnostics.Runtime.ClrObject Object;

        /// <inheritdoc />
        public IEnumerable<IClrObject> EnumerateObjectReferences(bool carefully = false)
        {
            return Object.EnumerateObjectReferences(carefully).Select(Converter.Convert);
        }

        /// <inheritdoc />
        public bool Equals(IClrObject other)
        {
            return Object.Equals(other);
        }

        /// <inheritdoc />
        public T GetField<T>(string fieldName) where T : struct
        {
            return Object.GetField<T>(fieldName);
        }

        /// <inheritdoc />
        public IClrObject GetObjectField(string fieldName)
        {
            return Converter.Convert(Object.GetObjectField(fieldName));
        }

        /// <inheritdoc />
        public string GetStringField(string fieldName)
        {
            return Object.GetStringField(fieldName);
        }

        /// <inheritdoc />
        public IClrValueClass GetValueClassField(string fieldName)
        {
            return Converter.Convert(Object.GetValueClassField(fieldName));
        }

        /// <inheritdoc />
        public ulong Address => Object.Address;

        /// <inheritdoc />
        public bool ContainsPointers => Object.ContainsPointers;

        /// <inheritdoc />
        public string HexAddress => Object.HexAddress;

        /// <inheritdoc />
        public bool IsArray => Object.IsArray;

        /// <inheritdoc />
        public bool IsBoxed => Object.IsBoxed;

        /// <inheritdoc />
        public bool IsNull => Object.IsNull;

        /// <inheritdoc />
        public int Length => Object.Length;

        /// <inheritdoc />
        public ulong Size => Object.Size;

        /// <inheritdoc />
        public IClrType Type { get; }
    }
}
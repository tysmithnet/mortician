﻿namespace Triage.Mortician.Core
{
    public interface IClrValueClass
    {
        /// <summary>
        /// The address of the object.
        /// </summary>
        ulong Address { get; }

        /// <summary>
        /// The address of the object in Hex format.
        /// </summary>
        string HexAddress { get; }

        /// <summary>
        /// The type of the object.
        /// </summary>
        IClrType Type { get; }

        /// <summary>
        /// Gets the given object reference field from this ClrObject.  Throws ArgumentException if the given field does
        /// not exist in the object.  Throws NullReferenceException if IsNull is true.
        /// </summary>
        /// <param name="fieldName">The name of the field to retrieve.</param>
        /// <returns>A ClrObject of the given field.</returns>
        IClrObject GetObjectField(string fieldName);

        /// <summary>
        /// Gets the value of a primitive field.  This will throw an InvalidCastException if the type parameter
        /// does not match the field's type.
        /// </summary>
        /// <typeparam name="T">The type of the field itself.</typeparam>
        /// <param name="fieldName">The name of the field.</param>
        /// <returns>The value of this field.</returns>
        T GetField<T>(string fieldName) where T : struct;

        /// <summary>
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        IClrValueClass GetValueClassField(string fieldName);

        /// <summary>
        /// Gets a string field from the object.  Note that the type must match exactly, as this method
        /// will not do type coercion.  This method will throw an ArgumentException if no field matches
        /// the given name.  It will throw a NullReferenceException if the target object is null (that is,
        /// if (IsNull returns true).  It will throw an InvalidOperationException if the field is not
        /// of the correct type.  Lastly, it will throw a MemoryReadException if there was an error reading
        /// the value of this field out of the data target.
        /// </summary>
        /// <param name="fieldName">The name of the field to get the value for.</param>
        /// <returns>The value of the given field.</returns>
        string GetStringField(string fieldName);
    }
}
﻿using System;
using System.Collections.Generic;           

namespace Triage.Mortician
{
    /// <summary>
    ///     Represents an object that is capable of getting the settings for this process
    /// </summary>
    public class SettingsRepository
    {
        protected internal SettingsRepository(Dictionary<string, string> settings)
        {
            SettingsInternal = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        protected internal Dictionary<string, string> SettingsInternal { get; set; }

        /// <summary>
        ///     Gets the setting associated with the provided key
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string Get(string key)
        {
            if(string.IsNullOrWhiteSpace(key))
                throw new ArgumentException($"{nameof(key)} cannot be null or whitespace");

            if (SettingsInternal.TryGetValue(key, out var value))
                return value;
            throw new ArgumentOutOfRangeException($"Could not find setting {key}");
        }
    }
}
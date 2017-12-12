﻿namespace Triage.Mortician.Abstraction
{
    /// <summary>
    /// Represents an AppDomain in the CLR
    /// </summary>
    public interface IAppDomain
    {
        /// <summary>
        /// Gets the name of the AppDomain e.g. Shared, Default
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Gets the configuration file for this app domain, can be null
        /// </summary>
        /// <value>
        /// The configuration file.
        /// </value>
        string ConfigFile { get; }

        /// <summary>
        /// Gets the application base directory for this app domain
        /// </summary>
        /// <value>
        /// The application base.
        /// </value>
        string ApplicationBase { get; }
        ulong Address { get; }
    }
}
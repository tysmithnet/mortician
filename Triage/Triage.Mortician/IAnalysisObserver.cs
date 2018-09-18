﻿// ***********************************************************************
// Assembly         : Triage.Mortician
// Author           : @tysmithnet
// Created          : 09-18-2018
//
// Last Modified By : @tysmithnet
// Last Modified On : 09-18-2018
// ***********************************************************************
// <copyright file="IAnalysisObserver.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Triage.Mortician
{
    /// <summary>
    ///     Represents an object that will observe messages emitted from other number crunching analyzers
    /// </summary>
    /// <seealso cref="Triage.Mortician.IAnalyzer" />
    /// <inheritdoc />
    /// <seealso cref="T:Triage.Mortician.IAnalyzer" />
    public interface IAnalysisObserver : IAnalyzer
    {
    }
}
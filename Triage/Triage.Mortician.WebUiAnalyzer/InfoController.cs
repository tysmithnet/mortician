﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Triage.Mortician.WebUiAnalyzer
{
    /// <summary>
    ///     Class ValuesController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public class InfoController : BaseController
    {
        [HttpGet]
        public object Get()
        {
            return new
            {
                StartTime = DumpInformationRepository.StartTimeUtc,
                Cpu = DumpInformationRepository.CpuUtilization
            };
        }
    }
}
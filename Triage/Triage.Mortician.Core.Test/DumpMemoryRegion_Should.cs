using System;
using FluentAssertions;
using Triage.Mortician.Core.ClrMdAbstractions;
using Triage.Testing.Common;
using Xunit;

namespace Triage.Mortician.Core.Test
{
    public class DumpMemoryRegion_Should : BaseTest
    {
        [Fact]
        public void Should_Construct_Without_Error()
        {
            // arrange
            Action sut = () => new DumpMemoryRegion();

            // act
            // assert
            sut.Should().NotThrow();
		}
    }
}
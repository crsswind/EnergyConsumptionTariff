using EnergyConsumptionTariff.Core.DTOs;
using EnergyConsumptionTariff.Core.Entities;
using EnergyConsumptionTariff.Core.Enums;
using EnergyConsumptionTariff.Core.Interfaces;
using EnergyConsumptionTariff.Core.Repositories;
using EnergyConsumptionTariff.Core.Services;
using EnergyConsumptionTariff.Tests.Core.Tests.Fixtures;
using FluentAssertions;
using Xunit;

namespace EnergyConsumptionTariff.Tests.Core.Tests.Services;

public class TariffComparisonServiceTests : IClassFixture<TariffComparisonServiceFixture>
{
    private readonly IReportService _service;
    private readonly ITariffRepository _tariffRepository;

    public TariffComparisonServiceTests(TariffComparisonServiceFixture fixture)
    {
        _service = fixture.Service;
        _tariffRepository = new TariffRepository();
    }

    [Fact]
    public void CompareTariffs_ShouldThrowArgumentException_WhenConsumptionIsNegative()
    {
        IEnumerable<ITariff> tariffs = new List<ITariff>
        {
            new FixedBaseTariff("basic electricity tariff", 5m, 0.22m),
            new IncrementalTariff("Packaged tariff", 800m, 4000m, 0.3m)
        };

        ReportService service = new ReportService(tariffs);

        Action act = () => service.GetTariffComparisonReport(-100m);
        act.Should().Throw<ArgumentException>().WithMessage("Consumption cannot be a negative value.*");
    }


    [Fact]
    public void CreateTariff_ThrowsArgumentException_WhenTariffTypeIsInvalid()
    {
        ITariffRepository tariffRepository = new TariffRepository();
        TariffType invalidTariffType = (TariffType)(-1);

        Action act = () => tariffRepository.CreateTariff(invalidTariffType);

        act.Should().Throw<ArgumentException>().WithMessage($"Tariff type '{invalidTariffType}' is not supported by the repository.");
    }

    [Fact]
    public void CompareTariffs_ShouldReturnTwoResults_WhenCalled()
    {
        decimal consumption = 3500;

        IEnumerable<TariffComparisonResult> results = _service.GetTariffComparisonReport(consumption);

        results.Should().HaveCount(2);
    }

    [Fact]
    public void CompareTariffs_ShouldReturnResultsInAscendingOrder_WhenCalled()
    {
        decimal consumption = 3500;

        IEnumerable<TariffComparisonResult> results = _service.GetTariffComparisonReport(consumption);

        TariffComparisonResult[] tariffComparisonResults = results as TariffComparisonResult[] ?? results.ToArray();
        tariffComparisonResults[0].AnnualCosts.Should().BeLessOrEqualTo(tariffComparisonResults[1].AnnualCosts);
    }

    [Theory]
    [InlineData(3500, 830)]
    [InlineData(4500, 1050)]
    [InlineData(6000, 1380)]
    public void TariffA_GetAnnualCosts_ReturnsCorrectResult(decimal consumption, decimal expectedAnnualCosts)
    {
        TariffType tariffType = TariffType.FixedBase;
        ITariff tariff = _tariffRepository.CreateTariff(tariffType);

        decimal annualCosts = tariff.GetAnnualCosts(consumption);

        annualCosts.Should().Be(expectedAnnualCosts);
    }

    [Theory]
    [InlineData(3500, 800)]
    [InlineData(4500, 950)]
    [InlineData(6000, 1400)]
    public void TariffB_GetAnnualCosts_ReturnsCorrectResult(decimal consumption, decimal expectedAnnualCosts)
    {
        TariffType tariffType = TariffType.Incremental;
        ITariff tariff = _tariffRepository.CreateTariff(tariffType);

        decimal annualCosts = tariff.GetAnnualCosts(consumption);

        annualCosts.Should().Be(expectedAnnualCosts);
    }

    [Fact]
    public void Constructor_ShouldCreateDefaultTariffs_WhenNoTariffsAreProvided()
    {
        IEnumerable<ITariff> tariffs = new List<ITariff>
        {
            new FixedBaseTariff("basic electricity tariff", 5m, 0.22m),
            new IncrementalTariff("Packaged tariff", 800m, 4000m, 0.3m)
        };

        ReportService service = new ReportService(tariffs);

        service.Should().NotBeNull();
        service.Should().BeOfType<ReportService>();
    }
}
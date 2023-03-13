using EnergyConsumptionTariff.Core.Enums;
using EnergyConsumptionTariff.Core.Interfaces;
using EnergyConsumptionTariff.Core.Repositories;
using FluentAssertions;
using Xunit;

namespace EnergyConsumptionTariff.Tests.Core.Tests.Repositories;

public class TariffRepositoryTests
{
    private readonly ITariffRepository _tariffRepository;

    public TariffRepositoryTests()
    {
        _tariffRepository = new TariffRepository();
    }

    [Fact]
    public void GetAllTariffs_ReturnsAllTariffs()
    {
        TariffRepository repository = new();
        var expectedTariffs = new List<ITariff>
        {
            repository.CreateTariff(TariffType.FixedBase),
            repository.CreateTariff(TariffType.Incremental)
        };

        IEnumerable<ITariff> actualTariffs = repository.GetAllTariffs();

        actualTariffs.Should().BeEquivalentTo(expectedTariffs);
    }

    [Fact]
    public void GetAllTariffs_ReturnsTariffsOfTypeITariff()
    {
        List<ITariff> tariffs = _tariffRepository.GetAllTariffs().ToList();

        tariffs.Should().NotBeNull();
        foreach (ITariff tariff in tariffs)
        {
            tariff.Should().BeAssignableTo<ITariff>();
        }
    }
}
using EnergyConsumptionTariff.Core.Entities;
using EnergyConsumptionTariff.Core.Interfaces;
using EnergyConsumptionTariff.Core.Services;

namespace EnergyConsumptionTariff.Tests.Core.Tests.Fixtures;

public class TariffComparisonServiceFixture
{
    public IReportService Service { get; }

    public TariffComparisonServiceFixture()
    {
        IEnumerable<ITariff> tariffs = new List<ITariff>
        {
            new FixedBaseTariff("basic electricity tariff", 5m, 0.22m),
            new IncrementalTariff("Packaged tariff", 800m, 4000m, 0.3m)
        };

        Service = new ReportService(tariffs);
    }
}
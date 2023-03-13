using EnergyConsumptionTariff.Core.Enums;
using EnergyConsumptionTariff.Core.Interfaces;
using EnergyConsumptionTariff.Core.Entities;

namespace EnergyConsumptionTariff.Core.Repositories;

public class TariffRepository : ITariffRepository
{
    private readonly IDictionary<TariffType, ITariff> _tariffs;

    public TariffRepository()
    {
        _tariffs = new Dictionary<TariffType, ITariff>();
    }

    public ITariff CreateTariff(TariffType tariffType)
    {
        if (!_tariffs.TryGetValue(tariffType, out var tariff))
        {
            tariff = CreateNewTariff(tariffType);
            _tariffs.Add(tariffType, tariff);
        }

        return tariff;
    }

    public IEnumerable<ITariff> GetAllTariffs()
    {
        return _tariffs.Values;
    }

    private static ITariff CreateNewTariff(TariffType tariffType)
    {
        return tariffType switch
        {
            TariffType.FixedBase => new FixedBaseTariff("Basic electricity tariff", 5m, 0.22m),
            TariffType.Incremental => new IncrementalTariff("Packaged tariff", 800m, 4000m, 0.3m),
            _ => throw new ArgumentException($"Tariff type '{tariffType}' is not supported by the repository."),
        };
    }
}
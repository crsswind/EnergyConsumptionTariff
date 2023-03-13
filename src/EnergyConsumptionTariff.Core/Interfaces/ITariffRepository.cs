using EnergyConsumptionTariff.Core.Enums;

namespace EnergyConsumptionTariff.Core.Interfaces;

public interface ITariffRepository
{
    ITariff CreateTariff(TariffType tariffType);
    IEnumerable<ITariff> GetAllTariffs();
}
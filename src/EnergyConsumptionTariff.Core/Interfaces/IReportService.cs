using EnergyConsumptionTariff.Core.DTOs;

namespace EnergyConsumptionTariff.Core.Interfaces;

public interface IReportService
{
    IEnumerable<TariffComparisonResult> GetTariffComparisonReport(decimal consumption);
}
using EnergyConsumptionTariff.Core.DTOs;
using EnergyConsumptionTariff.Core.Interfaces;

namespace EnergyConsumptionTariff.Core.Services;

public class ReportService : IReportService
{
    private readonly IEnumerable<ITariff> _tariffs;

    public ReportService(IEnumerable<ITariff> tariffs)
    {
        _tariffs = tariffs;
    }

    public IEnumerable<TariffComparisonResult> GetTariffComparisonReport(decimal consumption)
    {
        if (consumption < 0)
        {
            throw new ArgumentException("Consumption cannot be a negative value.", nameof(consumption));
        }

        List<TariffComparisonResult> tariffComparisonResults = new();

        // Loop through all the tariffs and compare their costs
        foreach (ITariff tariff in _tariffs)
        {
            decimal annualCosts = tariff.GetAnnualCosts(consumption);
            tariffComparisonResults.Add(new TariffComparisonResult(tariff.Name, annualCosts));
        }

        // Sort the results by annual costs and return
        return tariffComparisonResults.OrderBy(r => r.AnnualCosts);
    }
}
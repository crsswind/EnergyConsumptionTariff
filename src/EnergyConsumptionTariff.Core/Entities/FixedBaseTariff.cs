using EnergyConsumptionTariff.Core.Interfaces;

namespace EnergyConsumptionTariff.Core.Entities;

public class FixedBaseTariff : ITariff
{
    public string Name { get; }
    public decimal BaseCostsPerMonth { get; }
    public decimal ConsumptionCosts { get; }

    private const int MonthsInYear = 12;

    public FixedBaseTariff(string name, decimal baseCostsPerMonth, decimal consumptionCosts)
    {
        Name = name;
        BaseCostsPerMonth = baseCostsPerMonth;
        ConsumptionCosts = consumptionCosts;
    }

    public decimal GetAnnualCosts(decimal consumption)
    {
        return (BaseCostsPerMonth * MonthsInYear) + (consumption * ConsumptionCosts);
    }
}
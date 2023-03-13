using EnergyConsumptionTariff.Core.Interfaces;

namespace EnergyConsumptionTariff.Core.Entities;

public class IncrementalTariff : ITariff
{
    public string Name { get; }
    public decimal BaseCosts { get; }
    public decimal BaseConsumptionLimit { get; }
    public decimal AdditionalConsumptionCosts { get; }

    public IncrementalTariff(string name, decimal baseCosts, decimal baseConsumptionLimit, decimal additionalConsumptionCosts)
    {
        Name = name;
        BaseCosts = baseCosts;
        BaseConsumptionLimit = baseConsumptionLimit;
        AdditionalConsumptionCosts = additionalConsumptionCosts;
    }

    public decimal GetAnnualCosts(decimal consumption)
    {
        decimal additionalConsumption = Math.Max(0m, consumption - BaseConsumptionLimit);
        decimal additionalCost = additionalConsumption * AdditionalConsumptionCosts;

        return BaseCosts + additionalCost;
    }
}
namespace EnergyConsumptionTariff.Core.Interfaces;

public interface ITariff
{
    string Name { get; }
    decimal GetAnnualCosts(decimal consumption);
}
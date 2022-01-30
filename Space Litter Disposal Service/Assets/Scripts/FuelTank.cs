using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTank : MonoBehaviour
{
    #region fields
    private int maxFuelAmount;
    private int currentFuelAmount;
    private float consumeRate;
    #endregion

    #region properties
    public int CurrentFuelAmount { get => currentFuelAmount; }
    public float ConsumeRate { get => consumeRate; }
    public int MaxFuelAmount { get => maxFuelAmount; }
    #endregion

    #region constructors
    public FuelTank()
    {
        maxFuelAmount = 100;
        currentFuelAmount = maxFuelAmount;
        consumeRate = 0;
    }

    public FuelTank(int maxFuelAmount, float consumeRate)
    {
        this.maxFuelAmount = maxFuelAmount;
        currentFuelAmount = maxFuelAmount;
        this.consumeRate = consumeRate;
    }
    #endregion

    #region methods
    public int ChangeFuel(int amountToChange)
    {
        int newFuelAmount = currentFuelAmount + amountToChange;

        if (newFuelAmount > MaxFuelAmount)
            currentFuelAmount = MaxFuelAmount;
        else if (newFuelAmount < 0)
            currentFuelAmount = 0;
        else
            currentFuelAmount += amountToChange;

        return currentFuelAmount;
    }

    public void ConsumeFuel()
    {
        ChangeFuel(-1);
    }

    public bool FuelIsEmpty()
    {
        return currentFuelAmount == 0;
    }

    public void Refuel()
    {
        currentFuelAmount = maxFuelAmount;
        // TODO: tick up fuel
        // ChangeFuel(1);
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp
{
    public enum EffectType { SpeedBoost, SpeedDebuff, HealthBoost, HealthPickup, CashMoney}
    public EffectType thisPowerChoice;

    // what the modifier does
    public float speedModifier;
    public int healthModifier;
    public int maxHealthModifier;
    public float fireRateModifier;
    public int scoreBonus;

    // how long it lasts
    public float duration;

    // is it permanent
    public bool isPermanent;

    // speed boost
    void OnActivateSpeedBoost(TankData target)
    {
        target.adjustShipSpeed(speedModifier);
    }

    void OnDeactivateSpeedBoost(TankData target)
    {
        target.adjustShipSpeed(-speedModifier);
    }

    // speed debuff
    void OnActivateSpeedDebuff(TankData target)
    {
        target.adjustShipSpeed(-speedModifier);
    }

    void OnDeactivateSpeedDebuff(TankData target)
    {
        target.adjustShipSpeed(speedModifier);
    }

    // Total health boost
    void OnActivateHealthBoost(TankData target)
    {
        target.alterShipTotalHealth(maxHealthModifier, healthModifier);
    }

    void OnDeactivateHealthBoost(TankData target)
    {
        target.alterShipTotalHealth(-maxHealthModifier, -healthModifier);
    }

    // Health pickup
    void OnPickupHealth(TankData target)
    {
        target.addShipHealth(healthModifier);
    }

    // Score money pickup
    void OnPickupCashMoney(TankData target)
    {
        if (target.thisBeast == TankData.ShipType.PlayerShip)
        {
            target.addToScore(scoreBonus);
        }
    }

    public void ActivatePower(TankData target)
    {
        
        switch(thisPowerChoice)
        {
            case EffectType.SpeedBoost:
                OnActivateSpeedBoost(target);
                break;

            case EffectType.SpeedDebuff:
                OnActivateSpeedDebuff(target);
                break;

            case EffectType.HealthBoost:
                OnActivateHealthBoost(target);
                break;

            case EffectType.HealthPickup:
                OnPickupHealth(target);
                break;

            case EffectType.CashMoney:
                OnPickupCashMoney(target);
                break;

            default:
                OnPickupCashMoney(target);
                break;

        }
    }

    public void DeactivatePower(TankData target)
    {
        
        switch (thisPowerChoice)
        {
            case EffectType.SpeedBoost:
                OnDeactivateSpeedBoost(target);
                break;

            case EffectType.SpeedDebuff:
                OnDeactivateSpeedDebuff(target);
                break;

            case EffectType.HealthBoost:
                OnDeactivateHealthBoost(target);
                break;

            default:
                break;

        }
    }
}

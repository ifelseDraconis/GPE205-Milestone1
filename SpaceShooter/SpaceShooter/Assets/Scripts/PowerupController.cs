using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    private PowerUp thisPowerUp;
    public enum thisPowerupType { SpeedBoost, SpeedDebuff, HealthBoost, HealthPickup, CashMoney }
    public thisPowerupType powerUpChoice;

    private GameManager thisGameManager;

    // Start is called before the first frame update
    void Start()
    {
        this.thisPowerUp = new PowerUp();
        thisGameManager = GameManager.thisManager;
        this.thisPowerUp.duration = 0.0f;

        switch(powerUpChoice)
        {
            case thisPowerupType.SpeedBoost:
                this.thisPowerUp.thisPowerChoice = PowerUp.EffectType.SpeedBoost;
                this.thisPowerUp.speedModifier = thisGameManager.PowerUpspeedModifier;
                this.thisPowerUp.isPermanent = false;
                this.thisPowerUp.duration = thisGameManager.PowerUpduration;
                break;

            case thisPowerupType.SpeedDebuff:
                this.thisPowerUp.thisPowerChoice = PowerUp.EffectType.SpeedDebuff;
                this.thisPowerUp.speedModifier = thisGameManager.PowerUpspeedModifier;
                this.thisPowerUp.isPermanent = false;
                this.thisPowerUp.duration = thisGameManager.PowerUpduration;
                break;

            case thisPowerupType.HealthBoost:
                this.thisPowerUp.thisPowerChoice = PowerUp.EffectType.HealthBoost;
                this.thisPowerUp.healthModifier = thisGameManager.PowerUphealthModifier;
                this.thisPowerUp.isPermanent = false;
                this.thisPowerUp.duration = thisGameManager.PowerUpduration;
                break;

            case thisPowerupType.HealthPickup:
                this.thisPowerUp.thisPowerChoice = PowerUp.EffectType.HealthPickup;
                this.thisPowerUp.healthModifier = thisGameManager.PowerUphealthModifier;
                this.thisPowerUp.isPermanent = true;
                break;

            case thisPowerupType.CashMoney:
                this.thisPowerUp.thisPowerChoice = PowerUp.EffectType.CashMoney;
                this.thisPowerUp.scoreBonus = thisGameManager.PowerUpscoreBonus;
                this.thisPowerUp.isPermanent = true;
                break;

            default:
                this.thisPowerUp.thisPowerChoice = PowerUp.EffectType.CashMoney;
                this.thisPowerUp.scoreBonus = thisGameManager.PowerUpscoreBonus;
                this.thisPowerUp.isPermanent = true;
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "BadGuy")
        {
            TellMyParentILoveThem();
            Destroy(gameObject);
        }
        
    }

    public PowerUp getPowerUp()
    {
        return thisPowerUp;
    }

    void TellMyParentILoveThem()
    {
        GameObject heyMom = gameObject.transform.parent.gameObject;
        heyMom.GetComponent<PowerCreator>().someoneAteIt();
    }
}

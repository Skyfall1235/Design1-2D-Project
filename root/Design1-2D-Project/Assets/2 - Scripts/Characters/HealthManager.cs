using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager
{
    //static values across all instances
    public static int maximumBaseHP = 100;
    public static int maximumDamageValue = 90;

    #region health variables
    private int trueHealth;
    public int TrueHP
    {
        get { return trueHealth; }
        set { trueHealth = value; }
    }


    private int bonusHealth;
    public int BonusHP
    {
        get { return bonusHealth; }
        set { bonusHealth = value; }
    }


    private int baseHealth;
    public int BaseHP
    {
        get { return baseHealth; }
        set { baseHealth = value; }
    }
    #endregion


    public void SetHP(int inputHealth)
    {
        //sets the players base hp to just 100, and then calculates the trueHP
        bonusHealth = 0;
        baseHealth = inputHealth;
        TrueHealthClaculation();
    }

    public void TakeDamage(int damageValue)
    {
        //if the damagevalue is more than the max damage allowed, reduce it
        if (damageValue > maximumDamageValue) { damageValue = maximumDamageValue; }
        trueHealth -= damageValue;
        //handle death here if true health = 0
    }

    public void HealHP(int healAmount)
    {
        if(baseHealth < 100)
        {
            //heal the hp by the amount, then if it goes above 100, stop it
            baseHealth += healAmount;
            Debug.Log($"player healed {healAmount} health");
            if (baseHealth > maximumBaseHP) { baseHealth = maximumBaseHP; }
        }
        TrueHealthClaculation();
    }

    public void GiveBonusHP(int bonusHPAmount)
    {
        //if the base hp is lower that 100, heal the HP first and use the rest on the bonus health amount.
        //if (bonus)
    }

    public int TrueHealthClaculation()
    {
        //returns the true health. read da code nerd
        trueHealth = (baseHealth + bonusHealth);
            return trueHealth;
    }


}

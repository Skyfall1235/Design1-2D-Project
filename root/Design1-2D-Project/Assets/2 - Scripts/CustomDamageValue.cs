using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CustomDamageValue : MonoBehaviour
{
    //this will be placed on every collider on the attack
    //and defend locations of the player,

    PlayerData currentSaveData;
    [SerializeField] bool isDefending;

    private void Start()
    {
        

    }

    private void OnCollisionEnter(Collision col)
    {
        if(isDefending)
        {
            //turn on the collider, and block the attack
        }
        if(!isDefending)
        {
            //get tthe current attack type, and calculate the damage
            int chosenDamageValue;
            
            switch (currentSaveData.weaponType)
            {
            case WeaponType.Stick:
                    chosenDamageValue = 20;    
                    break;
            case WeaponType.Knife:
                    chosenDamageValue = 45;
                    break;
            case WeaponType.Sword:
                    chosenDamageValue = 75;
                    break;
            }
            I_Interactable interactable = col.gameObject.GetComponent<I_Interactable>();
            if (interactable != null)
            {
                interactable.TakeDamage(10); // Call TakeDamage method with parameter of 10
            }
            //assuming the HP of enemies is between 50-125, do some damage there
        }
    }

}

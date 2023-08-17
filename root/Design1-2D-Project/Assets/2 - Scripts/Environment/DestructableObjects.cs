using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DestructableObjects : MonoBehaviour, I_Interactable
{
    HealthManager healthManager = new HealthManager();
    [SerializeField] private int setBaseHP;
    [SerializeField] private int setBonusHP;
    // Start is called before the first frame update
    void Start()
    {
        VariableAssignment();
    }
    void Update()
    {
        if (healthManager.TrueHP <= 0)
        {
            Destroy(gameObject);
        }

    }
    void VariableAssignment()
    {
        healthManager.BonusHP = setBonusHP;
        healthManager.BaseHP = setBaseHP;
        //trueHealth is just for display purposes, if required
        healthManager.TrueHealthClaculation();
    }
    void I_Interactable.TakeDamage(int damage)
    {
        healthManager.TakeDamage(damage);
    }
    void I_Interactable.Interact()
    {
        //not needed here but is required to exist
        return;
    }
}

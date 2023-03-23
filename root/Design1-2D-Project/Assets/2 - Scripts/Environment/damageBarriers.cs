using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageBarriers : MonoBehaviour
{
    [SerializeField] int damageToPlayer;

    private void OnCollisionEnter2D(Collision2D hit)
    {
        Debug.Log(damageToPlayer);
        if (hit.gameObject.layer == 7)
        {
            hit.gameObject.GetComponent<I_Interactable>().TakeDamage(damageToPlayer);
            Debug.Log(damageToPlayer + hit.gameObject.layer);
        }
    }
}

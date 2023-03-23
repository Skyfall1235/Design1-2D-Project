using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPractice : MonoBehaviour
{
    // Vars
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    private float timeSinceAttack = 0.0f;
    public float attackRange = 0.5f;
    private int currentAttack = 0;
    public int attackDamage = 40;

    void Update()
    {
        if (Input.GetKeyDown(0) && timeSinceAttack > 0.25f)
        {
            Attack();
        }
    }

    void Attack()
    {
        currentAttack++;

        // Loop back to one after third attack
        if (currentAttack > 3)
            currentAttack = 1;

        // Reset Attack combo if time since last attack is too large
        if (timeSinceAttack > 1.0f)
            currentAttack = 1;

        // Call one of three attack animations "Attack1", "Attack2", "Attack3"
        animator.SetTrigger("Attack" + currentAttack);

        // Reset timer
        timeSinceAttack = 0.0f;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    //colliders are top to bottom
    [SerializeField] private GameObject[] attackColliders;
    [SerializeField] private GameObject[] defendColliders;
    //what varaibles do i need? i need all the instantiation locations on the player, he cooldown of the attacks for the player?
    //and maybe the weapon types fo at leas t the animation triggers.
    // Vars
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    private float timeSinceAttack = 0.0f;
    public bool attacking = false;
    [SerializeField] private float attackSpeed;
    public int currentAttack = 0;
    public int attackDamage = 40;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && timeSinceAttack > 0.25f && !attacking)
        {
            //Debug.Log("attacked 1");
            AttackAction();
            
        }
        //if (Input.GetButtonDown("b") && !attacking)
        //{
        //    BlockAction();
        //}
        timeSinceAttack += Time.deltaTime;
    }

    void AttackAction()
    {
        //Debug.Log("attacked 2");
        currentAttack++;

        // Loop back to one after third attack
        if (currentAttack > 3)
        {
            currentAttack = 1;
        }
            

        // Reset Attack combo if time since last attack is too large
        if (timeSinceAttack > 3.0f)
        {
            currentAttack = 1;
        }
        // Call one of three attack animations "Attack1", "Attack2", "Attack3"
        //animator.SetTrigger("Attack" + currentAttack);

        // Reset timer
        timeSinceAttack = 0.0f;
        DetermineAttack(currentAttack);
        
    }
    //void BlockAction()
    //{

    //}
    void DetermineAttack(int attack)
    {
        //Debug.Log("attacked 3");
        switch (attack)
        {
            case 1:
                StartCoroutine(Attack1());
                break;
            case 2:
                StartCoroutine(Attack2());
                break;
            case 3:
                StartCoroutine(Attack3());
                break;
            default:
                StartCoroutine(Attack1());
                break;
        }  
    }

    IEnumerator Attack1()
    {
        attacking = true;
        //turn on 1, then 2, then turn off 1, then 2
        attackColliders[0].SetActive(true);
        yield return new WaitForSeconds(attackSpeed);
        attackColliders[1].SetActive(true);
        yield return new WaitForSeconds(attackSpeed);
        attackColliders[0].SetActive(false);
        yield return new WaitForSeconds(attackSpeed);
        attackColliders[1].SetActive(false);
        yield return new WaitForSeconds(attackSpeed);
        Debug.Log("attacked");
        attacking = false;

    }
    IEnumerator Attack2()
    {
        attacking = true;
        //turn on 2, then 1, then off 1, then off 2
        attackColliders[1].SetActive(true);
        yield return new WaitForSeconds(attackSpeed);
        attackColliders[0].SetActive(true);
        yield return new WaitForSeconds(attackSpeed);
        attackColliders[1].SetActive(false);
        yield return new WaitForSeconds(attackSpeed);
        attackColliders[0].SetActive(false);
        yield return new WaitForSeconds(attackSpeed);
        Debug.Log("combo!");
        attacking = false;
    }
    IEnumerator Attack3()
    {
        //this is the stab
        attacking = true;
        attackColliders[2].SetActive(true);
        yield return new WaitForSeconds(attackSpeed);
        attackColliders[3].SetActive(true);
        yield return new WaitForSeconds(attackSpeed);
        attackColliders[2].SetActive(false);
        yield return new WaitForSeconds(attackSpeed);
        attackColliders[3].SetActive(false);
        yield return new WaitForSeconds(attackSpeed);
        Debug.Log("crit!");
        attacking = false;
    }

    //first, i need control for the availavle actions that the player can do.
    //probably a switch case



    //each method should be a coroutine as it will need to  run over a series of TIME, NOT JUST BE INSTANTANIOUS
    //each method will take the instantiation locations and spawn the respective prefab for it


    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, 2);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //[SerializeField] GameObject player;
    [SerializeField] PlayerController player;

    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float turnSpeed;

    [Header("Attack")]
    [SerializeField] bool attacking;
    [SerializeField] float attackRange;
    [SerializeField] float attackDelay;
    [SerializeField] int attackDamage;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        attacking = false;

    }

    // Update is called once per frame
    void Update()
    {
        TurnTowardPlayer();
        MoveForward();
    
        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange && !attacking)
        {
            StartCoroutine(Attack());
        }

    }

    IEnumerator Attack()
    {
        float originalTurnSpeed = turnSpeed;
        turnSpeed *= 10;
        attacking = true;
        yield return new WaitForSeconds(attackDelay);

        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            player.TakeDamage(attackDamage);
        }  
        
        turnSpeed = originalTurnSpeed;

        yield return new WaitForSeconds(attackDelay);

        attacking = false;
    }

    void MoveForward()
    {
        if (!attacking)
        {
            transform.position += transform.right * moveSpeed * Time.deltaTime;
        }
    }

    void TurnTowardPlayer()
    {

        Vector3 toTarget = player.transform.position - transform.position;
        Vector3 rotatedToTarget = Quaternion.Euler(0, 0, 90) * toTarget;

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, rotatedToTarget);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
}

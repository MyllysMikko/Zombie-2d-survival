using System;
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

    //Pidetään turnSpeed tallessa.
    //turnSpeediä kasvatetaan kun zombie hyökkää. Mikäli zombie kuolee hyökkäämisen aikana, turnspeedia ei resetoida.
    [SerializeField] float defaultTurnSpeed;

    [Header("HP")]
    [SerializeField] int hp;
    public GameObject CoinModel;

    public EventHandler Died;

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
        turnSpeed *= 10;
        attacking = true;
        yield return new WaitForSeconds(attackDelay);

        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            player.TakeDamage(attackDamage);
        }

        turnSpeed = defaultTurnSpeed;

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

    public void SetStats(int hp, float moveSpeed, int attackDamage)
    {
        this.hp = hp;
        this.moveSpeed = moveSpeed;
        this.attackDamage = attackDamage;

        attacking = false;
        turnSpeed = defaultTurnSpeed;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Die();

        }
    }

    public void Die()
    {
        if (Died != null)
        {

            Died.Invoke(this, EventArgs.Empty);

        }
        gameObject.SetActive(false);
        DropCoin();
    }

    private void DropCoin()
    {
        if (CoinModel != null)
        {
            Vector2 position = transform.position;
            GameObject coin = Instantiate(CoinModel, position + new Vector2(0.0f, 1.0f), Quaternion.identity);
            coin.SetActive(true);
            Destroy(coin, 5f);
        }
    }
}

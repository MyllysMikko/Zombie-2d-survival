using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //[SerializeField] GameObject player;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PlayerController player;
    [SerializeField] SpriteRenderer spriteRenderer;

    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float currentSpeed;
    [SerializeField] float acceleration = 1;
    [SerializeField] float turnSpeed;

    //Pidet‰‰n turnSpeed tallessa.
    //turnSpeedi‰ kasvatetaan kun zombie hyˆkk‰‰. Mik‰li zombie kuolee hyˆkk‰‰misen aikana, turnspeedia ei resetoida.
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

    public AudioSource source;
    public AudioClip clip;

    [SerializeField] Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        attacking = false;
        currentSpeed = 0;
        anim.SetBool("walking", true);
    }

    // Update is called once per frame
    void Update()
    {
        //TurnTowardPlayer();
        MoveForward();
    
        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange && !attacking)
        {
            StartCoroutine(Attack());
        }

    }

    void FixedUpdate()
    {
        if (!attacking)
        {
            currentSpeed += acceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, moveSpeed);
            //transform.position += transform.right * currentSpeed * Time.deltaTime;
            rb.MovePosition(transform.position += transform.right * currentSpeed * Time.deltaTime);
        }

        TurnTowardPlayer(false);
    }

    IEnumerator Attack()
    {
        anim.SetBool("walking", false);
        turnSpeed *= 10;
        attacking = true;
        anim.SetTrigger("attack");
        yield return new WaitForSeconds(0.7f);

        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange + 0.05f)
        {
            player.TakeDamage(attackDamage);
        }



        turnSpeed = defaultTurnSpeed;

        yield return new WaitForSeconds(attackDelay);

        attacking = false;
        anim.SetBool("walking", true);
    }

    void MoveForward()
    {

    }

    /// <summary>
    /// K‰‰nt‰‰ Zombia katsomaan pelaajaa kohti.
    /// Boolean hallitsee tapahtuuko t‰m‰ heti vai ajan mukana
    /// </summary>
    /// <param name="instantly"></param>
    void TurnTowardPlayer(bool instantly)
    {

        Vector3 toTarget = player.transform.position - transform.position;
        Vector3 rotatedToTarget = Quaternion.Euler(0, 0, 90) * toTarget;

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, rotatedToTarget);

        if (!instantly)
        {
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime));
        }
        else
        {
            transform.rotation = targetRotation;
        }

        
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        currentSpeed = 0;
        Debug.Log(currentSpeed);

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

    public void SetStats(int hp, float moveSpeed, int attackDamage)
    {
        this.hp = hp;
        this.moveSpeed = moveSpeed;
        this.attackDamage = attackDamage;

        attacking = false;
        turnSpeed = defaultTurnSpeed;
    }

    public void SetLayerOrder(int index)
    {
        spriteRenderer.sortingLayerName = "Enemy";
        spriteRenderer.sortingOrder = index;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        TurnTowardPlayer(true);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        TurnTowardPlayer(true);

        //if (collision.collider.CompareTag("Enemy"))
        //{
        //    Debug.Log("Separate");
        //    Vector3 dir = (transform.position - collision.transform.position).normalized;
        //    transform.position += dir * 0.1f;
        //    collision.transform.position -= dir * 0.01f;
        //}

    }
}

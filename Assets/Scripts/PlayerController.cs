using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hpText;

    [SerializeField] AudioSource audiosource;
    [SerializeField] AudioClip hurt;

    public EventHandler playerDied;

    [SerializeField] int currentHP;
    [SerializeField] int maxHP;

    public float moveSpeed = 5f;
    public float currentSpeed = 0;
    float moveX;
    float moveY;

    public float acceleration = 2f;
    public float deceleration = 2f;

    public Vector3 moveDirection;
    private Vector3 lastRecorderDir;


    [SerializeField] Rigidbody2D rb;

    [Header("Animations")]
    [SerializeField] PlayerAnimationController playerAnim;


    private void Start()
    {
        currentHP = maxHP;
        UpdateHpText();
    }

    private void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        Move();   
    }

    public void Awake()
    {
        

    }
    void ProcessInputs()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

    }

    void Move()
    { 

        Vector3 direction = new Vector3(moveX, moveY, 0f).normalized;

        if (playerAnim != null)
        {
            playerAnim.AnimateFeet(direction);
        }


        if (direction.magnitude >= 0.1f)
        {
            currentSpeed += acceleration * Time.deltaTime;
            if (currentSpeed > moveSpeed)
            {
                currentSpeed = moveSpeed;
            }
            rb.MovePosition(transform.position + direction * currentSpeed * Time.deltaTime);

            lastRecorderDir = direction;

            //feetAnimator.SetBool("running", true);
        }

        else
        {
            currentSpeed -= deceleration * Time.deltaTime;
            if (currentSpeed < 0)
            {
                currentSpeed = 0;
            }
            rb.MovePosition(transform.position + lastRecorderDir * currentSpeed * Time.deltaTime);
            //feetAnimator.SetBool("running", false);
        }
    }

    public void RestoreHP()
    {
        currentHP = maxHP;
        UpdateHpText();
    }

    public void IncreaseMaxHP(int hp)
    {
        //HP:n määrä suhteessa maksimi hp:hen pidetään samana.
        //Esim: Jos maksimi hp on 100 ja pelaajalla on 50 hp. 50 / 100 = 0.5. Hänellä on 50% hp:ta.
        //Jos maksimi hp nostetaan 150. 50% tästä on 75
        // 0.5 * 150 = 75
        float currentHPPercentage = (float)currentHP / (float)maxHP;
        maxHP = maxHP + hp;
        currentHP = (int)(currentHPPercentage * maxHP);

        UpdateHpText();
    }

    public void TakeDamage(int damage)
    {
        if (currentHP > 0)
        {
            audiosource.PlayOneShot(hurt);
            currentHP -= damage;
            UpdateHpText();
            if (currentHP <= 0)
            {
                gameObject.SetActive(false);
                if (playerDied != null)
                {
                    playerDied.Invoke(this, EventArgs.Empty);
                }
            }
        }

    }

    void UpdateHpText()
    {
        if (hpText != null)
        {
            hpText.text = $" {currentHP} / {maxHP}";
        }
    }

    enum PlayerState
    {
        Alive,
        Pause,
        Dead,
    }


}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip enemyHurt;
    [SerializeField] AudioClip enemyAttack;
    [SerializeField] AudioClip enemyDie;

    [SerializeField] float delayBetweenHurt = 0.1f;
    float nextHurt = 0;
    [SerializeField] float delayBetweenAttack = 0.0f;
    float nextAttack = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHurt(object sender, EventArgs e)
    {
        Debug.Log("Hurt");
        if (Time.time >= nextHurt)
        {
            audioSource.PlayOneShot(enemyHurt);
            nextHurt = Time.time + delayBetweenHurt;
        }

    }

    public void OnAttack(object sender, EventArgs e)
    {
        audioSource.PlayOneShot(enemyAttack);
    }

    public void OnDeath(object sender, EventArgs e)
    {
        audioSource.PlayOneShot(enemyDie);
    }
}

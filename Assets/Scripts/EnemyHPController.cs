using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPController : MonoBehaviour
{
    
    [SerializeField] int hp;
    public GameObject CoinModel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        gameObject.SetActive(false);
        DropCoin();
    }
    private void DropCoin()
    {
        Vector2 position = transform.position;
        GameObject coin = Instantiate(CoinModel, position + new Vector2(0.0f, 1.0f),Quaternion.identity);
        coin.SetActive(true);
        Destroy(coin,5f);
    }
}

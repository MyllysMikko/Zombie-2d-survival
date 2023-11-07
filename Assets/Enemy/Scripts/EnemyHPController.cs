using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPController : MonoBehaviour
{
    
    [SerializeField] int hp;
    public GameObject CoinModel;

    public EventHandler Died;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHP(int hp)
    {
        this.hp = hp;
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
        Died.Invoke(this, EventArgs.Empty);
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

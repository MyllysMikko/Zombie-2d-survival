using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPController : MonoBehaviour
{

    [SerializeField] int hp;


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
            gameObject.SetActive(false);
        }
    }
}
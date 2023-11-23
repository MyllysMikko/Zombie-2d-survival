using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{

    public void Die()
    {
        Destroy(gameObject);
        DropCoin();
    }

    private void DropCoin()
    {
        Vector3 position = transform.position;
        GameObject coin = Instantiate(gameObject);
    }
}

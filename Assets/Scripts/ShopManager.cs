using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject shopCanvas;
    [SerializeField] PlayerController playerController;

    bool inRange;
    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
        shopCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            shopCanvas.SetActive(!shopCanvas.activeInHierarchy);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
            shopCanvas.SetActive(false);
        }
    }
}

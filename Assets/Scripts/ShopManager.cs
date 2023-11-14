using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject shopCanvas;
    [SerializeField] PlayerController playerController;

    // PIDÄ HUOLTA ETTÄ NÄMÄ OVAT YHTÄ PITKIÄ.
    // = Jokaista upgradea kohtaa on hinta.
    public List<Upgrade> upgrades = new List<Upgrade>();
    [SerializeField] TextMeshProUGUI[] prices;

    bool inRange;
    // Start is called before the first frame update
    void Start()
    {
        UpdateShopDisplay();
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
    public void BuyUpgrade(int index)
    {
        if (index >= 0 && index < upgrades.Count)
        {
            Upgrade upgrade = upgrades[index];
            if (true) // Score.moneyAmount >= upgrade.price
            {
                upgrade.price = (int)(upgrade.price * upgrade.priceIncreasePercentage);
                UpdateShopDisplay();
            }
        }
    }

    void UpdateShopDisplay()
    {
        for (int i = 0; i < upgrades.Count; i++)
        {
            prices[i].text = upgrades[i].price.ToString();
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

[System.Serializable]
public class Upgrade
{
    public string name;
    public int price;
    public float priceIncreasePercentage; // Muodossa 1.00f! Esim 10% olisi 1.10f
}

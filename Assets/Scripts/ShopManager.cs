using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject shopCanvas;
    [SerializeField] PlayerController playerController;
    [SerializeField] WeaponHandler weaponHandler;

    // PIDÄ HUOLTA ETTÄ NÄMÄ OVAT YHTÄ PITKIÄ.
    // = Jokaista upgradea kohtaa on hinta.
    public List<Upgrade> upgrades = new List<Upgrade>();
    [SerializeField] TextMeshProUGUI[] prices;

    bool inRange;
    public static bool shopActive;

    [Header("Stat increases")]
    [SerializeField] int hpIncrease;
    [SerializeField] float damageIncreasePercentage;
    [SerializeField] float rateOfFireIncreasePercentage;
    [SerializeField] float clipIncreasePercentage;
    [SerializeField] float reserveIncreasePercentage;
    // Start is called before the first frame update
    void Start()
    {
        UpdateShopDisplay();
        inRange = false;
        shopActive = false;
        shopCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (inRange && !WaveManager.ongoingWave && Input.GetKeyDown(KeyCode.E))
        {
            shopActive = !shopActive;
            shopCanvas.SetActive(shopActive);
        }

        if (shopActive && WaveManager.ongoingWave)
        {
            shopActive = false;
            shopCanvas.SetActive(shopActive);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Score.moneyAmount += 100000;
        }
    }
    public void BuyUpgrade(int index)
    {
        if (index >= 0 && index < upgrades.Count)
        {
            Upgrade upgrade = upgrades[index];
            if (Score.moneyAmount >= upgrade.price)
            {
                Score.moneyAmount -= upgrade.price;

                UpgradeStat(upgrade.stat);

                upgrade.price = (int)(upgrade.price * upgrade.priceIncreasePercentage);
                UpdateShopDisplay();
            }
        }
    }

    void UpgradeStat(Upgrade.Stat stat)
    {
        switch (stat)
        {
            case Upgrade.Stat.HP:
                playerController.IncreaseMaxHP(hpIncrease);
                break;
            case Upgrade.Stat.RestoreHP:
                playerController.RestoreHP();
                break;
            case Upgrade.Stat.WeaponDamage:
                weaponHandler.IncreaseWeaponDamage(damageIncreasePercentage);
                break;
            case Upgrade.Stat.ShootSpeed:
                weaponHandler.IncreaseWeaponRateOfFire(rateOfFireIncreasePercentage);
                break;
            case Upgrade.Stat.ClipSize:
                weaponHandler.IncreaseWeaponClipSize(clipIncreasePercentage);
                break;
            case Upgrade.Stat.WeaponMaxAmmo:
                weaponHandler.IncreaseWeaponMaxReserve(reserveIncreasePercentage);
                break;
            case Upgrade.Stat.RestoreAmmo:
                weaponHandler.RestoreWeaponAmmo();
                break;
            default:
                break;
        }
    }

    void UpdateShopDisplay()
    {
        for (int i = 0; i < upgrades.Count; i++)
        {
            //prices[i].text = upgrades[i].price.ToString();
            prices[i].text = $"${upgrades[i].price}";
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
            shopActive = false;
            shopCanvas.SetActive(shopActive);
        }
    }


}

[System.Serializable]
public class Upgrade
{
    public Stat stat;
    public int price;
    public float priceIncreasePercentage; // Muodossa 1.00f! Esim 10% olisi 1.10f

    public enum Stat
    {
        HP,
        RestoreHP,
        WeaponDamage,
        ShootSpeed,
        ClipSize,
        WeaponMaxAmmo,
        RestoreAmmo
    }
}

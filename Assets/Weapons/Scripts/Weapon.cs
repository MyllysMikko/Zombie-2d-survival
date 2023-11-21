using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] PlayerAnimationController playerAnim;

    public bool reloading;

    [SerializeField] TextMeshProUGUI clipText;
    [SerializeField] TextMeshProUGUI reserveText;

    [Header("Gun attributes")]
    [SerializeField] int damage;
    [SerializeField] float bulletSpread;
    [SerializeField] int bulletsPerSecond;
    [SerializeField] float shootDelay;
    [SerializeField] float nextShotAt;
    [SerializeField] float shotLenght;
    [SerializeField] GameObject bulletTrail;
    [SerializeField] GunType gunType;

    [Header("Ammo")]
    [SerializeField] int maxClip;
    [SerializeField] int currentClip;
    [SerializeField] int maxReserve;
    [SerializeField] int currentReserve;
    [SerializeField] float reloadTime;


    // Start is called before the first frame update
    void Start()
    {
        CalculateRateOfFire();
        nextShotAt = 0;
        currentClip = maxClip;
        currentReserve = maxReserve;
    }

    private void OnEnable()
    {
        UpdateAmmoHud();

        if (gunType == GunType.automatic)
        {
            playerAnim.SetInt(0);
        }
        else
        {
            playerAnim.SetInt(1);
        }
    }


    // Update is called once per frame
    void Update()
    {
        switch (gunType)
        {
            case GunType.Semiautomatic:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Shoot();
                }
                break;

            case GunType.automatic:
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    Shoot();
                }
                break;

            default:
                break;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!reloading && currentClip < maxClip && currentReserve > 0)
            {
                StartCoroutine(Reload());
            }
        }

    }

    IEnumerator Reload()
    {
        reloading = true;
        playerAnim.TriggerReload();
        yield return new WaitForSeconds(reloadTime);

        int ammountToReload = maxClip - currentClip;

        if (currentReserve - ammountToReload < 0)
        {
            currentClip += currentReserve;
            currentReserve = 0;
        }
        else
        {
            currentClip = maxClip;
            currentReserve -= ammountToReload;
        }

        reloading = false;
        UpdateAmmoHud();
    }

    public virtual void Shoot()
    {
        if (!reloading)
        {
            if (Time.time >= nextShotAt && currentClip > 0)
            {
                FireBullet();

                currentClip--;
                nextShotAt = Time.time + shootDelay;
                UpdateAmmoHud();

                playerAnim.TriggerShoot();
            }
        }
    }

    public virtual void FireBullet()
    {

        float randomAngle = UnityEngine.Random.Range(-bulletSpread, bulletSpread);
        Quaternion deviation = Quaternion.AngleAxis(randomAngle, Vector3.forward);

        Vector3 finalDirection = deviation * transform.right;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, finalDirection);
        GameObject trail = Instantiate(bulletTrail, transform.position, Quaternion.identity);

        if (hit.collider != null)
        {
            trail.GetComponent<BulletTrail>().SetTarget(hit.point);

            EnemyController enemy= hit.collider.GetComponent<EnemyController>();

            if (enemy != null)
            {
                enemy.TakeDamage(CalculateDamage());

                Debug.Log("Hit!");
            }

        }
        else
        {
            trail.GetComponent<BulletTrail>().SetTarget(transform.position + finalDirection * 20);
        }
    }

    public void UpdateAmmoHud()
    {
        clipText.text = $"{currentClip} / {maxClip}";
        reserveText.text = currentReserve.ToString();
    }


    //Mikäli halutaan laskea vahinko eri tavalla, tämä voidana overrideta
    public virtual int CalculateDamage()
    {
        return damage;
    }

    public void CalculateRateOfFire()
    {
        shootDelay = 1f / bulletsPerSecond;
    }

    public void IncreaseDamage(float increasePercentage)
    {
        damage = (int)Mathf.Round(damage * increasePercentage);
    }

    public void IncreaseRateOfFire(float increasePercentage)
    {
        bulletsPerSecond = (int)Mathf.Round(bulletsPerSecond * increasePercentage);
        CalculateRateOfFire();
    }

    public void IncreaseMaxReserve(float increasePercentage)
    {
        maxReserve = (int)Mathf.Round(maxReserve * increasePercentage);
    }

    public void IncreaseClipSize(float increasePercentage)
    {
        maxClip = (int)Mathf.Round(maxClip * increasePercentage);
    }

    public void RestoreAmmo()
    {
        currentClip = maxClip;
        currentReserve = maxReserve;
    }

    enum GunType
    {
        Semiautomatic,
        automatic
    }


}

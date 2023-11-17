using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool reloading;

    [Header("Gun attributes")]
    [SerializeField] int damage;
    [SerializeField] float bulletSpread;
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
        nextShotAt = 0;
        currentClip = maxClip;
        currentReserve = maxReserve;
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


    //Mikäli halutaan laskea vahinko eri tavalla, tämä voidana overrideta
    public virtual int CalculateDamage()
    {
        return damage;
    }

    public void IncreaseDamage(float increasePercentage)
    {
        Debug.Log(MathF.Round(damage * increasePercentage));
        damage = (int)Mathf.Round(damage * increasePercentage);
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

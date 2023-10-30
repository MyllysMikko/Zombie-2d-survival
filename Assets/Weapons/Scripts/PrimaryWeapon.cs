using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryWeapon : MonoBehaviour
{
    [SerializeField] bool reloading;

    [Header("Gun attributes")]
    [SerializeField] float shootDelay;
    [SerializeField] float nextShotAt;
    [SerializeField] int damage;
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

    public void Shoot()
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

    void FireBullet()
    {
        //Vector3 direction = GetDirection();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 direction = (mousePosition - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);

        if (hit.collider != null)
        {
            Debug.Log("hit");

            GameObject trail = Instantiate(bulletTrail, transform.position, Quaternion.identity);
            trail.GetComponent<BulletTrail>().SetTarget(mousePosition);
        }
    }

    enum GunType
    {
        Semiautomatic,
        automatic
    }


}

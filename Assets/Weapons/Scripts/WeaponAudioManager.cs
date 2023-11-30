using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip shoot;
    [SerializeField] AudioClip rifleReload;
    [SerializeField] AudioClip handgunReload;

    //Kauanko laukauksien välissä on vähintään aikaa. Tämä on ettei audio täysin hajoa suurilla ampumisnopeuksilla.
    [SerializeField] float delayBetweenShots = 0.1f;
    float nextShot = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        if (Time.time >= nextShot)
        {
            audioSource.PlayOneShot(shoot);
            nextShot = Time.time + delayBetweenShots;
        }

    }

    public void Reload(Weapon.GunType type)
    {
        switch (type)
        {
            case Weapon.GunType.automatic:
                audioSource.PlayOneShot(rifleReload);
                break;

            case Weapon.GunType.Semiautomatic:
                audioSource.PlayOneShot(handgunReload);
                break;
        }
    }

}

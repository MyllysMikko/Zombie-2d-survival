using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] Weapon[] weapons = new Weapon[2];
    [SerializeField] int weaponIndex;

    // Start is called before the first frame update
    void Start()
    {
        weaponIndex = 0;
        SwitchWeapon(weaponIndex);
    }

    // Update is called once per frame
    void Update()
    {

        int lastIndex = weaponIndex;

        // Otetaan käyttäjän syöte aseen vaihtamiseen.
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            weaponIndex++;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            weaponIndex--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponIndex = 1;
        }

        //Pidetään weaponIndex nollan ja yhden välillä.
        if (weaponIndex > 1)
        {
            weaponIndex = 0;
        }
        else if (weaponIndex < 0)
        {
            weaponIndex = 1;
        }

        if (lastIndex != weaponIndex)
        {
            SwitchWeapon(weaponIndex);
        }
    }

    public void IncreaseWeaponDamage(float increasePercentage)
    {
        foreach (Weapon weapon in weapons)
        {
            weapon.IncreaseDamage(increasePercentage);
        }
    }

    public void IncreaseWeaponMaxReserve(float increasePercentage)
    {
        foreach (Weapon weapon in weapons)
        {
            weapon.IncreaseMaxReserve(increasePercentage);
        }
    }

    public void IncreaseWeaponClipSize(float increasePercentage)
    {
        foreach (Weapon weapon in weapons)
        {
            weapon.IncreaseClipSize(increasePercentage);
        }
    }

    public void RestoreWeaponAmmo()
    {
        foreach (Weapon weapon in weapons)
        {
            weapon.RestoreAmmo();
        }
    }

    void SwitchWeapon(int index)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == index)
            {
                weapons[i].gameObject.SetActive(true);
                weapons[i].reloading = false;
            }
            else
            {
                weapons[i].gameObject.SetActive(false);
            }
        }
    }
}

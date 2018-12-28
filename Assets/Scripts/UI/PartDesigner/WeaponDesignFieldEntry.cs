using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameConstructs;

public class WeaponDesignFieldEntry : PartDesignFieldEntry {
    private Weapon weapon;
    public Dropdown weaponTypeDropdown;
    public Dropdown turretTypeDropdown;
    public Slider caliberSlider;
    public Text caliberText;
    public Slider reloadSlider;
    public Text reloadText;

    public override void Initialize(Part p) {
        weapon = new Weapon(p);
        UpdateWeaponCaliber();
        UpdateWeaponReload();
        UpdateWeaponTurret();
        UpdateWeaponType();
    }

    public override PartType GetPartType() {
        return PartType.Weapon;
    }

    public override Part GetPart() {
        return weapon;
    }
    protected override void UpdateStrings() {
        base.UpdateStrings();
        caliberText.text = weapon.Caliber.ToString() + "mm";
        reloadText.text = weapon.ReloadTime.ToString() + "s";
    }

    public override void SetPart() {
        SetWeaponCaliber();
        SetWeaponReload();
        SetWeaponTurret();
        SetWeaponType();
        UpdateStrings();
    }

    public override void Clear() {
        caliberSlider.value = caliberSlider.minValue;
        reloadSlider.value = reloadSlider.minValue;
        weaponTypeDropdown.value = 0;
        turretTypeDropdown.value = 0;
        UpdateStrings();
    }


    public void UpdateWeaponType() {
        switch (weaponTypeDropdown.value) {
            case 0:
                weapon.weaponType = WeaponType.laser;
                break;
            case 1:
                weapon.weaponType = WeaponType.railgun;
                break;
            default:
                Debug.Log("Fell through on weapon type choice ");
                break;
        }
        UpdateStrings();
    }

    public void SetWeaponType() {
        switch (weapon.weaponType) {
            case WeaponType.laser:
                weaponTypeDropdown.value = 0;
                break;
            case WeaponType.railgun:
                weaponTypeDropdown.value = 1;
                break;
            default:
                Debug.Log("Fell through on weapon type set ");
                break;
        }
    }

    public void UpdateWeaponTurret() {
        switch (turretTypeDropdown.value) {
            case 0:
                weapon.SetTurrets(false, 1);
                break;
            case 1:
                weapon.SetTurrets(true, 1);
                break;
            case 2:
                weapon.SetTurrets(true, 2);
                break;
            case 3:
                weapon.SetTurrets(true, 3);
                break;
            case 4:
                weapon.SetTurrets(true, 4);
                break;
            default:
                Debug.Log("Fell through on turret type choice ");
                break;
        }
        UpdateStrings();
    }

    public void SetWeaponTurret() {
        if(weapon.Turreted == false) {
            turretTypeDropdown.value = 0;
        } else {
            turretTypeDropdown.value = weapon.TurretNumber + 1;
        }
    }

    public void UpdateWeaponCaliber() {
        weapon.Caliber = Mathf.FloorToInt(caliberSlider.value) * 4;
        UpdateStrings();
    }

    public void SetWeaponCaliber() {
        caliberSlider.value = weapon.Caliber;
    }

    public void UpdateWeaponReload() {
        weapon.ReloadTime = reloadSlider.value;
        UpdateStrings();
    }

    public void SetWeaponReload() {
        reloadSlider.value = weapon.ReloadTime;
    }

}

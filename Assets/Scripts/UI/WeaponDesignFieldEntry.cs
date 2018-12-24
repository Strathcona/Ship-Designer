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

    public override void Initialize() {
        weapon = new Weapon();
        UpdateWeaponCaliber();
        UpdateWeaponReload();
        UpdateWeaponTurret();
        UpdateWeaponType();
    }

    public override Part GetPart() {
        return weapon;
    }
    protected override void UpdateStrings() {
        base.UpdateStrings();
        caliberText.text = weapon.Caliber.ToString() + "mm";
        reloadText.text = weapon.ReloadTime.ToString() + "s";
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

    public void UpdateWeaponCaliber() {
        weapon.Caliber = Mathf.FloorToInt(caliberSlider.value) * 4;
        UpdateStrings();
    }

    public void UpdateWeaponReload() {
        weapon.ReloadTime = reloadSlider.value;
        UpdateStrings();
    }

}

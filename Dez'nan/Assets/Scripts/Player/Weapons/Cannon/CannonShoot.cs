using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShoot : WeaponShoot
{
    void Update()
    {
        // TODO Kontrola, jestli Hráč stiskl tlačítko na výstřel, může vystřelit, není aktivní menu, stojí (zhruba) na místě a má dostatek coinů pro postavení Věže
        if(Input.GetKeyDown(shootKey) && readyToShoot && !BuildMenu.IsActive &&
           !WaveManager.endscreenIsActive && playerRigidbody.velocity.magnitude < 1 
           && PlayerInterface.Coins >= BuildMenu.SelectedTowerPrice)
        {
            Shoot();
        }
    }
}

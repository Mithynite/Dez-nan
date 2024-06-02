using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShoot : WeaponShoot
{
    void Update()
    {
        if(Input.GetKeyDown(shootKey) && readyToShoot && !BuildMenu.IsActive &&
           !WaveManager.endscreenIsActive && playerRigidbody.velocity.magnitude < 1 
           && PlayerInterface.Coins >= BuildMenu.SelectedTowerPrice)
        {
            Shoot();
        }
    }
}

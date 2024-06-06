using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Transform cam;
    [SerializeField] protected Transform shootPoint; // TODO Pozice odkud bude projektil létat
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected GameObject player;
    protected Rigidbody playerRigidbody;

    [Header("Settings")]
    [SerializeField] protected float cooldown;

    [Header("Shooting")]
    [SerializeField] protected KeyCode shootKey = KeyCode.Mouse0;
    [SerializeField] protected float shootForce;
    [SerializeField] protected float shootUpwardForce;

    protected bool readyToShoot;
    
    void Start()
    {
        playerRigidbody = player.GetComponent<Rigidbody>();
        readyToShoot = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(shootKey) && readyToShoot && !BuildMenu.IsActive && !WaveManager.endscreenIsActive && playerRigidbody.velocity.magnitude < 1)
        {
            Shoot();
        }
    }

    protected void Shoot()
    {
        readyToShoot = false;

        GameObject projectile = Instantiate(bulletPrefab, shootPoint.position, cam.rotation); // TODO Tvorba projektilu
        
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        Vector3 forceDirection = cam.transform.forward; // TODO Směr, kudy má projektil letět

        RaycastHit hit;

        if(Physics.Raycast(cam.position, cam.forward, out hit, 100f))
        {
            forceDirection = (hit.point - shootPoint.position).normalized;
        }

        Vector3 forceToAdd = forceDirection * shootForce + transform.up * shootUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse); // TODO Udělení příslušné síly projektilu

        Invoke(nameof(ResetShooting), cooldown); // TODO Vyvolání funkce pro obnovení možnosti vyetřelit po uplynutí cooldownu
    }

    protected void ResetShooting()
    {
        readyToShoot = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Transform cam;
    [SerializeField] protected Transform shootPoint;
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

        // instantiate object to shoot
        GameObject projectile = Instantiate(bulletPrefab, shootPoint.position, cam.rotation);

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if(Physics.Raycast(cam.position, cam.forward, out hit, 100f))
        {
            forceDirection = (hit.point - shootPoint.position).normalized;
        }

        // add force
        Vector3 forceToAdd = forceDirection * shootForce + transform.up * shootUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        // implement cooldown
        Invoke(nameof(ResetShooting), cooldown);
    }

    protected void ResetShooting()
    {
        readyToShoot = true;
    }
    
    
}

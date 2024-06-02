using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehavior : MissileBehavior
{
    private CrossbowShoot crossbowShoot;

void Start()
{
    crossbowShoot = FindObjectOfType<CrossbowShoot>();
    damage = crossbowShoot.damage;
}
void OnCollisionEnter(Collision coll)
{
        GameObject other = coll.gameObject;

        if (other.CompareTag(tag))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}

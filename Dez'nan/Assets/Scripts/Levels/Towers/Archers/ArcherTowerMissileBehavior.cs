using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTowerMissileBehavior : MissileBehavior
{
    private ArcherTower at; // TODO reference na ArcherTower script
    private Vector3 targetPosition; // TODO cíl (nepřítel)
    private float missileSpeed; // TODO rychlost prefabu
    
// TODO Nastavení cílové pozice prefabu 
 public void SetTarget(Vector3 target, float speed)
    {
        targetPosition = target;
        missileSpeed = speed;
    }
void Start()
{
    // TODO Nastavení příslušných atributů z dané věže ("ArcherTower")
    at = FindObjectOfType<ArcherTower>();
    damage = at.damage;
    speed = at.missileSpeed;

        if (targetPosition == Vector3.zero)
        {
            Debug.LogError("Target position is not set!");
            return;
        }

        // TODO Výpočet, kterým směrem musí prefab letět
        Vector3 direction = (targetPosition - transform.position).normalized;

        // TODO Aplikování rychlosti na prefab
        GetComponent<Rigidbody>().velocity = direction * missileSpeed;
}

// TODO Pokud je tag kolidujícího objektu roven cílovému tagu, tak se provede akce ubrání života Nepříteli
void OnCollisionEnter(Collision coll)
{
    if(coll.gameObject.tag.Equals(tag)){
        coll.gameObject.GetComponent<Enemy>().TakeDamage(damage);
    } 
    Destroy(gameObject);
}

}





using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTowerMissileBehavior : MissileBehavior
{
    private ArcherTower at; //reference na ArcherTower script
    private Vector3 targetPosition; // cíl (nepřítel)
    private float missileSpeed; // rychlost prefabu

 public void SetTarget(Vector3 target, float speed)
    {
        targetPosition = target;
        missileSpeed = speed;
    }
void Start()
{
    at = FindObjectOfType<ArcherTower>();
    damage = at.damage;
    speed = at.missileSpeed;

        if (targetPosition == Vector3.zero)
        {
            Debug.LogError("Target position is not set!");
            return;
        }

        // Výpočet, kterým směrem musí prefab letět
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Aplikování rychlosti na prefab
        GetComponent<Rigidbody>().velocity = direction * missileSpeed;
}

void OnCollisionEnter(Collision coll)
{
    if(coll.gameObject.tag == tag){
        coll.gameObject.GetComponent<Enemy>().TakeDamage(damage);
    }
            Destroy(gameObject);
}

}





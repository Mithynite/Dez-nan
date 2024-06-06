using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTowerMissileBehavior : MissileBehavior
{
    private MageTower mageTower; // TODO reference na ArcherTower script
    private Vector3 targetPosition; // TODO cíl (nepřítel)
    private float missileSpeed; // TODO rychlost prefabu
    
    public void SetTarget(Vector3 target, float speed)
    {
        targetPosition = target;
        missileSpeed = speed;
    }
    void Start()
    {
        // TODO Nastavení příslušných atributů z dané věže ("MageTower")
        mageTower = FindObjectOfType<MageTower>();
        damage = mageTower.damage;
        speed = mageTower.missileSpeed;

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

    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag.Equals(tag)){
            coll.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        } 
        Debug.Log("shoot " + coll.gameObject.tag);
        
        Destroy(gameObject);
    }
}

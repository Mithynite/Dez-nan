using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MortarTower : Tower
{
private GameObject[] enemies;
private GameObject[] targets;
private IEnumerable<GameObject> UpdateForTargets()
{
    enemies = GameObject.FindGameObjectsWithTag(enemyTag); // Vytvoření pole nepřátel, kteří jsou vyhledáni podle Tagu
    
    if(enemies.Length > 0){
        foreach (GameObject enemy in enemies) //Procházení pole nepřátel
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); // Vzdálenost věže od nepřítele
            if(distanceToEnemy <= range)
            {   
                yield return enemy;
            }
        }
    }
}

private void SetTargets()
{
    targets = UpdateForTargets().ToArray();
}

void Start()
{
    InvokeRepeating("SetTargets", 1f, 4f);
    Shoot();
}
public override void Shoot()
{
    if(targets != null && targets.Length > 0)
    {
        foreach (var enemy in targets)
        {
            if(enemy != null)
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }
}
    void Update()
    {
            CallShooting();
    }
}

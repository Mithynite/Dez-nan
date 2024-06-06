using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MortarTower : Tower
{
private GameObject[] enemies; // TODO Pole pro uložení nepřatel
private GameObject[] targets; // TODO Pole pro uložení cílů Mortaru

// TODO Předělaná metoda pro hledání cílů věže, tak aby mohla najít více nepřátel vrátit
private IEnumerable<GameObject> UpdateForTargets()
{
    enemies = GameObject.FindGameObjectsWithTag(enemyTag); // TODO Vytvoření pole nepřátel, kteří jsou vyhledáni podle Tagu
    
    if(enemies.Length > 0){
        foreach (GameObject enemy in enemies) // TODO Procházení pole nepřátel
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); // TODO Vzdálenost věže od nepřítele
            if(distanceToEnemy <= range) // TODO Kontrola, jestli je nepřítel v rámci dostřelu věže (jeho vzdálenosti od věže)
            {   
                yield return enemy; // TODO Vrácení najitého nepřítele
            }
        }
    }
}

// TODO Uložení nepřátel do pole
private void SetTargets()
{
    targets = UpdateForTargets().ToArray(); // TODO Převedení vrácených cílů do pole
}

void Start()
{
    InvokeRepeating("SetTargets", 2f, 4f); // TODO Opakování metody "SetTargets()" po 2s a následně každé 4s
    Shoot();
}

// TODO Přepsaná metoda Shoot(), aby dokázala dát poškození rovnoměrně každému najitému nepříteli
public override void Shoot()
{
    if(targets != null && targets.Length > 0)
    {
        foreach (var enemy in targets)
        {
            if(enemy != null)
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage); // TODO Použití metody "TakeDamage(...)" ze třídy Enemy pro udělení poškození nepříteli 
            }
        }
    }
}
    void Update()
    {
            CallShooting();
    }
}

using UnityEngine;

public class ArcherTower : Tower
{
    void Update()
    {
        if(target == null){
            return;
        }
        TurnAgainstYourEnemy();
        CallShooting();
    }
    public override void SetTargetAndSpeedOfProjectile(Vector3 interceptPoint)
    {
        // TODO Vytvoření Prefabu projektilu
        GameObject missile = Instantiate(missilePrefab, shootPoint.position, Quaternion.identity);
    
        // TODO Nastavení cílu a rychlosti projektilu
        missile.GetComponent<ArcherTowerMissileBehavior>().SetTarget(interceptPoint, missileSpeed);
    }
    void Start(){
        // TODO Volání metody "UpdateTarget", poprvé po 5s, poté vždy po 3s, aby to nebylo tak náročné na výpočet
         InvokeRepeating("UpdateTarget", 5f, 3f);
    }
}

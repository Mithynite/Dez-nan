using UnityEngine;

public class MageTower : Tower
{
    void Update()
    {
        if(target == null){
            return;
        }
        TurnAgainstYourEnemy();
        CallShooting();
    }
    
    // TODO Tvorba prefabu projektilu
    public override void SetTargetAndSpeedOfProjectile(Vector3 interceptPoint)
    {
        GameObject missile = Instantiate(missilePrefab, shootPoint.position, Quaternion.identity);
        
        missile.GetComponent<MageTowerMissileBehavior>().SetTarget(interceptPoint, missileSpeed);
    }
    void Start(){
        // TODO Volání metody "UpdateTarget", poprvé po 4s, poté vždy po 2s
        InvokeRepeating("UpdateTarget", 4f, 2f);
    }
}

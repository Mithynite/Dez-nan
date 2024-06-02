using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    
    public int AllEnemies { get; private set; }
    public int DelayBetweenSpawn; //TODO Rozptyl mezi spawnováním jednotlivých nepřátel
    
    [Header("References")]
    [SerializeField] private SpawnedEnemy[] enemies; //TODO Pole nepřátel, kteří se budou spawnovat

    private List<int> indexesOfAliveEnemies = new List<int>();

    //TODO Sečtení všech množství nepřátel a uložení této hodnoty do proměnné "AllEnemies"
    public void SumUpAllEnemies(){
        for(int a = 0; a < enemies.Length; a++){
            AllEnemies += enemies[a].amount;
        }
    }
    //TODO Generování náhodného typu nepřítele ze třídy "SpawnedEnemy" a následné snížení hodnoty množství "Amount"
    public GameObject GenerateRandomEnemy() //Vector3 position, Quaternion rotation
    {
        AnyEnemyRemaining();

        if(indexesOfAliveEnemies.Count > 0)
        {
            int randomIndexFromAlive = Random.Range(0, indexesOfAliveEnemies.Count-1);
            int randomEnemyIndex = indexesOfAliveEnemies[randomIndexFromAlive];
           
            GameObject newEnemy = enemies[randomEnemyIndex].prefab;
           
            enemies[randomEnemyIndex].amount--;
           
            return newEnemy;
        } 
        return null;
    }
    //TODO Hledání nepřátel, kteří se mají spawnout a uložení jeho indexu, aby jsem ho mohl později zavolat
    private void AnyEnemyRemaining()
    {
        for (int a = 0; a < enemies.Length; a++)
        {
            if (enemies[a].amount > 0)
            {
                indexesOfAliveEnemies.Add(a); // přidání indexu nového nepřítele
            }else
            {
                indexesOfAliveEnemies.Remove(a); // odebrání zbytečného indexu nepřítele
            }
        }
}
    
    public bool AreAllEnemiesDead()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length > 0)
        {
            return false;
        }
        return true;
    }
}

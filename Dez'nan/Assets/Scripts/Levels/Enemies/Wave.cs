using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    
    public int AllEnemies { get; private set; }
    public int DelayBetweenSpawn; // TODO Rozptyl mezi spawnováním jednotlivých nepřátel
    
    [Header("References")]
    [SerializeField] private SpawnedEnemy[] enemies; // TODO Pole nepřátel, kteří se budou spawnovat

    private List<int> indexesOfAliveEnemies = new List<int>(); // TODO List pro ukádání indexů nepřátel

    // TODO Sečtení všech množství nepřátel a uložení této hodnoty do proměnné "AllEnemies"
    public void SumUpAllEnemies(){
        for(int a = 0; a < enemies.Length; a++){
            AllEnemies += enemies[a].amount;
        }
    }
    // TODO Generování náhodného typu nepřítele ze třídy "SpawnedEnemy" a následné snížení hodnoty množství "Amount"
    public GameObject GenerateRandomEnemy()
    {
        AnyEnemyRemaining();

        if(indexesOfAliveEnemies.Count > 0)
        {
            int randomIndexFromAlive = Random.Range(0, indexesOfAliveEnemies.Count-1); // TODO Vybrání náhodného indexu pro List "indexesOfAliveEnemies"
            int randomEnemyIndex = indexesOfAliveEnemies[randomIndexFromAlive]; // TODO Vybrání náhodného indexu nepřítele pro pole "enemies[]"
           
            GameObject newEnemy = enemies[randomEnemyIndex].prefab; // TODO Výběr náhodného nepřítele z pole 
           
            enemies[randomEnemyIndex].amount--;
           
            return newEnemy;
        } 
        return null;
    }
    // TODO Hledání nepřátel, kteří se mají spawnout a uložení jeho indexu, aby jsem ho mohl později zavolat pro snadnější přístup k nim
    private void AnyEnemyRemaining()
    {
        for (int a = 0; a < enemies.Length; a++)
        {
            if (enemies[a].amount > 0)
            {
                indexesOfAliveEnemies.Add(a); // TODO přidání indexu nového nepřítele
            }else
            {
                indexesOfAliveEnemies.Remove(a); // TODO odebrání zbytečného indexu nepřítele
            }
        }
}
    
    // TODO Kontrola, jestli jsou všichni nepřátelé zabiti
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

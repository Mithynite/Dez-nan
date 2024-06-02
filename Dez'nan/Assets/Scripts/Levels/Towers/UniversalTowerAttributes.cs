using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UniversalTowerAttributes", menuName = "ScriptableObjects/UniversalTowerAttributes", order = 2)]
public class UniversalTowerAttributes : ScriptableObject
{
    public TowerAttributes[] towerAttributesArray;

[System.Serializable]
    public class TowerAttributes
    {
        public string name;
        [Header("Attributes")]
        public int cost;
    }

    public void ResetValues()
    {
        towerAttributesArray[0].cost = 15;
        towerAttributesArray[1].cost = 25;
        towerAttributesArray[2].cost = 20;
        towerAttributesArray[3].cost = 8;
    }

    public void ChangeTowerAttributesByValue(int value)
    {
        foreach (var tower in towerAttributesArray)
        {
            tower.cost += value;
        }
    } 
}

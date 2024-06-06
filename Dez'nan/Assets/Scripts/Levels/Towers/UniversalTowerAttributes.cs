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

    // TODO Obnovení hodnot (cen) věží na původní
    public void ResetValues()
    {
        towerAttributesArray[0].cost = 20;
        towerAttributesArray[1].cost = 35;
        towerAttributesArray[2].cost = 30;
        towerAttributesArray[3].cost = 15;
    }

    // TODO Úprava cen věží o určitou hodnotu
    public void ChangeTowerAttributesByValue(int value)
    {
        foreach (var tower in towerAttributesArray)
        {
            tower.cost += value;
        }
    } 
}

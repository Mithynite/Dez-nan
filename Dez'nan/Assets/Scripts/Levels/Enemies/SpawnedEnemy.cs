using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]

public class SpawnedEnemy
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private string _name;
    public int amount;

    public string name => _name;

    public GameObject prefab => _prefab;
}

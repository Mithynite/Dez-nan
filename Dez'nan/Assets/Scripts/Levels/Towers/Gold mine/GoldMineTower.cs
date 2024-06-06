using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMineTower : Tower
{
    [SerializeField] private int coinIncrease;
    [SerializeField] private PlayerBehaviorVariables playerBehaviorVariables;
    
    public override void Shoot()
    {
        PlayerInterface.AddCoins(coinIncrease);
    }
    void Update()
    {
        CallShooting();
    }
}

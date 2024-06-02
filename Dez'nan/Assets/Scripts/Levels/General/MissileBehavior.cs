using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehavior : MonoBehaviour
{
   [SerializeField] protected float life;
    protected int damage;
    protected int speed;
    [SerializeField] protected string tag;
void Awake()
{
    Destroy(gameObject, life);
}



}

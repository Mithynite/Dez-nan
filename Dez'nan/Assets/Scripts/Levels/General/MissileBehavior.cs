using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehavior : MonoBehaviour
{
   [SerializeField] protected float life; // TODO Jak dlouho bude trvat, než se projektil zničí
    protected int damage;
    protected int speed;
    [SerializeField] protected string tag; // TODO Proměnná pro uložení tagu objektu, který je cílem projektilu a kterému tedy má něco udělat (např. Nepříteli)
void Awake()
{
    Destroy(gameObject, life); // TODO Zničení tohoto objektu, po daném čase
}
}

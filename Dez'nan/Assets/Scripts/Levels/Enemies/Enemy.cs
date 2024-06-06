using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int maxHealth;
    private float health;
    [SerializeField] private int damage;
    [SerializeField] private int takenLifes = 1;
    [SerializeField] private int defensePercentage = 5;
    [SerializeField] private int coinsEarnedAfterDeath; // TODO Proměnná počet coinů, který dostane hráč, když tohoto nepřítele zabije

    [Header("References")]
    private NavMeshAgent agent;
    private GameObject target; // TODO Proměnná pro uložení výsledné cílové destinace NavMesh Agenta (nepřítele)
    private GameObject[] targets; // TODO Pole možností cílových destinací
    
    private EnemyHealthBar hp; // TODO Reference na healthbar
    private static GameObject player; 
    private PlayerInterface playerInterface; // TODO Reference hráčovo rozhraní kvůli odečtení života, když se tento objekt (nepřítel) dostane až do cílové destinace

   
void Start()
{
    targets = GameObject.FindGameObjectsWithTag("Target"); // TODO Vyhledání všech objektů, co mají tag "Target"
    target = ChooseTheTarget(); // TODO Přidělení cílové destinace
    agent = gameObject.GetComponent<NavMeshAgent>();
    agent.SetDestination(target.transform.position); // TODO Nastavení cílové destinace tomuto NavMesh Agentovi

    health = maxHealth;
    hp = GetComponentInChildren<EnemyHealthBar>();
    player = GameObject.FindGameObjectWithTag("Player");
    playerInterface = player.GetComponent<PlayerInterface>();
}
void Update()
{
    CheckIfIDie();
    IsDestinationReached();
}
    public void TakeDamage(int damage)
    {
        health -= (damage - damage * (defensePercentage/100)); // TODO Odečtení života - obrana
        hp.UpdateHealthBar(health, maxHealth); // TODO Aktualizování healthbaru
    }
    private bool IsDead()
    {
        return health <= 0;
    }
    private void CheckIfIDie()
    {
        if(IsDead())
        {
            PlayerInterface.AddCoins(coinsEarnedAfterDeath); // TODO Přidělení coinů hráči
            Destroy(gameObject);
        }
    }
    
    // TODO Kontrola, jestli už NavMesh Agent dorazil do cíle podle jeho vzdálenosti od něj
    // TODO Dal jsem vzdálenosti nějakou preventivní toleranci (2f)
    private void IsDestinationReached(){
        if(Vector3.Distance(transform.position, target.transform.position) < 2f)
        {
            playerInterface.TakeDamage(takenLifes); // TODO Ubrání životů Hráčovi
            Destroy(gameObject);
        }   
    }

    // TODO Výběr náhodné cílové destinace z pole těch, které byly nalezeny a jeho vrácení
    private GameObject ChooseTheTarget()
    {
        if (targets.Length > 1)
        {
            return targets[Random.Range(0, targets.Length-1)];
        }
        // TODO Pokud je velikost pole == 1, tak se použije první (jediný) prvek v poli jako cílová destinace 
        return targets[0];
    }
}

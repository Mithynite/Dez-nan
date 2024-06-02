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
    [SerializeField] private int coinsEarnedAfterDeath = 0;

    [Header("References")]
    private NavMeshAgent agent;
    private GameObject target;
    private GameObject[] targets;
    
    private EnemyHealthBar hp;
    private static GameObject player;
    private PlayerInterface playerInterface;


void Start()
{
    //target = GameObject.FindGameObjectWithTag("Target");
    targets = GameObject.FindGameObjectsWithTag("Target");
    target = ChooseTheTarget();
    agent = gameObject.GetComponent<NavMeshAgent>();
    agent.SetDestination(target.transform.position);

    health = maxHealth;
    hp = GetComponentInChildren<EnemyHealthBar>();
    player = GameObject.FindGameObjectWithTag("Player");
    playerInterface = player.GetComponent<PlayerInterface>();
}
void Update()
{
    Die();
    IsDestinationReached();
}
    public void TakeDamage(int damage)
    {
        health -= (damage - damage * (defensePercentage/100));
        hp.UpdateHealthBar(health, maxHealth);
    }
    private bool IsDead()
    {
        return health <= 0;
    }
    private void Die()
    {
        if(IsDead())
        {
            PlayerInterface.AddCoins(coinsEarnedAfterDeath);
            Destroy(gameObject);
        }
    }
    private void IsDestinationReached(){
        if(Vector3.Distance(transform.position, target.transform.position) < 2f)
        {
            playerInterface.TakeDamage(takenLifes);
            Destroy(gameObject);
        }   
    }

    private GameObject ChooseTheTarget()
    {
        if (targets.Length > 1)
        {
            return targets[Random.Range(0, targets.Length-1)];
        }
        return targets[0];
    }
}

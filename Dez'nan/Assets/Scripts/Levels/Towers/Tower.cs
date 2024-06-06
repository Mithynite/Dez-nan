using UnityEngine;

public abstract class Tower : MonoBehaviour
{
[Header("Variables")]
    protected int cost;
    [SerializeField] protected int range;
    [SerializeField] protected float turnSpeed;

[Header("Fire Attributes")]
    [SerializeField] protected float repeationSpeed;
    protected float cooldown;
    [SerializeField] protected int _damage;
    public int damage => _damage;
    [SerializeField] protected int _missileSpeed;

    public int missileSpeed => _missileSpeed;

[Header("References")]
    [SerializeField] protected UniversalTowerAttributes towerAttributes;
    [SerializeField] protected Transform shootPoint;
    [SerializeField] protected string enemyTag;
    [SerializeField] protected GameObject missilePrefab;
    [SerializeField] protected Transform objectToRotate; // TODO Objekt, který bude rotovat směrem k nepříteli
    
    protected Transform target;

    protected bool canBeBought(int coins)
    {
        return coins >= cost;
    }
    
// TODO Aktualizování aktuálního cíle, po kterém bude věž střílet    
protected void UpdateTarget()
{
    GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); //TODO Vytvoření pole nepřátel, kteří jsou vyhledáni podle Tagu
    float shortestDistanceToEnemy = Mathf.Infinity; // TODO Na začátku nevím, kde jsou nepřátelé, takže podobně jako u algoritmů pro grafu nastavím nejbližšímu "vrcholu" (nepříteli) nekonečnou vzdálenost od věže
    GameObject theEnemyICurrentlySee = null;

    //TODO Procházení pole nepřátel a hledání aktuálního nejbližšího nepřítele
    foreach (GameObject enemy in enemies)
    {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); //TODO Vzdálenost věže od nepřítele
            if(distanceToEnemy < shortestDistanceToEnemy)
            {
                    shortestDistanceToEnemy = distanceToEnemy; // TODO Uložím si aktuální nejbližší vzdálenost k nepříteli
                    theEnemyICurrentlySee = enemy; // TODO Aktuální zaměřený nepřítel
            }
    }
    // TODO Přiřazení pozice nejbližsího nepřítele do targetu
    if (theEnemyICurrentlySee != null && shortestDistanceToEnemy <= range)
    {
        target = theEnemyICurrentlySee.transform;
        return;
    }
    target = null;
}

// TODO Nakreslení "Koule", která symbolizuje dostřel věže 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, range);
    }

//TODO Aplikování atributů ze atributů ScriptableObject
    protected void ApplyAttributes()
    {
        int index = FindProperTowerIndexFromTheArray();
        if(index > 0)
        {
            cost = towerAttributes.towerAttributesArray[index].cost;
        }else
        {
           Debug.Log($"Element with the tag: {gameObject.tag} was not found in the array!"); 
        }
    }
    
    // TODO Najití správného indexu věže v poli věží, které se nachází v Assetu "UniversalTowerAttributes" 
    private int FindProperTowerIndexFromTheArray()
    {
        for(int a = 0; a < towerAttributes.towerAttributesArray.Length; a++)
        {
            if(towerAttributes.towerAttributesArray[a].name == gameObject.tag)
            {
                return a;
            }
        }
        return -1;
    }

void Start(){
    ApplyAttributes();
    cooldown = 10f/repeationSpeed;
}

// TODO Odpočítáváni cooldownu, než bude věž moci opět vystřelit
protected void CallShooting()
{
    if(cooldown <= 0)
    {
        Shoot();
        cooldown = 10f/repeationSpeed; 
    }
    cooldown -= Time.deltaTime;
}

// TODO Vzor pro metodu na tvorbu a nastavení vystřeleného projektilu věže
public virtual void SetTargetAndSpeedOfProjectile(Vector3 interceptPoint)
{
    
}

// TODO Střílení věže
public virtual void Shoot()
{
    // TODO Použití metody pro výpočet "bodu protnutí"
    Vector3 predictedTargetPosition = PredictTargetPosition(target.position, target.GetComponent<Rigidbody>().velocity);

    // TODO Použití metody pro výpočet "bodu protnutí"
    Vector3 interceptPoint = CalculateInterceptPoint(transform.position, Vector3.zero, predictedTargetPosition, target.GetComponent<Rigidbody>().velocity, missileSpeed);
    
    SetTargetAndSpeedOfProjectile(interceptPoint);
}

// TODO Část kódu, která se nachází pod tímto komentářem byla trochu inspirována zdrojem ChatGPT, tím myšlen výpočet "bodu protnutí"

// TODO Výpočet "bodu protnutí" mezi nepřítelem a projektilem
public Vector3 CalculateInterceptPoint(Vector3 shooterPosition, Vector3 shooterVelocity, Vector3 targetPosition, Vector3 targetVelocity, float projectileSpeed)
{
    // TODO Výpočet vzdálenosti mezi pozicemi objektů (cíle a věže)
    Vector3 relativePosition = targetPosition - shooterPosition;
    // TODO Výpočet rozdílu rychlostí mezi objekty (cíle a věže)
    Vector3 relativeVelocity = targetVelocity - shooterVelocity;

    // TODO Vypočtení času, který zbývá do kolize
    float timeToIntercept = relativePosition.magnitude / (projectileSpeed + relativeVelocity.magnitude);

    // TODO Vypočtení "bodu protnutí"
    return targetPosition + (targetVelocity * timeToIntercept);
}
protected Vector3 PredictTargetPosition(Vector3 targetPosition, Vector3 targetVelocity)
{
    // TODO Za předpokladu, že se cíl pohybuje konstantně se dá předpokládat, kde zhruba za nějaký čas bude
    return targetPosition + targetVelocity * Time.deltaTime;
}

// TODO Otáčení (části) věže ve směru nepřítele
protected void TurnAgainstYourEnemy(){
    Vector3 direction = target.position - transform.position; // TODO Výpočet směru odečtením vektorů
    Quaternion lookRotation = Quaternion.LookRotation(direction); // TODO Výpočet směru odečtením vektorů
    Vector3 rotation = Quaternion.Lerp(objectToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
    objectToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f); // TODO nastavení rotace objektu
}
}






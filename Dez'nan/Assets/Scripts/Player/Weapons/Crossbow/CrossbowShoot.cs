using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowShoot : WeaponShoot
{
    [SerializeField] public int damage; 
 /*   [Header("Behavior")]
#region Behavior
    public float timeBetweenShots;
    public float spread;
    public float range;
    public float reloadTime;
    
    public float missileSpeed;
    public int missilesPerTap;
    public bool allowFullAuto;
    [HideInInspector] public bool shooting;
#endregion*/

/*[Header("Effectiveness")]
#region Effectiveness
    [SerializeField] public int damage; 
    public int damage => _damage;
    
    public int magazineSize = 5;
    int ammoLeft;
    int ammoShot;
    bool readyToShoot;
    bool reloading;
    
    public float shotPower = 2f;
    private  RaycastHit hit;
    [SerializeField] KeyCode shootKey = KeyCode.Mouse0;
#endregion*/

/*[Header("References")]
#region References
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform shootPoint;
    [SerializeField] LayerMask whatIsEnemy;
    private Camera camera;    
    [SerializeField] private GameObject player;
    private Rigidbody playerRigidbody;
    
#endregion*/
        
/*void Start()
        {
           ammoLeft = magazineSize;
           readyToShoot = true; 
           playerRigidbody = player.GetComponent<Rigidbody>();
        }*/

        /*void Update()
        {
            if(Input.GetKeyDown(shootKey) )
            {
                Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * shotPower*100);
            }
        }*/
    /*private void MyInput()
{
    if (allowFullAuto)
    {
        shooting = Input.GetKey(shootKey);
    }
    else
    {
        shooting = Input.GetKeyDown(shootKey);
    }

    if(Input.GetKey(KeyCode.R) && ammoLeft < magazineSize && !reloading)
    {
        Reload();
    }

    if (readyToShoot && shooting && !reloading && ammoLeft > 0)
    {
        Shoot();
    }
}
private void Shoot()
{
    readyToShoot = false;

    //Najití přesného místa, kam má kulka letět 
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //MID obrazovky (crosshair)

    //Rozptyl = Spread
    float x = Random.Range(-spread, spread);
    float y = Random.Range(-spread, spread);

    Vector3 targetPoint;
    if(Physics.Raycast(ray, out hit))
    {
        targetPoint = hit.point;
    }else
    {
        targetPoint = ray.GetPoint(75);
    }
    //Výpočet směru střelby bez rozptylu
    Vector3 directionWithoutSpread = targetPoint - shootPoint.position;
    //Výpočet směru střelby s rozptylem
    Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x,y,0);
    //Kulka
    GameObject currentBullet = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);
    currentBullet.transform.forward = directionWithSpread.normalized;
    //Síla
    currentBullet.GetComponent<Rigidbody>().AddForce(missileSpeed * directionWithoutSpread.normalized, ForceMode.Impulse); //directionWithSpread
    currentBullet.transform.Rotate(90f,0f, 0f);

    ammoLeft--;
    ammoShot--;
    Invoke("ResetShot", timeBetweenShots);
    if(ammoShot > 0 && ammoLeft > 0)
    {
        ammoShot = missilesPerTap;
        Invoke("Shoot", timeBetweenShots);
    }
}
private void ResetShot()
{
    readyToShoot = true;
}

private void Reload()
{
    reloading = true;
    Invoke("ReloadingFinished", reloadTime);
}

private void ReloadingFinished()
{
    ammoLeft = magazineSize;
    reloading = false;
}*/
}

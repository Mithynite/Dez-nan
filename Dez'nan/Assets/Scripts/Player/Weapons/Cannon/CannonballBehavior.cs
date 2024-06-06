using UnityEngine;

public class CannonballBehavior : MissileBehavior
{
    private GameObject selectedTowerPrefab; // TODO Vybraný prefab věže z Build Menu 
    [SerializeField] private PlayerBehaviorVariables playerBehaviorVariables;
    [SerializeField] private float towerBuildOffset = 1f;
    void Start()
    {
        selectedTowerPrefab = BuildMenu.SelectedTowergGameObject;
    }
    void OnCollisionEnter(Collision coll)
    {
        GameObject other = coll.gameObject;

        if (other.CompareTag(tag) && selectedTowerPrefab != null)
        {
            Instantiate(selectedTowerPrefab, new Vector3(transform.position.x, transform.position.y + towerBuildOffset, transform.position.z), selectedTowerPrefab.transform.rotation); // TODO Vytvoření objektu věže na místě kolize s jiným objektem (zemí)
            PlayerInterface.TakeCoins(BuildMenu.SelectedTowerPrice); // TODO Odečtení coinů Hráči, jenom pokud se tedy Věž skutečně postaví
        }
        Destroy(gameObject);
    }
}

using UnityEngine;

public class CannonballBehavior : MissileBehavior
{
    private GameObject selectedTowerPrefab;
    void Start()
    {
        selectedTowerPrefab = BuildMenu.SelectedTowergGameObject;
    }
    void OnCollisionEnter(Collision coll)
    {
        GameObject other = coll.gameObject;

        if (other.CompareTag(tag) && selectedTowerPrefab != null)
        {
            Instantiate(selectedTowerPrefab, transform.position, Quaternion.identity);
            PlayerInterface.TakeCoins(BuildMenu.SelectedTowerPrice);
        }
        Destroy(gameObject);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class GateEnterPoint : MonoBehaviour
{
[SerializeField] private GameObject warningText; // TODO Upozornění, že hráč nemá zpřístupněnou danou úroveň
[SerializeField] private int sceneIndex; // TODO Index scény, do které se má přejít
[SerializeField] private int levelIndex; // TODO Číslo úrovně, které slouží k porovnání čísla vyhraných úrovní Hráče 
private void OnTriggerEnter(Collider other)
{
  if (other.CompareTag("Player") && PlayerManager.DifferentLevelWins >= levelIndex) // TODO Kontroluji, jestli tento objekt kolidoval Hráč a zároveň vyhrál dostatek úrovní
  {
    Debug.Log("You entered Level " + (sceneIndex-1));
    SceneManager.LoadScene(sceneIndex);
  }else if(other.CompareTag("Player"))
  {
      warningText.SetActive(true);
  }   
}
private void OnTriggerExit(Collider other){
  	warningText.SetActive(false);
}



}

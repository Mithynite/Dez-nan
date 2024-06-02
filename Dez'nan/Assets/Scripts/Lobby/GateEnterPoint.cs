using UnityEngine;
using UnityEngine.SceneManagement;

public class GateEnterPoint : MonoBehaviour
{
[SerializeField] private GameObject warningText;
[SerializeField] private int sceneIndex;
[SerializeField] private int levelIndex;
private void OnTriggerEnter(Collider other)
{
  if (other.CompareTag("Player") && PlayerManager.DifferentLevelWins >= levelIndex)
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

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehavior : MonoBehaviour
{
    [SerializeField] private GameObject options;
    [SerializeField] private UniversalTowerAttributes towerAttributes;
    [SerializeField] private PlayerBehaviorVariables playerBehaviorVariables;
    public void PlayGame()
    {
	    playerBehaviorVariables.ResetValues();
	    towerAttributes.ResetValues();
		SceneManager.LoadScene(1);
	}
	public void QuitGame()
	{
    Debug.Log("Game is shutting down!");
		Application.Quit();
	}
}

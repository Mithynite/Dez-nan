using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuBehavior : MonoBehaviour
{
    [SerializeField] private GameObject options;
    [SerializeField] private UniversalTowerAttributes towerAttributes;
    [SerializeField] private PlayerBehaviorVariables playerBehaviorVariables;
    
    // TODO Startnutí nové hry --> přechod do scény "Lobby"
    public void PlayGame()
    {
	    playerBehaviorVariables.ResetValues(); // TODO Obnovení Assetu s atributy Hráče (rychlosti, coinů, atd.)
	    towerAttributes.ResetValues(); // TODO Obnovení Assetu s atributy (ceny) jednotlivých věží
		SceneManager.LoadScene(1);
	}
    
    // TODO Ukončení hry
	public void QuitGame()
	{
		Debug.Log("Game is shutting down!");
		Application.Quit();
	}
}

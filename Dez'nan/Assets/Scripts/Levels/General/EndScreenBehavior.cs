using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenBehavior : MonoBehaviour
{
    [SerializeField] private int lobbySceneIndex = 1;
    
    // TODO Metoda, která je přidělena tlačítku, které se objeví společně s finální zprávou po ukončení úrovně a vrátí Hráče zpět do Lobby
    public void BackToLobby()
    {
        Time.timeScale = 1f;
		SceneManager.LoadScene(lobbySceneIndex);
	}
}

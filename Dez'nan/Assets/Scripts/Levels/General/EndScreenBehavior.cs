using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenBehavior : MonoBehaviour
{
    [SerializeField] private int lobbySceneIndex = 1;
    public void BackToLobby()
    {
        Time.timeScale = 1f;
		SceneManager.LoadScene(lobbySceneIndex);
	}
}

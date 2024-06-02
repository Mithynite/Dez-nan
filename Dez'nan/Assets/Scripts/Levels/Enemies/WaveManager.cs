
using System.Collections;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

[Header("Variables")]
    [SerializeField] private float XSpawnRange = 0;
    [SerializeField] private float ZSpawnRange = 0;
    private bool waveIsRunning;
    private int waveCount = 0;
    private bool allWavesAreFinished;
    public static bool endscreenIsActive;

[Header("References")]
    [SerializeField] private PlayerBehaviorVariables variables;
    [SerializeField] private Transform spawnpoint;
    [SerializeField] private Wave[] waves;
    [SerializeField] private TextMeshProUGUI textToPress;
    [SerializeField] private GameObject endscreenPanel;
    [SerializeField] private TextMeshProUGUI endscreenMessage;
    [SerializeField] private static GameObject player;
    private PlayerInterface playerInterface;
void Start()
{
    player = GameObject.FindGameObjectWithTag("Player");
    playerInterface = player.GetComponent<PlayerInterface>();
    endscreenIsActive = false;
    waveIsRunning = false;
    allWavesAreFinished = false; 
    waveCount = 0;
}
    void Update()
    {
        if(waveIsRunning)
        {
            IsTheWaveFinished();
            if(CheckIfPlayerIsDead())
        {
            endscreenMessage.text = "<color=\"red\">You have Lost!</color>";
            AdjustEndscreenMessage();
        }
        }
        StartNewWave();
    }
    private void StartNewWave()
    {
        if(!waveIsRunning && !allWavesAreFinished)
        {
            textToPress.enabled = true;
            if(Input.GetKeyDown(KeyCode.E))
            {
                textToPress.enabled = false;
                waves[waveCount].SumUpAllEnemies();
                StartCoroutine(SpawnTheWave());
            }
        }
    }

    private IEnumerator SpawnTheWave()
    {
        Quaternion rotation = Quaternion.identity;
        Vector3 position;
        waveIsRunning = true;
        for(int i = 0; i < waves[waveCount].AllEnemies; i++)
        {
            float x = spawnpoint.position.x + XSpawnRange;
            float z = spawnpoint.position.z - ZSpawnRange;
            position = new Vector3(Random.Range(x, spawnpoint.position.x), 0.5f, Random.Range(z, spawnpoint.position.z));

            GameObject tmp = waves[waveCount].GenerateRandomEnemy();
            Instantiate(tmp, position, rotation);
            yield return new WaitForSeconds(waves[waveCount].DelayBetweenSpawn);
    } 
}

    private void IsTheWaveFinished()
    {
        if(waves[waveCount].AreAllEnemiesDead())
        {
            waveIsRunning = false;
            if(waveCount < waves.Length-1)
            {
                waveCount++;
            }else
            {
                allWavesAreFinished = true;
                AdjustEndscreenMessage();
                variables.diamonds += 1;
                PlayerManager.IncrementDifferentLevelWins();
            }
        }
    }
    private bool CheckIfPlayerIsDead()
    {
        return playerInterface.Health <= 0;
    }
    private void AdjustEndscreenMessage()
    {
            endscreenPanel.SetActive(true);
            endscreenIsActive = true;
            waveIsRunning = false;
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
    }
}


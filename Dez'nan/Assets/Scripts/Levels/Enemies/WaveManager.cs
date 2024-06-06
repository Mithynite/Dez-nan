
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class WaveManager : MonoBehaviour
{

    [FormerlySerializedAs("promptText")]
    [Header("Variables")] 
    [SerializeField] private KeyCode promptTextKey = KeyCode.E;
    [SerializeField] private float XSpawnRange; // TODO Rozptyl na ose X
    [SerializeField] private float ZSpawnRange; // TODO Rozptyl na ose Z
    private bool waveIsRunning;
    private int waveCount;
    private bool allWavesAreFinished;
    public static bool endscreenIsActive;

[Header("References")]
    [SerializeField] private PlayerBehaviorVariables variables;
    [SerializeField] private Transform spawnpoint; // TODO Objekt, na kterém se budou nepřátelé spawnovat (ale případně se přičtou "ZSpawnRange" a "XSpawnRange")
    [SerializeField] private Wave[] waves; // TODO Pole s Vlnami, které se budou používat, proto je Vlna ("Wave") "[System.Serializable]"
    [SerializeField] private TextMeshProUGUI textToPress; // TODO Reference na text nápovědy
    [SerializeField] private GameObject endscreenPanel; // TODO Reference na celý panel finálové zprávy po ukončení úrovně
    [SerializeField] private TextMeshProUGUI endscreenMessage; // TODO Reference na text finálové zprávy
    private static GameObject player;
    private PlayerInterface playerInterface;
void Start()
{
    player = GameObject.FindGameObjectWithTag("Player");
    playerInterface = player.GetComponent<PlayerInterface>();

    textToPress.text = $"Press <color=\"yellow\">{promptTextKey.ToString()}</color> to start new wave "; // TODO Nastavení textu nápovědy
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
            // TODO Nastavení finálové zprávy při prohře
            endscreenMessage.text = "<color=\"red\">You have Lost!</color>";
            AdjustEndscreenMessageEnvironment();
        }
    }
    StartNewWave();
}

// TODO Startnutí nové vlny
private void StartNewWave()
{
    // TODO Kontroluji zda-li nyní neprobíhá žádná vlna ("waveIsRunning") a zároveň jestli vůbec zbývají nějaké vlny ("allWavesAreFinished")
    if(!waveIsRunning && !allWavesAreFinished)
    {
        textToPress.enabled = true;
        if(Input.GetKeyDown(promptTextKey)) // TODO Kontrola jestli byla stisknuta klávesa na spuštění nové vlny
        {
            textToPress.enabled = false; // TODO Vypnutí nápovědy
            waves[waveCount].SumUpAllEnemies(); // TODO Sečtení všech nepřátel do "AllEnemies" ve třídě "Wave"
            StartCoroutine(SpawnTheWave()); // TODO Zapnutí korutiny pro postupné spawnování nepřátel
        }
    }
}

// TODO Metoda, která vybere náhodného nepřítele z aktuální "Wave" a spawne ho, následně počkká po dobu jáká má být mezi spawnováním pro danou Wave ("DelayBetweenSpawn")
private IEnumerator SpawnTheWave()
{
    Quaternion rotation = Quaternion.identity;
    Vector3 position;
    waveIsRunning = true;
    for(int i = 0; i < waves[waveCount].AllEnemies; i++)
    {
        float x = spawnpoint.position.x + XSpawnRange;
        float z = spawnpoint.position.z - ZSpawnRange;
        position = new Vector3(Random.Range(x, spawnpoint.position.x), 0.5f, Random.Range(z, spawnpoint.position.z)); // TODO Určení náhodné pozice pro spawn nepřítele

        GameObject tmp = waves[waveCount].GenerateRandomEnemy(); // TODO Generování náhodného objektu nepřítele
        Instantiate(tmp, position, rotation);
        yield return new WaitForSeconds(waves[waveCount].DelayBetweenSpawn);
} 
}

// TODO Tato metoda kontroluje, jestli je nynější "Wave" dokončena --> všichní nepřátelé jsou mrtví
private void IsTheWaveFinished()
{
    if(waves[waveCount].AreAllEnemiesDead())
    {
        waveIsRunning = false;
        if(waveCount < waves.Length-1)
        {
            waveCount++; // TODO Zvýšení počítadla Vln (indexu)
            
        }else // TODO Pokud je "waveCount" >= počet vln, tak to znamená, že již všechny proběhly --> konec (Hráč zvítězil)
        {
            allWavesAreFinished = true;
            AdjustEndscreenMessageEnvironment();
            variables.diamonds += 1;
            PlayerManager.IncrementDifferentLevelWins(); // TODO Zvýšení počtu výher Hráče, aby měl přístup k další úrovni
        }
    }
}
private bool CheckIfPlayerIsDead()
{
    return playerInterface.Health <= 0;
}

// TODO Nastavení všech proměnných pro ukončení úrovně, nehledě na výhře či prohře. Také aktivování panelu finálové zprávy "endscreenPanel"
private void AdjustEndscreenMessageEnvironment()
{
    endscreenPanel.SetActive(true);
    endscreenIsActive = true;
    waveIsRunning = false;
    Time.timeScale = 0f;
    Cursor.visible = true;
    Cursor.lockState = CursorLockMode.None;
}
}


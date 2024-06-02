using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private static int levelCount;
    public static int DifferentLevelWins { get; private set; } // TODO Statická vlastnost pro sledování počtu výher v různých úrovních
    public static PlayerManager Instance { get; private set;} // TODO Singleton instance třídy PlayerManager
    private PlayerInterface playerInterface; // TODO Odkaz na skript PlayerInterface
    private GameObject spawnpoint; // TODO spawnpoint, kde se Hráč po načtení scény objeví

    
    public static void IncrementDifferentLevelWins()
    {
        if (DifferentLevelWins < levelCount)
        {
            DifferentLevelWins += 1;
        }
    }
    void Awake()
    {
        playerInterface = gameObject.GetComponent<PlayerInterface>();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // TODO Nastavení objektu Hráče, aby se při načítání scén nezničil, a tím pádem zůstal jako jediný úplně v každé z nich 
        }else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        DifferentLevelWins = 0;
        levelCount = GameObject.FindGameObjectsWithTag("Level").Length;
    }
    // TODO Metoda volaná při "povolení" skriptu
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // TODO Metoda volaná při "zakázání" skriptu
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //TODO Metoda volaná při načtení nové scény, která slouží k nastavení hodnot 
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Úprava uživatelského rozhraní hráče podle názvu nové scény
        playerInterface.AdjustBattlePlayerUI(scene.name);

        // TODO Skrytí kurzoru a jeho uzamčení ve středu obrazovky
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // TODO Spuštění coroutiny pro nastavení pozice hráče po načtení scény
        StartCoroutine(SetPlayerPositionAfterSceneLoad(scene));
    }

    // TODO Coroutine pro nastavení pozice hráče po úplném načtení scény
    private IEnumerator SetPlayerPositionAfterSceneLoad(Scene scene)
    {
        // TODO Počkání na volání FixedUpdate(), aby bylo na jistotu, že se všechny Start() metody už spustily
        yield return new WaitForFixedUpdate(); 

        // TODO Nalezení spawnpointu Hráče v nové scéně podle jeho tagu
        spawnpoint = GameObject.FindGameObjectWithTag("PlayerSpawnPoint");

        // TODO Přesunutí existujícího Hráče na nový spawnpoint
        gameObject.transform.position = spawnpoint.transform.position;
    }

}

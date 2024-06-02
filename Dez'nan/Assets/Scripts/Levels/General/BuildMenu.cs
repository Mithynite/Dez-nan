using TMPro;
using UnityEngine;

public class BuildMenu : MonoBehaviour
{
    public static bool IsActive { get; private set; }
    public static GameObject SelectedTowergGameObject { get; private set; }
    public static int SelectedTowerPrice { get; private set; }

[Header("Refferences")]
    [SerializeField] private UniversalTowerAttributes towerAttributes;
    [SerializeField] private GameObject buildMenuPanel;
    [SerializeField] private GameObject[] towerPrefabs;
    
    //[SerializeField] private TextMeshProUGUI[] towerPriceObjects; TODO AdjustPriceTexts() --> tohle pole se odkazuje se na null
    [SerializeField] private TextMeshProUGUI archerTowerPriceObject;
    [SerializeField] private TextMeshProUGUI mortarTowerPriceObject;
    [SerializeField] private TextMeshProUGUI mageTowerPriceObject;
    [SerializeField] private TextMeshProUGUI goldMineTowerPriceObject;

    
[Header("Controls")]
    [SerializeField] private KeyCode openKey = KeyCode.B;
    [SerializeField] private KeyCode closeKey = KeyCode.B;

    void Start()
    {
        AdjustPriceTexts();
        buildMenuPanel.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKeyDown(openKey) && !IsActive)
        {
            OpenBuildMenu();
        }else if(Input.GetKeyDown(closeKey))
        {
            CloseBuildMenu();
        }
    }

    // Metoda pro otevření Build menu
    public void OpenBuildMenu()
    {
        buildMenuPanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        IsActive = true;
        Time.timeScale = 0f;
    }

    // Metoda pro zavření Build menu
    public void CloseBuildMenu()
    {
        buildMenuPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        IsActive = false;
        Time.timeScale = 1f;
    }

    private void AdjustPriceTexts() //TODO nevypadá moc dobře
    {
        archerTowerPriceObject.text = towerAttributes.towerAttributesArray[0].cost.ToString();
        mortarTowerPriceObject.text = towerAttributes.towerAttributesArray[1].cost.ToString(); 
        mageTowerPriceObject.text = towerAttributes.towerAttributesArray[2].cost.ToString();
        goldMineTowerPriceObject.text = towerAttributes.towerAttributesArray[3].cost.ToString();
    }

    private void BuildTower(int index)
    {
        if (PlayerInterface.Coins >= towerAttributes.towerAttributesArray[index].cost)
        {
            SelectedTowergGameObject = towerPrefabs[index];
            SelectedTowerPrice = towerAttributes.towerAttributesArray[index].cost;
        }else
        {
            SelectedTowergGameObject = null;
            SelectedTowerPrice = 0;
        }
    }

    public void BuildArcherTower() => BuildTower(0);
    public void BuildMortarTower() => BuildTower(1);
    public void BuildMageTower() => BuildTower(2);
    public void BuildGoldMineTower() => BuildTower(3);
    
    
}
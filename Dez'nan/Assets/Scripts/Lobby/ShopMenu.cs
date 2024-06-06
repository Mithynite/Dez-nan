using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    public static bool isActive;

    [Header("Controls")]
    [SerializeField] private KeyCode openKey = KeyCode.B;
    [SerializeField] private KeyCode closeKey = KeyCode.B;

    [Header("Value changes")]
    [SerializeField] private int walkingSpeedChange;
    [SerializeField] private int sprintingSpeedChange;
    [SerializeField] private int airSpeedChange;
    [SerializeField] private int healthChange;

    [SerializeField] private int towerCostChange;

    [SerializeField] private int diamondDecrease; // TODO Počet odečtených diamantů za nákup vylepšení

    [Header("Refferences")]
    [SerializeField] private UniversalTowerAttributes towerAttributes;
    [SerializeField] private PlayerBehaviorVariables variables;
    [SerializeField] private GameObject shopMenuPanel;
    [SerializeField] private GameObject player;
    private PlayerInterface playerInterface;
    void Start()
    {
        playerInterface = player.GetComponent<PlayerInterface>();
        shopMenuPanel.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKeyDown(openKey) && !isActive)
        {
            OpenBuildMenu();
        }else if(Input.GetKeyDown(closeKey))
        {
            CloseBuildMenu();
        }
    }

    private void OpenBuildMenu()
    {
        shopMenuPanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isActive = true;
        Time.timeScale = 0f;
    }
    private void CloseBuildMenu()
    {
        shopMenuPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isActive = false;
        Time.timeScale = 1f;
    }
    
    // TODO Odečtení diamantů
    private void DeductDiamonds(int amount)
    {
        variables.diamonds -= amount;
        playerInterface.ChangeDiamondText(); // TODO Aktualizování textu pro počet diamantů v rozhraní
    }
    
    // TODO Nakoupení vylepšení léčení
    public void BuyHealthUpgrade()
    {
        if(variables.diamonds > 0)
        {
            variables.maxHealth += 5;
            DeductDiamonds(diamondDecrease);
        }
    }
    
    // TODO Nakoupení vylepšení rychlosti hráče
    public void BuySpeedUpgrade()
    {
        if(variables.diamonds > 0)
        {
            variables.walkingSpeed += walkingSpeedChange;
            variables.sprintingSpeed += sprintingSpeedChange;
            variables.airSpeed += airSpeedChange;
            DeductDiamonds(diamondDecrease);
        }
    }
    
    // TODO Nakoupení vylepšení ceny
    public void BuyTowerPriceUpgrade()
    {
        if(variables.diamonds > 0)
        {
            towerAttributes.ChangeTowerAttributesByValue(towerCostChange); 
            DeductDiamonds(diamondDecrease);
        }
    }
}



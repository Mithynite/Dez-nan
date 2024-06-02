using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    public static bool isActive = false;

    [Header("Controls")]
    [SerializeField] private KeyCode openKey = KeyCode.B;
    [SerializeField] private KeyCode closeKey = KeyCode.B;

    [Header("Value changes")]
    [SerializeField] private int walkingSpeedChange;
    [SerializeField] private int sprintingSpeedChange;
    [SerializeField] private int airSpeedChange;
    [SerializeField] private int healthChange;

    [SerializeField] private int towerCostChange;

    [SerializeField] private int diamondDecrease;

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
    private void DeductDiamonds(int amount)
    {
        variables.diamonds -= amount;
        playerInterface.ChangeDiamondText();
    }
    public void BuyHealthUpgrade()
    {
        if(variables.diamonds > 0)
        {
            variables.maxHealth += 5;
            DeductDiamonds(diamondDecrease);
        }
    }
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
    public void BuyTowerPriceUpgrade()
    {
        if(variables.diamonds > 0)
        {
            towerAttributes.ChangeTowerAttributesByValue(towerCostChange); 
            DeductDiamonds(diamondDecrease);
        }
    }
}



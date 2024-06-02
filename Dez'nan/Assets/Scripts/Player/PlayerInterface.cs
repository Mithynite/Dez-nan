using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInterface : MonoBehaviour
{
    [SerializeField] private PlayerBehaviorVariables variables;
#region In levels
    public int Health {get; private set;}
    
    [SerializeField] private Color damageColour = Color.red;
    private Color defaultColour;
    [SerializeField] private float colourChangeDuration = 0.5f;
    private TextMeshProUGUI healthText;
    private static TextMeshProUGUI coinText;
    public static int Coins {get; private set;}
#endregion

#region In lobby
    private TextMeshProUGUI diamondText;
#endregion

public void AdjustBattlePlayerUI(string sceneName)
{
    if(!sceneName.Equals("Lobby"))
        {
            Health = variables.maxHealth;
            Coins = variables.coins;
            healthText = GameObject.FindWithTag("PlayerHealth").GetComponent<TextMeshProUGUI>();
            coinText = GameObject.FindWithTag("PlayerCoin").GetComponent<TextMeshProUGUI>();
            defaultColour = healthText.color;
            healthText.text = Health.ToString();
            coinText.text = Coins.ToString();
        }else
        {
            ChangeDiamondText();
        }
}
public void TakeDamage(int amount)
{
    Health -= amount;
    if (healthText != null && Health >= 0)
        {
            StartCoroutine(ChangeColour(damageColour, colourChangeDuration));
            healthText.text = Health.ToString();
        }
}
private IEnumerator ChangeColour(Color temporaryColour, float duration)
{
    healthText.color = temporaryColour;
    yield return new WaitForSeconds(duration);
    healthText.color = defaultColour;
}

public static void AddCoins(int amount)
{
    Coins += amount;
    coinText.text = Coins.ToString();
}

public static void TakeCoins(int amount)
{
    Coins -= amount;
    coinText.text = Coins.ToString();
}
public void ChangeDiamondText()
{
    SetUpAtDiamondText(); //if null
    diamondText.text = variables.diamonds.ToString();
}

private void SetUpAtDiamondText()
{
    if(diamondText == null)
    {
        diamondText = GameObject.FindWithTag("PlayerDiamond").GetComponent<TextMeshProUGUI>();
    }
}

}

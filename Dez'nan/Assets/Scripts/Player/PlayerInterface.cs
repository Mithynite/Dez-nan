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
    [SerializeField] private float colourChangeDuration = 0.5f; // TODO délka změny barvy textu pokud Hráč dostane poškození
    private TextMeshProUGUI healthText;
    private static TextMeshProUGUI coinText;
    public static int Coins {get; private set;}
#endregion

#region In lobby
    private TextMeshProUGUI diamondText; // TODO Reference na text ukazující počet diamantů
#endregion

public void AdjustBattlePlayerUI(string sceneName)
{
    // TODO Pokud Hráč není v "Lobby" (je automaticky v nějaké úrovni), aby se nastavili veškeré hodnoty pro toto UI
    if(!sceneName.Equals("Lobby"))
        {
            Health = variables.maxHealth;
            Coins = variables.coins;
            healthText = GameObject.FindWithTag("PlayerHealth").GetComponent<TextMeshProUGUI>();
            coinText = GameObject.FindWithTag("PlayerCoin").GetComponent<TextMeshProUGUI>();
            defaultColour = healthText.color;
            healthText.text = Health.ToString();
            coinText.text = Coins.ToString();
        }else // TODO Tento "else" se tedy aktivuje pouze v "Lobby" 
        {
            ChangeDiamondText();
        }
}

//TODO Udělení poškození Hráči
public void TakeDamage(int amount)
{
    Health -= amount;
    if (healthText != null && Health >= 0)
        {
            StartCoroutine(ChangeColour(damageColour, colourChangeDuration)); // TODO Zapnutí Courutine pro změnu barvy textu, když Hráč dostane poškození
            healthText.text = Health.ToString();
        }
}
private IEnumerator ChangeColour(Color temporaryColour, float duration)
{
    healthText.color = temporaryColour;
    yield return new WaitForSeconds(duration);
    healthText.color = defaultColour;
}

//TODO Přidání coinů
public static void AddCoins(int amount)
{
    Coins += amount;
    coinText.text = Coins.ToString();
}


//TODO Odebrání coinů
public static void TakeCoins(int amount)
{
    Coins -= amount;
    coinText.text = Coins.ToString();
}

// TODO Aktualizace textu diamantů
public void ChangeDiamondText()
{
    SetUpAtDiamondText();
    diamondText.text = variables.diamonds.ToString();
}

// TODO Kontroluji jestli není reference null
private void SetUpAtDiamondText()
{
    if(diamondText == null)
    {
        diamondText = GameObject.FindWithTag("PlayerDiamond").GetComponent<TextMeshProUGUI>();
    }
}

}

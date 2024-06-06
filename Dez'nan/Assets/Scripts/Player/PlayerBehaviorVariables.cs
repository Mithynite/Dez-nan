
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBehaviorVariables", menuName = "ScriptableObjects/PlayerBehaviorVariables", order = 1)]
public class PlayerBehaviorVariables : ScriptableObject
{
    [Header("Movement")]
	public float walkingSpeed;
	public float sprintingSpeed;
	public float airSpeed;

    [Header("Dragging")]
	public float groundDrag;
	public float playerHeight;

    [Header("Jumping")]
	public float jumpForce;
	public float jumpCooldown;
	
	[Header("Slope Handling")]
	public float maxSlopeAngle; //TODO Maximální sklon plochy, po které Hráč může jít

    [Header("Interface")]
    public int diamonds;
	public int coins;
    public int maxHealth;

    // TODO Obnovení hodnot na původní
    public void ResetValues()
    {
	    walkingSpeed = 5f;
	    sprintingSpeed = 7f;
	    airSpeed = 3f;
	    groundDrag = 5f;
	    playerHeight = 5f;
	    jumpForce = 8f;
	    jumpCooldown = 1.2f;
	    maxSlopeAngle = 50;
	    diamonds = 0;
	    coins = 50;
	    maxHealth = 20;
    } 
    
}


using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rigidbody; // TODO Deklarace proměnné pro Rigidbody
    [SerializeField] private PlayerBehaviorVariables variables; // TODO Proměnné Assetu pro přístup k vlastnostem chování Hráče

    [Header("Movement")]

    // TODO Deklarace proměnných pro pohyb a rychlost
    private float actualSpeed;
    public Vector3 moveDirection;
    public Transform orientation;

    private float horizontalInput;
    private float vertiqalInput;
    
    [SerializeField] private LayerMask whatIsGround; // TODO Vrstva pro detekci země
    private bool onTheGround; // TODO Proměnná pro kontrolu, zda je hráč na zemi

    [Header("Jumping")]
    private bool readyToJump = true; // TODO Proměnná pro kontrolu, zda je Hráč připraven skákat
    [SerializeField] private KeyCode jumpKey = KeyCode.Space; // TODO Klávesa pro skákání

    [Header("Sprinting")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift; // TODO Klávesa pro sprintování

    [Header("Slope Handling")]
    // TODO Proměnné pro detekci a manipulaci se sklonem plochy
    private RaycastHit slopeHit;
    private bool exitingSlope;

    private MovementState state; // TODO Deklarace stavu pohybu
    public enum MovementState
    {
        walking, sprinting, flying
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // TODO Metoda pro manipulaci stavu pohybu
    public void StateHandler()
    {
        // TODO Pokud Hráč sprintuje a je na zemi, nastaví stav na sprinting a rychlost na "sprintingSpeed"
        if (Input.GetKey(sprintKey) && onTheGround)
        {
            state = MovementState.sprinting;
            actualSpeed = variables.sprintingSpeed;
        }
        // TODO Pokud Hráč není sprinting a je na zemi, nastaví stav na "walking" a rychlost na "walkingSpeed"
        else if (onTheGround)
        {
            state = MovementState.walking;
            actualSpeed = variables.walkingSpeed;
        }
        // TODO Pokud Hráč není na zemi, nastaví stav na "flying" a rychlost na "airSpeed"
        if (!onTheGround)
        {
            state = MovementState.flying;
            actualSpeed = variables.airSpeed;
        }
    }

    // TODO Metoda pro získání vstupu od Hráče
    private void MoveInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        vertiqalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(jumpKey) && onTheGround && readyToJump)
        {
            Jump();
            readyToJump = false;
            Invoke(nameof(ResetJump), variables.jumpCooldown); // TODO Volání metody pro obnovení skoku po nějakém čase
        }
    }

    // TODO Metoda pro pohybování Hráče
    private void Move()
    {
        moveDirection = orientation.forward * vertiqalInput + orientation.right * horizontalInput;
        if (OnSloup() && !exitingSlope)
        {
            rigidbody.AddForce(GetSlopeMoveDirection() * actualSpeed * 10f, ForceMode.Force);
        }
        if (onTheGround)
            rigidbody.AddForce(moveDirection.normalized * actualSpeed * 10f, ForceMode.Force);
    }

    // TODO Metoda pro kontrolu rychlosti Hráče
    private void SpeedControl()
    {
        if (OnSloup() && !exitingSlope)
        {
            if (rigidbody.velocity.magnitude > actualSpeed)
                rigidbody.velocity = rigidbody.velocity.normalized * actualSpeed;
        }
        else
        {
            Vector3 flatVelocity = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z);
            if (flatVelocity.magnitude > actualSpeed) // TODO Magnitude podle vzorce určí velikost Vectoru a porovná ji s požadovanou rychlostí, kterou nesmí překročit
            {
                Vector3 limitedVelocity = flatVelocity.normalized * actualSpeed; // TODO Limitace rychlosti 
                rigidbody.velocity = new Vector3(limitedVelocity.x, rigidbody.velocity.y, limitedVelocity.z);
            }
        }
    }

    // TODO Metoda pro skákání Hráče
    private void Jump()
    {
        exitingSlope = true;
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z); // TODO Resetování Y velocity => bude skákat vždy stejně vysoko
        rigidbody.AddForce(transform.up * variables.jumpForce, ForceMode.Impulse);
    }

    // TODO Metoda pro resetování skoku
    private void ResetJump()
    {
        readyToJump = true;
        exitingSlope = false;
    }

    // TODO Metoda pro kontrolu, zda je Hráč na "svahu"
    private bool OnSloup()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, variables.playerHeight * 0.5f + 0.1f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < variables.maxSlopeAngle && angle != 0;
        }
        return false;
    }

    // TODO Metoda pro získání směru pohybu na svahu
    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
    
    // TODO FixedUpdate pomůže k vyšší "přesnosti"
    private void FixedUpdate()
    {
        Move();
    }

    // TODO Metoda pro aktualizaci stavu Hráče každý frame
    void Update()
    {
        MoveInput();
        onTheGround = Physics.Raycast(transform.position, Vector3.down, variables.playerHeight * 0.5f + 0.2f, whatIsGround); // TODO Kontrola jestli je Hráč na zemi pomocí Raycastu
        if (onTheGround)
        {
            rigidbody.drag = variables.groundDrag;
        }
        else
        {
            rigidbody.drag = 0;
        }
        SpeedControl();
        StateHandler();
    }
}

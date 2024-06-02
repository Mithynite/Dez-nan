using UnityEngine;

public class Movement : MonoBehaviour
{
	//[HideInInspector] = schová v Inspektoru
	Rigidbody rigidbody;

	[SerializeField] private PlayerBehaviorVariables variables;

	[Header("Movement")] //Víceméně nadpis přímo v inspektoru

	private float actualSpeed;

	public Vector3 moveDirection;
	public Transform orientation;

	private float horizontalInput;
	private float vertiqalInput;
	
	[SerializeField] private LayerMask whatIsGround;
	
	private bool onTheGround;
	

	[Header("Jumping")]
	private bool readyToJump = true;
	[SerializeField] private KeyCode jumpKey = KeyCode.Space;

	[Header("Sprinting")]
	[SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;

	[Header("Slope Handling")]
	private RaycastHit slopeHit;
	private bool exitingSlope;

	//Kontrola z jiných tříd

	private MovementState state;
	public enum MovementState
	{
		walking, sprinting, flying
	}

	void Start()
	{
		rigidbody = GetComponent<Rigidbody>();
	}

	public void StateHandler()
	{
		//Mode - Sprint
		if (Input.GetKey(sprintKey) && onTheGround)
		{
			state = MovementState.sprinting;
			actualSpeed = variables.sprintingSpeed;
			//Mode - Walk
		}
		else if (onTheGround)
		{
			state = MovementState.walking;
			actualSpeed = variables.walkingSpeed;
		}
		//Mode - In the air
		if (!onTheGround)
		{
			state = MovementState.flying;
			actualSpeed = variables.airSpeed;
		}
	}

	private void MoveInput()
	{
		horizontalInput = Input.GetAxisRaw("Horizontal");
		vertiqalInput = Input.GetAxisRaw("Vertical");
		if (Input.GetKey(jumpKey) && onTheGround && readyToJump)
		{
			Jump();
			readyToJump = false;
			Invoke(nameof(ResetJump), variables.jumpCooldown);
		}
	}

	private void Move()
	{
		moveDirection = orientation.forward * vertiqalInput + orientation.right * horizontalInput;
		if (OnSloup() && !exitingSlope)
		{
			rigidbody.AddForce(GetSlopeMoveDirection() * actualSpeed * 10f, ForceMode.Force);
		}
		if (onTheGround)
			rigidbody.AddForce(moveDirection.normalized * actualSpeed * 10f, ForceMode.Force);

		//vypnutí gravitace
		//rigidbody.useGravity = !OnSloup();
	}
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
			if (flatVelocity.magnitude > actualSpeed) //magnitude podle vzorce určí velikost Vectoru
			{
				Vector3 limitedVelocity = flatVelocity.normalized * actualSpeed;
				rigidbody.velocity = new Vector3(flatVelocity.x, rigidbody.velocity.y, flatVelocity.z);
			}
		}
	}
	private void Jump()
	{
		exitingSlope = true;
		rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z); //resetování Y velocity => bude skákat vždy stejně vysoko
		rigidbody.AddForce(transform.up * variables.jumpForce, ForceMode.Impulse);
	}
	private void ResetJump()
	{
		readyToJump = true;
		exitingSlope = false;
	}
	private bool OnSloup()
	{
		if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, variables.playerHeight * 0.5f + 0.1f))
		{
			float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
			return angle < variables.maxSlopeAngle && angle != 0;
		}
		return false;
	}
	private Vector3 GetSlopeMoveDirection()
	{
		return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
	}
private void FixedUpdate(){
		Move();
}

	void Update()
	{
		MoveInput();
		onTheGround = Physics.Raycast(transform.position, Vector3.down, variables.playerHeight * 0.5f + 0.2f, whatIsGround);
		if (onTheGround)
		{
			rigidbody.drag = variables.groundDrag;
		}else
		{
			rigidbody.drag = 0;
		}
		SpeedControl();
		StateHandler();
	}
}
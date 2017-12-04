using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMovement : MonoBehaviour {

	[Header("Object references")]
	public CharacterController m_target;
	public Animator m_cameraAnimator;

	[Header("Movement")]
	public float m_walkSpeed;
	public float m_runSpeed;
	[Tooltip("If you want to jump higher, increase this variable.")]
	public float m_jumpHeight;
	[Tooltip("Indicate how fast you jump up and then fall down.")]
	public float m_jumpSpeed;

	public Vector3 MoveDirection { get; set; }
	private IFPSCharacterState m_state = null;

	void Awake() {
		GameStats._FPSMovement = this;
	}

	// Use this for initialization
	void Start () {
		if (m_target == null) {
			m_target = GetComponent<CharacterController> ();
		}

		MoveDirection = new Vector3 (0.0f, 0.0f, 0.0f);
		ChangeState (new FPSCharacterStateIdle ());
	}

	// Update is called once per frame
	void Update () {
		// Calculate MoveDirection
		CalculateMoveDirection ();

		// Apply movement for CharacterController.
		m_target.Move (MoveDirection * Time.deltaTime);

		// Update state.
		m_state.Update (this);

		// Set animator's parameter.
		SetAnimatorParameters ();
	}

	#region Movement

	void CalculateMoveDirection () {
		MoveDirection = new Vector3 (0.0f, MoveDirection.y, 0.0f);

		// Walk.
		Vector3 dir = new Vector3 (MyInput.GetAxis ("Horizontal"), 0.0f, MyInput.GetAxis ("Vertical"));
		dir = m_target.transform.TransformDirection (dir);
		dir.y = 0.0f;

		if (MyInput.GetButton ("Sprint")) {
			dir *= m_runSpeed;
		} else {
			dir *= m_walkSpeed;
		}

		MoveDirection += dir;

		// Jump.
		if (m_target.isGrounded && MyInput.GetButtonDown("Jump")) {
			ChangeState (new FPSCharacterStateJump ());
		}

		// Character controller don't have gravity support, so apply it here.
		MoveDirection = new Vector3 (
			MoveDirection.x,
			MoveDirection.y - m_jumpSpeed * Time.deltaTime,
			MoveDirection.z
		);
	}

	#endregion

	#region Camera animation

	void SetAnimatorParameters() {
		m_cameraAnimator.SetFloat ("Velocity", GetVelocity ());
	}

	#endregion

	#region Utilities

	public float GetVelocity() {
		if (m_target.isGrounded) {
			return m_target.velocity.magnitude;
		} else {
			return 0.0f;
		}
	}

	public void ChangeState (IFPSCharacterState newState) {
		if (m_state != null) {
			m_state.EndState (this);
		}

		m_state = newState;
		m_state.StartState (this);
	}

	#endregion
}

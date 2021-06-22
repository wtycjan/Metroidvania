using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System;

public class CharacterController : MonoBehaviour
{
	[Header("Player Stats:")]

	public float runSpeed;

	public int maxHealth = 3;

	public int attackDamage = 1;

	public float attackRate = 1;

	[Range(0, 1f)] public float attackRange = .5f;

	[SerializeField] private float m_JumpForce = 400f;

	[SerializeField] private bool m_AirControl = false;

	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;

	[Space]

	public LayerMask enemyLayers;

	public Transform attackPoint;

	[SerializeField] private LayerMask m_WhatIsGround;

	[SerializeField] private Transform m_GroundCheck;

	const float k_GroundedRadius = .2f;

	[HideInInspector] public Animator animator;
	[HideInInspector] public Rigidbody2D rigidbody2d { get; private set; }
	[HideInInspector] public bool isGrounded { get; private set; }

	[HideInInspector] public float horizontalMove = 0;

	[HideInInspector] public bool jump = false;

	private bool isFacingRight = true;

	private Vector3 m_Velocity = Vector3.zero;


    private void OnEnable()
    {
		if (SpawnsOnGround())
			LandOnGround();
	}

    private void Awake()
	{
		rigidbody2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	private void FixedUpdate()
	{
		HandleGroundCheck();
		HandleMovement();
	}
	public void StopPlayer()
	{
		rigidbody2d.velocity = new Vector2(0, 0);
	}

	private void HandleGroundCheck()
	{
		bool wasGrounded = isGrounded;
		isGrounded = false;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				isGrounded = true;
				if (!wasGrounded && rigidbody2d.velocity.y < 0.05)
					LandOnGround();
			}
		}
		
	}
	private void HandleMovement()
    {
		Move(horizontalMove * Time.fixedDeltaTime, jump);
		jump = false;
		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
		animator.SetBool("IsGrounded", isGrounded);
	}

	private void Move(float move, bool jump)
	{
		if (isGrounded || m_AirControl)
		{
			Vector3 targetVelocity = new Vector2(move * 10f, rigidbody2d.velocity.y);
			rigidbody2d.velocity = Vector3.SmoothDamp(rigidbody2d.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			if (move > 0 && !isFacingRight)
			{
				Flip();
			}
			else if (move < 0 && isFacingRight)
			{
				Flip();
			}
		}
		if (isGrounded && jump)
		{
			isGrounded = false;
			rigidbody2d.AddForce(new Vector2(0f, m_JumpForce));
		}
	}


	private void Flip()
	{ 
		isFacingRight = !isFacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private void LandOnGround()
	{
		animator.SetBool("IsJumping", false);
	}

	private bool SpawnsOnGround()
    {
		return !isGrounded && rigidbody2d.velocity.y < 0.05;

	}

}
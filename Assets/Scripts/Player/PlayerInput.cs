using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private InputMaster controls;

	private CharacterController characterController;

	private float nextAttackTime = 0;


	private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
		ClearMovementInput();
    }
    private void Awake()
    {
        SetupInputController();
		characterController = GetComponent<CharacterController>();
	}
    private void SetupInputController()
    {
        controls = new InputMaster();
        controls.Player.Attack.performed += context => Attack();
        controls.Player.Jump.performed += context => Jump();
        controls.Player.Movement.started += context => GetMovementInput(context.ReadValue<float>());
        controls.Player.Movement.canceled += context => ClearMovementInput();

        
    }
    private void Update()
    {
        print( controls.Player.Movement.ReadValue<float>());
    }

	public void Attack()
	{
		print("iter");
		if (Time.time >= nextAttackTime && characterController.isGrounded)
		{
			characterController.animator.SetTrigger("Attack");
			Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(characterController.attackPoint.position, characterController.attackRange, characterController.enemyLayers);
			foreach (Collider2D enemy in hitEnemies)
			{
				enemy.GetComponent<Enemy>().TakeDamage(characterController.attackDamage);
			}
			float attackTime = 1f / characterController.attackRate;
			nextAttackTime = Time.time + attackTime;
			StartCoroutine(PausePlayer(attackTime));
		}

	}
	private void Jump()
	{
		characterController.jump = true;
		characterController.animator.SetBool("IsJumping", true);
	}

	private void GetMovementInput(float direction)
	{
		characterController.horizontalMove = direction * characterController.runSpeed;
	}

	private void ClearMovementInput()
	{
		characterController.horizontalMove = 0;
	}

	private IEnumerator PausePlayer(float time)
	{
		characterController.StopPlayer();
		characterController.enabled = false;
		yield return new WaitForSeconds(time);
		characterController.enabled = true;
	}

}

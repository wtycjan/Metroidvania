using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinEnemy : Enemy
{
	[Header("Bump into strength:")]
    public int attackDamage = 1;

	public float knockbackDuration = 0.15f;

	public float knockbackPower = 150;

	[Header("Sword strength:")]
	public float swordAttackRate = 1;

	public int swordAttackDamage = 1;

	public float swordKnockbackDuration = 0.15f;

	public float swordKnockbackPower = 150;

	[SerializeField] private BoxCollider2D swordAttackPoint;

	private Coroutine attacksDisabled;



	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			Vector2 direction = new Vector2(Mathf.Sign(transform.position.x - collision.transform.position.x) * 1, 1);
			collision.gameObject.GetComponent<PlayerStatus>().TakeDamage(attackDamage, knockbackDuration, knockbackPower, direction);
		}
	}

	public override void TakeDamage(int damage)
    {
		base.TakeDamage(damage);
		attacksDisabled = StartCoroutine(DisableAttacks(animator.GetCurrentAnimatorClipInfo(0).Length));
	}

	protected override void Die()
	{
		base.Die();
		StopCoroutine(attacksDisabled);
	}

	private IEnumerator DisableAttacks(float time)
	{
		swordAttackPoint.enabled = false;
		yield return new WaitForSeconds(time);
		swordAttackPoint.enabled = true;
	}
}

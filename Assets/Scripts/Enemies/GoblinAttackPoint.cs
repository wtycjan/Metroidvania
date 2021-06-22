using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttackPoint : MonoBehaviour,AttackPoint
{
	private float nextAttackTime = 0;

	private Coroutine dealDamageToPlayer;

	private GoblinEnemy goblin;

    private void Awake()
    {
		goblin = GetComponentInParent<GoblinEnemy>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			Attack(collision.gameObject);
		}
	}

	public void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			Attack(collision.gameObject);
		}
	}

	public void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			StopCoroutine(dealDamageToPlayer);
		}
	}

    public void Attack(GameObject player)
	{
		if (Time.time >= nextAttackTime)
		{
			goblin.animator.SetTrigger("Attack");
			float attackTime = 1f / goblin.swordAttackRate;
			nextAttackTime = Time.time + attackTime;
			StartCoroutine(goblin.Stun(attackTime));
			dealDamageToPlayer = StartCoroutine(DealDamageToPlayer(attackTime / 4, player));
		}
	}

	public IEnumerator DealDamageToPlayer(float animationStartTime, GameObject player)
	{
		yield return new WaitForSeconds(animationStartTime);
		Vector2 direction = new Vector2(Mathf.Sign(transform.position.x - player.transform.position.x) * 1, 1);
		player.GetComponent<PlayerStatus>().TakeDamage(goblin.swordAttackDamage, goblin.swordKnockbackDuration, goblin.swordKnockbackPower, direction);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomEnemy : Enemy
{
    [SerializeField] private int attackDamage = 1;

    [SerializeField] private float knockbackDuration = 0.15f;

    [SerializeField] private float knockbackPower = 150;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 direction = new Vector2(Mathf.Sign(transform.position.x - collision.transform.position.x) * 1, 1);
            collision.gameObject.GetComponent<PlayerStatus>().TakeDamage(attackDamage, knockbackDuration, knockbackPower, direction);
        }
    }

}

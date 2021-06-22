using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBelowMap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStatus>().TakeDamage(999999, 0, 0, Vector2.zero);
        }
    }
}

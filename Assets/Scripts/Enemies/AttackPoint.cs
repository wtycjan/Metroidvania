using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AttackPoint
{
    void OnTriggerEnter2D(Collider2D collision);
    void OnTriggerStay2D(Collider2D collision);
    void OnTriggerExit2D(Collider2D collision);
    void Attack(GameObject player);
    IEnumerator DealDamageToPlayer(float animationStartTime, GameObject player);
}

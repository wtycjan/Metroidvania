using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Coin : MonoBehaviour
{
    public delegate void CoinCollected();

    public static Action<int> OnCoinCollected;

    private int pointValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            OnCoinCollected.Invoke(pointValue);
            gameObject.SetActive(false);
        }
    }
}

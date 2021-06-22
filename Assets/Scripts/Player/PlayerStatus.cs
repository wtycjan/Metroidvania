using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerStatus : MonoBehaviour
{
    public delegate void DamageTaken();

    public static Action<int> OnDamageTaken;

    private CharacterController characterController;

    private int currentHealth;

    private Coroutine stunningCoroutine;

    private float cameraShakeOnHitPower = 0.5f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        currentHealth = characterController.maxHealth;
    }

    public void TakeDamage(int damage, float knockbackDuration, float knockbackPower, Vector2 knockbackDirection)
    {
        currentHealth -= damage;
        OnDamageTaken.Invoke(currentHealth);
        characterController.animator.SetTrigger("Hurt");
        stunningCoroutine = StartCoroutine(Stun(knockbackDuration));
        StartCoroutine(Knockback(knockbackDuration,knockbackPower, knockbackDirection));
        CameraShake.Shake(knockbackDuration, cameraShakeOnHitPower);
        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Stun(float time)
    {
        characterController.StopPlayer();
        characterController.enabled = false;
        yield return new WaitForSeconds(time);
        characterController.enabled = true;
    }

    private IEnumerator Die()
    {
        characterController.animator.SetBool("IsDead",true);
        StopCoroutine(stunningCoroutine);
        var colliders = GetComponents<BoxCollider2D>();
        foreach (BoxCollider2D collider in colliders)
        {
            collider.enabled = false;
        }
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        yield return new WaitForSeconds(characterController.animator.GetCurrentAnimatorClipInfo(0).Length);
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    private IEnumerator Knockback(float duration, float power, Vector2 direction)
    {
        float timer = 0;
        while (duration>timer)
        {
            timer += Time.deltaTime;
            characterController.rigidbody2d.AddForce(new Vector2(direction.x * -1 * power, direction.y  * power/2));
        }
        yield return null;
    }

}

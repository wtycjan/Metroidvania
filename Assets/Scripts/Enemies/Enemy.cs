using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    [SerializeField] protected AIPath aiPath;

    [SerializeField] private int maxHealth=2;

    private bool facingRight=true;

    protected int currentHealth;

    protected Coroutine stunningCoroutine;

    private float cameraShakeOnHitPower = 0.1f;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if(ShouldBeFacingRight())
        {
            Flip();
        }
        else if (ShouldBeFacingLeft())
        {
            Flip();
        }
        UpdateAnimator();
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        float camShakeTime = (float)animator.GetCurrentAnimatorClipInfo(0).Length/5;
        CameraShake.Shake(camShakeTime, cameraShakeOnHitPower);
        if (currentHealth<=0)
        {
            Die();
        }     
        else
        {
            stunningCoroutine = StartCoroutine(Stun(animator.GetCurrentAnimatorClipInfo(0).Length));
        }
    }


    public IEnumerator Stun(float time)
    {
        aiPath.enabled = false;
        yield return new WaitForSeconds(time);
        aiPath.enabled = true;
    }

    protected virtual void Die()
    {
        StopCoroutine(stunningCoroutine);
        animator.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, animator.GetCurrentAnimatorClipInfo(0).Length);
    }


    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private bool ShouldBeFacingRight()
    {
        return aiPath.desiredVelocity.x >= 0.01f && !facingRight;
    }    
    
    private bool ShouldBeFacingLeft()
    {
        return aiPath.desiredVelocity.x <= -0.01f && facingRight;
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("Speed", Mathf.Abs(aiPath.desiredVelocity.x));
    }
}

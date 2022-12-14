using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float movementSpeed = 2.0f;
    public bool vertical;
    public float flipWindow = 2.0f;
    public float degradeTime = 10.0f;
    public float contactDamage = 10;

    Rigidbody2D rigidbody2D;
    Animator animator;
    float lastFlippedTime;
    float lastFixedTime;
    int direction = 1;
    bool broken = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!broken)
        {
            if (Time.time > lastFixedTime + degradeTime)
            {
                Break();
            }
            else
            {
                return;
            }
        }

        if (Time.time > lastFlippedTime + flipWindow)
        {
            direction = -direction;
            lastFlippedTime = Time.time;
        }
    }

    void FixedUpdate()
    {
        if (!broken) return;

        Vector2 position = rigidbody2D.position;
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * movementSpeed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * movementSpeed * direction;    
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        
        rigidbody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
         RubyController ruby = other.gameObject.GetComponent<RubyController>();

        if (ruby != null)
        {
            ruby.ChangeHP(-contactDamage);
        }
    }

    public void Fix()
    {
        broken = false;
        rigidbody2D.simulated = false;
        lastFixedTime = Time.time;
        animator.SetTrigger("Fixed");
    }

    public void Break()
    {
        broken = true;
        rigidbody2D.simulated = true;
        animator.SetTrigger("Broken");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float movementSpeed = 4.0f;

    public float maxHp = 100;
    public float invulnerabilityWindow = 1.0f;
    public float launchDelay = 0.3f;

    public GameObject projectilePrefab;

    [HideInInspector]
    public float hp { get {return currentHp; }}
    float currentHp;

    bool isInvulnerable;
    float lastInvulnerableTime;
    float lastLaunchTime;

    Rigidbody2D rigidbody2d;
    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);
    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        isInvulnerable = Time.time < lastInvulnerableTime + invulnerabilityWindow;

        if (Time.time > lastLaunchTime + launchDelay && Input.GetButtonDown("Fire1"))
        {
            Launch();
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + movementSpeed * horizontal * Time.deltaTime;
        position.y = position.y + movementSpeed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHP(float change)
    {
        if (change < 0)
        {
            if (isInvulnerable) return;
            
            animator.SetTrigger("Hit");

            lastInvulnerableTime = Time.time;
        }

        currentHp = Mathf.Clamp(currentHp + change, 0, maxHp);
        Debug.Log(currentHp);
        
        //if (currentHp =0)
        //{

        //}
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
    }
}

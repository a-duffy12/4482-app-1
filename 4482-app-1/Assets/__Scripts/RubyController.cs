using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float movementSpeed = 4.0f;

    public float maxHp = 100;
    public float hp { get {return currentHp; }}
    float currentHp;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
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
        currentHp = Mathf.Clamp(currentHp + change, 0, maxHp);
        
        //if (currentHp =0)
        //{

        //}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // get inputs
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // move character
        Vector2 position = transform.position;
        position.x = position.x + 4f * horizontal * Time.deltaTime;
        position.y = position.y + 4f * vertical * Time.deltaTime;
        transform.position = position;
    }
}

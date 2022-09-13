using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overdrive : MonoBehaviour
{
    public float overdriveDuration = 5.0f;

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController ruby = other.GetComponent<RubyController>();

        if (ruby != null)
        {
            ruby.Overdrive(overdriveDuration);
            Destroy(gameObject);
        }
    }
}

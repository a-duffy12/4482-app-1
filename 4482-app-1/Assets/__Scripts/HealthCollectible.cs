using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public float hpRestore = 25;

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController ruby = other.GetComponent<RubyController>();

        if (ruby != null)
        {
            if (ruby.hp < ruby.maxHp)
            {
                ruby.ChangeHP(hpRestore);
                Destroy(gameObject);
            }
            
        }
    }
}

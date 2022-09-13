using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollectible : MonoBehaviour
{
    public int ammoSupply = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController ruby = other.GetComponent<RubyController>();

        if (ruby != null)
        {
            if (ruby.ammo < ruby.maxAmmo)
            {
                ruby.GiveAmmo(ammoSupply);
                Destroy(gameObject);
            }
        }
    }
}

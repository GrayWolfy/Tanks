using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRate : Bonus
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GunController gc = collision.gameObject.GetComponentInChildren<GunController>();
        gc.SetSpeed();
        
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : Bonus
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Animator>().SetTrigger("invincible");
        Destroy(gameObject);
    }
}

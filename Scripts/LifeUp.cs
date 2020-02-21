using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUp : Bonus
{
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Tracker.playerLifes++;
        GamePlayManger GPM = GameObject.Find("Canvas").GetComponent<GamePlayManger>();
        GPM.UpdatePlayerLifes();
        Destroy(gameObject);
    }
}

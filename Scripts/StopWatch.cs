using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopWatch : Bonus
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GamePlayManger GPM = GameObject.Find("Canvas").GetComponent<GamePlayManger>();
        GPM.ActivateFreeze();
        Destroy(gameObject);
    }
}

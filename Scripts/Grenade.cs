using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Bonus
{
    GameObject[] smallTanks, armoredTanks, fastTanks, heavyTanks;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        List<GameObject> enemies = new List<GameObject>();
        smallTanks = GameObject.FindGameObjectsWithTag("Small");
        armoredTanks = GameObject.FindGameObjectsWithTag("Armored");
        fastTanks = GameObject.FindGameObjectsWithTag("Fast");
        heavyTanks = GameObject.FindGameObjectsWithTag("Heavy");
        enemies.AddRange(smallTanks);
        enemies.AddRange(armoredTanks);
        enemies.AddRange(fastTanks);
        enemies.AddRange(heavyTanks);
        GameObject[] enemiesArray = enemies.ToArray();
        
        foreach (GameObject enemy in enemiesArray)
        {
            TankHealth health = enemy.GetComponent<TankHealth>();
            health.Death();
        }
        
        Destroy(gameObject);
    }
}

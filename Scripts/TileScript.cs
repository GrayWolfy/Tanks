using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class TileScript : MonoBehaviour
{
    [SerializeField] private Sprite yMinusBrick;
    [SerializeField] private Sprite xMinusBrick;
    [SerializeField] private Sprite yPlusBrick;
    [SerializeField] private Sprite xPlusBrick;
    [SerializeField] private int health;

    public void TakeDamage(string bulletDirection)
    {
        health--;
        if (health < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

            switch (bulletDirection)
            {
                case "xPlus":
                    spriteRenderer.sprite = xPlusBrick;

                    break;
                case "xMinus":
                    spriteRenderer.sprite = xMinusBrick;

                    break;
                case "yPlus":
                    spriteRenderer.sprite = yPlusBrick;

                    break;
                case "yMinus":
                    spriteRenderer.sprite = yMinusBrick;

                    break;
            }
        }
    }
}
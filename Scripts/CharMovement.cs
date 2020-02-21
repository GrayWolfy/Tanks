using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class CharMovement : MonoBehaviour
{
    public int speed = 5;
    
    protected bool isInMovement;
    
    protected IEnumerator MoveHorizontal(float horizontal, Rigidbody2D rb)
    {
        isInMovement = true;

        Quaternion rotation = Quaternion.Euler(0, 0, -horizontal * 90f);
        
        transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
        transform.rotation = rotation;

        float movementMomentum = 0f;

        Vector2 movement, lastPosition;

        while (movementMomentum < Mathf.Abs(horizontal))
        {
            float distance = speed * Time.deltaTime;
            movementMomentum += distance;

            movementMomentum = Mathf.Clamp(movementMomentum, 0f, 1f);

            movement = new Vector2(distance * horizontal, 0f);
            lastPosition = rb.position + movement;

            if (1 == movementMomentum)
            {
                lastPosition = new Vector2(Mathf.Round(lastPosition.x), lastPosition.y);
            }
            
            rb.MovePosition(lastPosition);
            yield return new WaitForFixedUpdate();
            
        }

        isInMovement = false;
    }

    protected IEnumerator MoveVertical(float vertical, Rigidbody2D rb)
    {
        isInMovement = true;
        
        transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));

        Quaternion rotation;
        
        rotation = Quaternion.Euler(0, 0, 0);

        if (vertical < 0)
        {
            rotation = Quaternion.Euler(0, 0, vertical * 180f);
        }

        transform.rotation = rotation;

        float movementMomentum = 0f;
        Vector2 lastPosition, movement;

        while (movementMomentum < Mathf.Abs(vertical))
        {
            float distance = speed * Time.deltaTime;
            movementMomentum += distance;

            movementMomentum = Mathf.Clamp(movementMomentum, 0f, 1f);
            
            movement = new Vector2(0f, distance * vertical);
            lastPosition = rb.position + movement;

            if (1 == movementMomentum)
            {
                lastPosition = new Vector2(lastPosition.x, Mathf.Round(lastPosition.y));
            }
            
            rb.MovePosition(lastPosition);
            yield return new WaitForFixedUpdate();
            
        }

        isInMovement = false;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ice")
        {
            speed -= 4;
        } 
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ice")
        {
            speed = 5;
        } 
    }
}
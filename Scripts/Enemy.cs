
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : CharMovement
{
    private Rigidbody2D rb;
    private bool playerDetected;
    private GunController gc;
    public static bool freezing;

    [SerializeField] private LayerMask layer;

    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    };

    private Direction playerDirection;
    private float vertical, horizontal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        RandomDirection();
        gc = GetComponentInChildren<GunController>();
        Invoke("FireWhenWanted", Random.Range(1f, 5f));
    }

    public void RandomDirection()
    {
        CancelInvoke("RandomDirection");
        playerDetected = false;
        
        List<Direction> whereTo = new List<Direction>();

        Dictionary<Vector2, Direction> directionCheck = new Dictionary<Vector2, Direction>();

        directionCheck.Add(new Vector2(1, 0), Direction.Right);
        directionCheck.Add(new Vector2(-1, 0), Direction.Left);
        directionCheck.Add(new Vector2(0, 1), Direction.Up);
        directionCheck.Add(new Vector2(0, -1), Direction.Down);

        LayerMask playerLayer = LayerMask.GetMask("Player");

        foreach (KeyValuePair<Vector2, Direction> entry in directionCheck)
        {
            if (!Physics2D.Linecast(transform.position, (Vector2) transform.position + entry.Key, layer))
            {
                whereTo.Add(entry.Value);
            }

            if (Physics2D.Linecast(transform.position, (Vector2) transform.position + entry.Key, playerLayer))
            {
                playerDetected = true;
                playerDirection = entry.Value;
            }
        }

        Direction direction = whereTo[Random.Range(0, whereTo.Count)];

        if (playerDetected)
        {
            direction = playerDirection;
        }

        foreach (KeyValuePair<Vector2, Direction> entry in directionCheck)
        {
            if (entry.Value == direction)
            {
                vertical = entry.Key.y;
                horizontal = entry.Key.x;
            }
        }

        Invoke("RandomDirection", Random.Range(3, 6));
    }
    
    void FireWhenWanted()
    {
        gc.Fire();
        Invoke("FireWhenWanted", Random.Range(1f, 5f));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        RandomDirection();
    }

    private void FixedUpdate()
    {
        if (vertical != 0 && isInMovement == false) StartCoroutine(MoveVertical(vertical, rb));
        else if (horizontal != 0 && isInMovement == false) StartCoroutine(MoveHorizontal(horizontal, rb));
    }
    
    public void ToFreezeTank()
    {
        CancelInvoke();
        StopAllCoroutines();
    }
    public void ToUnfreezeTank()
    {
        isInMovement = false;
        RandomDirection();
        Invoke("FireWhenWanted", Random.Range(0.5f, 1));     
    }
}
using UnityEngine;


public class Player : CharMovement
{
    private float vertical;
    private float horizontal;
    private Rigidbody2D rb;
    private GunController gc;
    private GameObject immortal;

    void Start()
    {
        gc = GetComponentInChildren<GunController>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gc.Fire();
        }
    }

    private void FixedUpdate()
    {
        if (!isInMovement && 0 != horizontal)
        {
            StartCoroutine(MoveHorizontal(horizontal, rb));
        }
        else if (!isInMovement && 0 != vertical)
        {
            StartCoroutine(MoveVertical(vertical, rb));
        }
    }

}
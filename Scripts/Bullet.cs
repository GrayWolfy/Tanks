using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool destroyConcrete;

    [SerializeField] bool toBeDestroyed;
    
    public int speed = 7;
    Rigidbody2D rb;

    private Vector2 prevPosition;
    private Vector2 nowPosition;
    private Vector2 difference;


    private string bulletDirection;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        prevPosition = rb.position;
        rb.velocity = transform.up * speed;
    }

    void Update()
    {
        nowPosition = rb.position;
        difference = nowPosition - prevPosition;

        if (difference.x == 0 && prevPosition.y < nowPosition.y)
        {
            bulletDirection = "yPlus";
        }
        else if(difference.x == 0 && prevPosition.y > nowPosition.y)
        {
            bulletDirection = "yMinus";
        }
        
        if (difference.y == 0 && prevPosition.x < nowPosition.x)
        {
            bulletDirection = "xPlus";
        }
        else if(difference.y == 0 && prevPosition.x > nowPosition.x)
        {
            bulletDirection = "xMinus";
        }
        
        prevPosition = nowPosition;
    }

    private void OnEnable()
    {
        if (rb != null)
        {
            rb.velocity = transform.up * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector2.zero;

        if (collision.gameObject.GetComponent<TankHealth>() != null)
        {
            collision.gameObject.GetComponent<TankHealth>().TakeDamage();
        }
        
        if (collision.gameObject.CompareTag("Brick") ||
            (destroyConcrete && collision.gameObject.CompareTag("Concrete")))
        {
            TileScript brick = collision.gameObject.GetComponent<TileScript>();
            brick.TakeDamage(bulletDirection);
        }
        
        if (collision.gameObject.CompareTag("Trees"))
        {
           Destroy(collision.gameObject);
        }

        
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (toBeDestroyed)
        {
            Destroy(gameObject);
        }
    }

    public void DestroyBullet()
    {
        if (gameObject.activeSelf == false)
        {
            Destroy(gameObject);
        }

        toBeDestroyed = true;
    }
}
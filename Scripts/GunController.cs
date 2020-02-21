using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    private GameObject canonBall;
    Bullet canon;
    [SerializeField] int speed;

    void Start()
    {
        canonBall = Instantiate(projectile, transform.position, transform.rotation);
        canon = canonBall.GetComponent<Bullet>();
        canon.speed = speed;
        canonBall.SetActive(false);
    }

    public void Fire()
    {
        if (canonBall.activeSelf == false)
        {
            canonBall.transform.position = transform.position;
            canonBall.transform.rotation = transform.rotation;
            canonBall.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        if (canonBall != null) canon.DestroyBullet();
    }

    public void SetSpeed()
    {
        speed += 10;
    }
}
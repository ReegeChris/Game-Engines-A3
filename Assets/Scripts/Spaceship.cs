using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public Projectile bulletPrefab;
    public Transform bulletLocation;
    public Rigidbody rb;

    public float speed, maxVelocity;
    public int lives; 

    protected float sqrMaxVelocity;
    public bool canShoot = true;

    // Start is called before the first frame update
    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
        sqrMaxVelocity = maxVelocity * maxVelocity;
    }

    protected void Shoot()
        {
        if (!(canShoot = true))
            {
            return;
            }
      
        Quaternion quat = Quaternion.Euler(0, 90, 90);
      
        // Projectile projectile = Instantiate(this.bulletPrefab, bulletLocation.transform.position, quat);
        
        //Bullet projectile is acquired from the pool 
       Projectile projectile = ProjectileObjectPool.Instance.GetFromPool();

        //Set bullet object from pool to the bulletLocation variable
       projectile.transform.position = bulletLocation.position;

        projectile.destroyed += LaserDestroyed;
        
        canShoot = false;

        Debug.Log("Shoot");
        }

    public void LaserDestroyed()
        {
        canShoot = true;
        }

    }

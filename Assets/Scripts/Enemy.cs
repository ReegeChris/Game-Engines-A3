using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Spaceship
	{

	public float minVelocity;


	void Awake()
	{
		float temp = Random.Range(minVelocity, maxVelocity);
		rb.velocity = new Vector3(0.0f, -temp, 0.0f);
	}


	public void OnTriggerEnter(Collider other)
		{
		if (other.gameObject.CompareTag("TopBoundary"))
			{ return; }
	
		if (other.gameObject.CompareTag("Boundary") || (other.gameObject.CompareTag("Player")))
			{

			//Call the object pool class and create an instance of the enemy object after colliding with the player.
			//Once the instance is created, the AddToPool function is called, adding the object to the queue
			ObjectPool.Instance.AddToPool(gameObject);
		
			}
	
		if (other.gameObject.CompareTag("PlayerBullet"))
			{
			if (lives == 0) 
			{
				Destroy(this.gameObject);
			} else {
				lives -= 1;
			}
		}

	}

}

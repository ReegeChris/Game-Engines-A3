using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Spaceship
	{

	public float minVelocity;

	//Action to track the total number of enemies destroyed
	public static event System.Action<string> enemiesDestroyed;

	
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
				//Counter is incremented each time an enemy ship is destroyed
				EnemySpawner.enemyDestroyedCount++;

				//Enemiesdestroyed action is invoked each time the enmey ship is destroyed
				enemiesDestroyed?.Invoke("Total Enemies destroyed:" + EnemySpawner.enemyDestroyedCount);


				Destroy(this.gameObject);

			}
			
			else {
				
				lives -= 1;
			}
		}

	}

}

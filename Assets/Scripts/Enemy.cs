using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Spaceship
	{

	public float minVelocity;

	//Action to track the total number of enemies destroyed
	public static event System.Action<string> enemiesDestroyed;

	//Implement Observer pattern action
	// Code referenced from Parisa's Lecture 4 Videos: https://drive.google.com/file/d/1mKuH4BzcJgqX2wQFOKWYbX6r7i3cS7mQ/view
	public static event System.Action deathSoundEffect;

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

				//deathSoundEffect Action is invoked each time a ship is destroyed
				deathSoundEffect?.Invoke();

				//Enemy Ship added to object pool after a bullet is shot at it
				//ObjectPool.Instance.AddToPool(this);


				Destroy(this.gameObject);

			}
			
			else {
				
				lives -= 1;
			}
		}

	}

}

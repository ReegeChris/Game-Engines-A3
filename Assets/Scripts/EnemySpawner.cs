using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class EnemySpawner : MonoBehaviour
	{
	public float min__X = -1056f, max__X = 1056;
	public int min_Size = 10, max_Size = 30;

	public Projectile asteroidPrefab;
	public GameObject enemyPrefab;
	public string sceneName;

	public TextMeshProUGUI WaveText;
	public float spawnTimer; 
	public float waveTimer;
	public int wave = 1;

	//Enemy counter used with queue and decoupling action
	public static int enemyDestroyedCount = 0;

	public static int enemyLimit=1, waveTotal=1;
	private int enemyCount;

	public Canvas menu;
	public TextMeshProUGUI enemyText;
	public TextMeshProUGUI waveCountText;

	[DllImport("A2Plugin")]
	private static extern int randomScale(float i1, float i2);

	//Create a variable to store audio clip
	private AudioSource _BGAudioSource;

	private void Awake()
    {
		//_audioSource gets the 'AudioSource'  component attached to the 'Music' gameobject.
		_BGAudioSource = GetComponent<AudioSource>();
	}

	// Start is called before the first frame update
	void Start()		{
		menu.enabled = true;
		enemyLimit = 1;
		waveTotal = 1;
		while (CommandInvoker.commandHistory.Count > CommandInvoker.counter)
		{
			CommandInvoker.commandHistory.RemoveAt(CommandInvoker.counter);
		}
		CommandInvoker.counter = 0;

	}
	private void Update()
    {
		if (menu.enabled == true)
        {
			enemyText.text = "Enemy Count: " + enemyLimit;
			waveCountText.text = "Total Waves: " + EnemySpawner.waveTotal;
		}

	}
    public void StartGame()
    {
		menu.enabled = false;
		//Invoke("SpawnEnemies", spawnTimer);
		Invoke("SpawnEnemiesFromPool", spawnTimer);
		Invoke("nextWave", waveTimer);

	}

	void nextWave()
		{

		if (wave < waveTotal)
			{
			wave += 1;
			WaveText.text = "Wave " + wave;
			enemyCount = 0;
			enemyLimit++;
			//Invoke("SpawnEnemies", spawnTimer);
			Invoke("SpawnEnemiesFromPool", spawnTimer);
			Invoke("nextWave", waveTimer);
			}
		else
			{SceneManager.LoadScene("YouWon");}
		}


	public void FoeUP()
    {
		ICommand foePlus = new EnemyUp();
		CommandInvoker.AddCommand(foePlus);

	}
	public void FoeDown()
	{
		CommandInvoker.UndoCommand();

	}
	public void WaveUP()
	{
		ICommand wavePlus = new WavesUp();
		CommandInvoker.AddCommand(wavePlus);

	}
	public void WaveDown()
	{
		CommandInvoker.UndoCommand();

	}
	//void SpawnEnemies()
	//{

	//	float pos__X = Random.Range(min__X, max__X);
	//	Vector3 spawnLocation = transform.position;

	//	spawnLocation.x = pos__X;
	//	if (enemyCount < enemyLimit)
	//	{
	//		if (Random.Range(0, 2) > 0)
	//		{
	//			Projectile asteroid = Instantiate(asteroidPrefab, spawnLocation, Quaternion.identity);
	//			int rand = randomScale(min_Size, max_Size); // Random.Range(min_Size, max_Size);
	//			asteroid.transform.localScale = new Vector3(rand, rand, rand);
	//			Invoke("SpawnEnemies", spawnTimer);
	//		}
	//		else
	//		{
	//			Instantiate(enemyPrefab, spawnLocation, Quaternion.Euler(0f, -90f, 90f));
	//			Invoke("SpawnEnemies", spawnTimer);
	//		}
	//		enemyCount++;
	//	}
	//}

	//Function Similar to spawn enemies, gets objects from the queue in the ObjectPool class
	void SpawnEnemiesFromPool()
	{

		float pos__X = Random.Range(min__X, max__X);
		Vector3 spawnLocation = transform.position;

		spawnLocation.x = pos__X;
		if (enemyCount < enemyLimit)
		{
			if (Random.Range(0, 2) > 0)
			{
				//Asteroid variable gets instance of the asteroid prefab from the asteroid object pool
				Projectile asteroid = AsteroidObjectPool.Instance.GetFromPool();

				asteroid.transform.position = spawnLocation;
				
				int rand = randomScale(min_Size, max_Size); // Random.Range(min_Size, max_Size);
				asteroid.transform.localScale = new Vector3(rand, rand, rand);
				Invoke("SpawnEnemiesFromPool", spawnTimer);
			}
			else
			{
				//Enemey variable gets instance of the enemy object from the object pool
				var enemy = ObjectPool.Instance.GetFromPool();

				//Enemy variable has it's position = to the spawn Location position. This also randomly spawns in each enemy
				enemy.transform.position = spawnLocation;
				Invoke("SpawnEnemiesFromPool", spawnTimer);
			}
			enemyCount++;
		}
	}
}


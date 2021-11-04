using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileObjectPool : MonoBehaviour
{
    //Private variable to store specific prefab
    [SerializeField]
    private Projectile prefab;

    private Queue<Projectile> deactivatedObjects = new Queue<Projectile>();

    public static ProjectileObjectPool Instance { get; private set; }

    //Awake function used to create an instance of each prefab
    //The GrowPool function is called to add prefabs into the pool once an instance of them has been created
    private void Awake()
    {
        Instance = this;
        IncreaseObjectPool();

    }

    public Projectile GetFromPool()
    {
        if (deactivatedObjects.Count == 0)
        {
            IncreaseObjectPool();
        }

        //Object is taken out from the queue and the instance is set to true
        var instance = deactivatedObjects.Dequeue();
        instance.gameObject.SetActive(true);
        return instance;
    }

    private void IncreaseObjectPool()
    {
        for (int i = 0; i < EnemySpawner.enemyLimit; i++)
        {
            var InstanceToAdd = Instantiate(prefab);

            InstanceToAdd.transform.SetParent(transform);

            AddToPool(InstanceToAdd);
        }
    }

    public void AddToPool(Projectile instance)
    {
        instance.gameObject.SetActive(false);
        deactivatedObjects.Enqueue(instance);

    }
}

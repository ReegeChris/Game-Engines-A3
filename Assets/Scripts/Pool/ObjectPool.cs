using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code for the object pool design pattern was referenced from Parisa's Lecture 6 video: https://drive.google.com/file/d/1INnjZ2ANi6TWEA-OkGgZZFC-tb6XpDCu/view

public class ObjectPool : MonoBehaviour
{

    //Private variable to store specific prefab
    [SerializeField]
    private GameObject prefab;

    private Queue<GameObject> deactivatedObjects = new Queue<GameObject>();

    public static ObjectPool Instance { get; private set; }

    //Awake function used to create an instance of each prefab
    //The GrowPool function is called to add prefabs into the pool once an instance of them has been created
    private void Awake()
    {
        Instance = this;
        IncreaseObjectPool();

    }

    //If there
    public GameObject GetFromPool()
    {
        if(deactivatedObjects.Count == 0)
        {
            IncreaseObjectPool();
        }

        //Object is taken out from the queue and the instance is set to true
        var instance = deactivatedObjects.Dequeue();
        instance.SetActive(true);
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
    
    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        deactivatedObjects.Enqueue(instance);

    }

    
    
}

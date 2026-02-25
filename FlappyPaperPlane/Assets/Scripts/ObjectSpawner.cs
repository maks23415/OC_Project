using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public float spawnCD;
    public GameObject[] objectsVariants;

    private float startSpawnCD;

    private void Start()
    {
        startSpawnCD = spawnCD;
    }

    private void Update()
    {
        if (spawnCD <= 0)
        {
            // Выбираем случайный объект
            int randomVariant = Random.Range(0, objectsVariants.Length);

            // Создаем позицию для спавна
            Vector3 spawnPosition = new Vector3(
                transform.position.x,
                objectsVariants[randomVariant].transform.position.y,
                transform.position.z
            );

            // Создаем объект
            Instantiate(objectsVariants[randomVariant], spawnPosition, Quaternion.identity);

            // Уменьшаем время спавна
            spawnCD = startSpawnCD;
        }
        else
        {
            spawnCD -= Time.deltaTime;
        }
    }
}
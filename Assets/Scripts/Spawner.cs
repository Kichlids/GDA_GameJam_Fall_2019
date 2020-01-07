using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawner1;     // Top platform top spawner
    public GameObject spawner2;     // Top platform bottom spawner
    public GameObject spawner3;     // Bottom platform top spawner
    public GameObject spawner4;     // Bottom platform bottom spawner

    private GameObject nextSpawner;

    public GameObject largeObstacle;
    public GameObject smallObstacle;
    public GameObject smallBonus;
    private GameObject nextObstacle;

    public float maxTime = 4f;      // Maximum time between spawns
    public float minTime = 2f;      // Minimum time between spawns
    private float time;             // Current time
    private float spawnTime;        // Time for next spawn

    private float obstacleMoveSpeed;

    public float numObstaclesSpawned;

    public enum Level { Level1, Level2, Level3, Level4, Level5 };
    public Level level;

    private void Start()
    {
        SetRandomTime();
        SetRandomObstacle();
        SetRandomSpawner();
        time = 0;
        numObstaclesSpawned = 0;
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;

        UpdateLevel();
        SetSpawnSetting();

        if (time >= spawnTime)
        {
            SpawnObstacle();

            SetRandomTime();
            SetRandomObstacle();
            SetRandomSpawner();
            time = 0;
        }
    }

    private void UpdateLevel()
    {
        if (numObstaclesSpawned < 10)
        {
            level = Level.Level1;
        }
        else if (numObstaclesSpawned < 20)
        {
            level = Level.Level2;
        }
        else if (numObstaclesSpawned < 30)
        {
            level = Level.Level3;
        }
        else if (numObstaclesSpawned < 40)
        {
            level = Level.Level4;
        }
        else
        {
            level = Level.Level5;
        }
    }

    private void SetSpawnSetting()
    {
        if (level == Level.Level1)
        {
            obstacleMoveSpeed = 1f;
            maxTime = 4f;
            minTime = 2f;
        }
        else if (level == Level.Level2)
        {
            obstacleMoveSpeed = 2f;
            maxTime = 3f;
            minTime = 2f;
        }
        else if (level == Level.Level3)
        {
            obstacleMoveSpeed = 3f;
            maxTime = 2f;
            minTime = 2f;
        }
        else if (level == Level.Level4)
        {
            obstacleMoveSpeed = 4f;
            maxTime = 2f;
            minTime = 1f;
        }
        else if (level == Level.Level5)
        {
            obstacleMoveSpeed = 5f;
            maxTime = 1f;
            minTime = 1f;
        }
    }

    private void SpawnObstacle()
    {
        time = 0;
        GameObject obstacle = Instantiate(nextObstacle, nextSpawner.transform.position, nextSpawner.transform.rotation) as GameObject;
        if (nextSpawner == spawner1 || nextSpawner == spawner2)
        {
            obstacle.GetComponent<Rigidbody>().velocity = new Vector3(-obstacleMoveSpeed, 0, 0);
        }
        else
        {
            obstacle.GetComponent<Rigidbody>().velocity = new Vector3(obstacleMoveSpeed, 0, 0);
        }
        numObstaclesSpawned++;
    }

    private void SetRandomSpawner()
    {
        float spawner = Random.Range(0, 4);
        if (spawner < 1)
        {
            nextSpawner = spawner1;
        }
        else if (spawner < 2)
        {
            nextSpawner = spawner2;
        }
        else if (spawner < 3)
        {
            nextSpawner = spawner3;
        }
        else
        {
            nextSpawner = spawner4;
        }
    }

    private void SetRandomObstacle()
    {
        float obstacle = Random.Range(0, 4);    // small:large:small = 1:2:1 ratio
        if (obstacle < 1)
        {
            nextObstacle = smallObstacle;
        }
        else if (obstacle < 3)
        {
            nextObstacle = largeObstacle;
        }
        else
        {
            nextObstacle = smallBonus;
        }
    }

    private void SetRandomTime()
    {
        spawnTime = Random.Range(minTime, maxTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    [SerializeField] GameObject[] spawnPointsCloud;
    [SerializeField] GameObject[] prefabsCloud;

    float minX = -4.0f;
    float maxX = 4.0f;
    float minY = -5.0f;
    float maxY = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init()
    {
        // generate random Vector2 value
        for (int i = 0; i < prefabsCloud.Length * 2; i++)
        {
            Vector2 randomVector = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            SpawnCloud(randomVector);
        }

        InvokeRepeating("SpawnCloud", 0.0f, 0.5f);
    }

    void SpawnCloud()
    {
        // select random Cloud prefab
        int randomCloud = Random.Range(0, prefabsCloud.Length);
        int randomSpawnPoint = Random.Range(0, spawnPointsCloud.Length);

        Instantiate(
            prefabsCloud[randomCloud],
            spawnPointsCloud[randomSpawnPoint].transform.position,
            Quaternion.identity
            );
    }

    // spawn Clouds at random time on random spawnPoint
    void SpawnCloud(Vector2 pos)
    {
        // select random Cloud prefab
        int randomCloud = Random.Range(0, prefabsCloud.Length);
        int randomSpawnPoint = Random.Range(0, spawnPointsCloud.Length);

        Instantiate(
            prefabsCloud[randomCloud],
            pos,
            Quaternion.identity
            );
    }
}

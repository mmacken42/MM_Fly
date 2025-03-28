using UnityEngine;
using System.Collections.Generic;

public class SpawnObstacles : MonoBehaviour
{
    //obstacle prefabs
    public GameObject obstaclePrefab_VeryEasy;
    public GameObject obstaclePrefab_Easy;
    public GameObject obstaclePrefab_Medium;
    public GameObject obstaclePrefab_Hard;
    private List<GameObject> prefabList = new List<GameObject>();

    //spawning those prefabs
    public float spawnRate = 2f;
    public float heightOffset = 10f;
    private float spawnTimer = 0f;
    int prefabIndexMin = 0;
    int prefabIndexMax = 4;

    //game logic ref
    private FlyingGameLogicManager gameLogicManager;

    void Start()
    {
        gameLogicManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<FlyingGameLogicManager>();

        //setup our list of available obstacle prefabs
        prefabList.Add(obstaclePrefab_VeryEasy);
        prefabList.Add(obstaclePrefab_Easy);
        prefabList.Add(obstaclePrefab_Medium);
        prefabList.Add(obstaclePrefab_Hard);

        //spawn one right away so we don't need to wait for first timer to elapse
        SpawnNewObstacle();
    }
    
    void Update()
    {
        //spawn a new obstacle each time our timer reaches threshold value
        if (spawnTimer < spawnRate)
        {
            spawnTimer += Time.deltaTime;
        }
        else
        {
            SpawnNewObstacle();
            spawnTimer = 0f;
        }
    }

    void SpawnNewObstacle()
    {
        /*
            0 = easiest obstacle
            3 = most difficult obstacle
            use current player score to determine the range of prefabs we select from
            this causes difficulty to increase as the player score gets higher
        */
        switch (gameLogicManager.GetPlayerScore())
        {
            case int n when (n <= 5):
                prefabIndexMin = 0;
                prefabIndexMax = 2;
                break;

            case int n when (n > 5 && n <= 10):
                prefabIndexMin = 0;
                prefabIndexMax = 3;
                break;

            case int n when (n > 10 && n <= 20):
                prefabIndexMin = 0;
                prefabIndexMax = 4;
                break;

            case int n when (n > 20 && n <= 30):
                prefabIndexMin = 1;
                prefabIndexMax = 4;
                break;

            case int n when (n > 30):
                prefabIndexMin = 2;
                prefabIndexMax = 4;
                break;
        }
        //now that we have our difficulty-based range, we choose a random index from that range
        int chosenPrefabIndex = UnityEngine.Random.Range(prefabIndexMin, prefabIndexMax);
        //now we know which prefab we want to spawn next
        GameObject chosenPrefab = prefabList[chosenPrefabIndex];

        //randomize the height of our chosen prefab
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        //finally, we spawn our next obstacle
        Instantiate(chosenPrefab, 
                    new Vector3(transform.position.x, UnityEngine.Random.Range(lowestPoint, highestPoint), 0), 
                    transform.rotation);
    }
}

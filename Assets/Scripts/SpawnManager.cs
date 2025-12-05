using UnityEngine;

public class SpawnManager : MonoBehaviour

{
    public GameObject alienPrefabs;
    public GameObject powerupPrefab;

    private float spawnRangeX = 200.0f; // ENCAPSULATION 
    private float startDelay = 2.0f; // ENCAPSULATION 
    private float spawnInterval = 1.5f; // ENCAPSULATION 
    public float alienSpeed = 8.0f;
    private float powerupSpawnDelay = 10.0f; // ENCAPSULATION 
    private float powerupSpawnInterval = 10.0f; // ENCAPSULATION 
    private GameObject player; // ENCAPSULATION
     
    void Start()

    {
        player = GameObject.Find("Player");
    }

    public void StartSpawning() // ABSTRACTION 
    {
        InvokeRepeating("SpawnAlien", startDelay, spawnInterval);
        InvokeRepeating("SpawnPowerup", powerupSpawnDelay, powerupSpawnInterval);
    }

    void SpawnAlien()
    {
        if (player == null) return; 

        float spawnDistance = Random.Range(100.0f, 150.0f);
        float spawnZ = player.transform.position.z + spawnDistance; 

        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 15, spawnZ);

        Instantiate(alienPrefabs, spawnPos, alienPrefabs.transform.rotation);

    }
    public void IncreaseAlienSpeed() // ABSTRACTION 

    {
        alienSpeed += 90.0f;
        Debug.Log("Alien Speed Increased to: " + alienSpeed);
    }
    public void SpawnPowerup()
    {
        {
            if (player == null) return;

            float spawnDistance = Random.Range(50.0f, 100.0f);
            float spawnZ = player.transform.position.z + spawnDistance;

            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 15, spawnZ);

            Instantiate(powerupPrefab, spawnPos, powerupPrefab.transform.rotation);
        }
    }
    public void StopSpawningAndClearScene()

    {
        CancelInvoke("SpawnAlien");
        CancelInvoke("SpawnPowerup");

        GameObject[] aliens = GameObject.FindGameObjectsWithTag("Alien");
        foreach (GameObject alien in aliens)

        {
            Destroy(alien);
        }
        GameObject[] powerups = GameObject.FindGameObjectsWithTag("Powerup");

        foreach (GameObject powerup in powerups)

        {
            Destroy(powerup);
        }

    }

}

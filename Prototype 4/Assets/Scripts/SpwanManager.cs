using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpwanManager : MonoBehaviour
{
    // Prefab references for the enemies and powerups
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    // Number of enemies currently in the scene
    public int enemyCount;

    // The current wave number, starts at 1
    public int waveNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn the first wave of enemies and a powerup at the start
        SpwanEnemyWave(waveNumber);
        GeneratePowerup();
    }

    // Update is called once per frame
    void Update()
    {
        // Count the number of enemies in the scene
        enemyCount = FindObjectsOfType<Enemy>().Length;

        // Check if there are no enemies left, meaning the wave is complete
        if (enemyCount == 0)
        {
            // Increase wave number and spawn a new wave of enemies
            waveNumber++;
            SpwanEnemyWave(waveNumber);

            // Generate a new powerup after the wave is cleared
            GeneratePowerup();
        }
    }

    // Method to spawn a wave of enemies
    void SpwanEnemyWave(int enemiesToSpwan)
    {
        // Loop through the number of enemies to spawn
        for (int i = 0; i < enemiesToSpwan; i++)
        {
            // Instantiate each enemy at a random spawn position
            Instantiate(enemyPrefab, GenerateSpwanPosition(), enemyPrefab.transform.rotation);
        }
    }

    // Method to generate a powerup at a random position
    void GeneratePowerup()
    {
        // Instantiate the powerup at a random spawn position
        Instantiate(powerupPrefab, GenerateSpwanPosition(), powerupPrefab.transform.rotation);
    }

    // Method to generate a random spawn position within a specified range
    private Vector3 GenerateSpwanPosition()
    {
        // Generate random X and Z positions between -9 and 9
        float spawnPosX = Random.Range(-9, 9);
        float spawnPosz = Random.Range(-9, 9);

        // Return the random position as a Vector3
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosz);

        return randomPos;
    }
}

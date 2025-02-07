using System.Collections;
using pixelook;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private PowerUp[] powerUps;
    [SerializeField] private float spawnRate = 5f;
    [SerializeField] private int maxPowerUps = 3;
    
    [SerializeField] private float xRange = 9f;
    [SerializeField] private float yRange = 6f;
    
    private bool _isSpawning;

    private void OnEnable()
    {
        EventManager.AddListener(Events.LEVEL_READY, OnGameReady);
        EventManager.AddListener(Events.GAME_OVER, OnGameOver);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.LEVEL_READY, OnGameReady);
        EventManager.RemoveListener(Events.GAME_OVER, OnGameOver);
    }
    
    private void OnGameReady()
    {
        if (_isSpawning) return;
        
        _isSpawning = true;
        
        StartCoroutine(WaitAndSpawn());
    }
    
    private void OnGameOver()
    {
        StopAllCoroutines();
    }
    
    IEnumerator WaitAndSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);

            if (!GameState.IsGameRunning) yield break;
            
            SpawnPowerUp();
        }
    }

    void SpawnPowerUp()
    {
        if (GameState.PowerUpsCount >= maxPowerUps) return;

        var powerUp = powerUps[Random.Range(0, powerUps.Length)];
        var spawnPosition = new Vector3(Random.Range(-xRange, xRange), 0.5f, Random.Range(-yRange, yRange));
        
        Instantiate(powerUp, spawnPosition, Quaternion.identity);
    }
}

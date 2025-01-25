using System;
using pixelook;
using UnityEngine;

public enum PowerUpType
{
    Speed,
    Shield,
    HeavyWeigh
}

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpType powerUpType;
    
    public PowerUpType PowerUpType => powerUpType;

    private void Start()
    {
        GameState.PowerUpsCount++;
    }

    private void OnTriggerEnter(Collider other)
    {
        var playerPowerUp = other.GetComponentInParent<PlayerPowerUp>();
        if (playerPowerUp == null) return;
        
        switch (powerUpType)
        {
            case PowerUpType.Speed:
                playerPowerUp.ActivateSpeedUp();
                break;
            case PowerUpType.Shield:
                playerPowerUp.ActivateShield();
                break;
            case PowerUpType.HeavyWeigh:
                // TODO: implement heavy weigh power up
                break;
        }

        GameState.PowerUpsCount--;
        
        // TODO: spawn particles
        
        Destroy(gameObject);
    }
}

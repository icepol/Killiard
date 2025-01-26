using System;
using System.Collections;
using pixelook;
using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpDisplay powerUpDisplay;
    
    [SerializeField] float speedupMultiplier = 1.5f;
    [SerializeField] float powerUpDuration = 5f;
    
    private PlayerController _playerController;
    private Player _player;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        EventManager.AddListener(Events.GAME_OVER, OnGameOver);
    }

    private void Start()
    {
        Reset();
    }

    public void ActivateSpeedUp()
    {
        Reset();
        
        _playerController.CurrentForce *= speedupMultiplier;
        _player.LiveCam.SetAngryFace();
        
        powerUpDisplay.SetSpeedPowerUp();

        StartCoroutine(WaitAndReset());
    }
    
    public void ActivateShield()
    {
        Reset();

        _player.IsShieldActive = true;
        _player.LiveCam.SetAngryFace();
        
        powerUpDisplay.SetShieldPowerUp();

        StartCoroutine(WaitAndReset());
    }
    
    private void OnDisable()
    {
        EventManager.RemoveListener(Events.GAME_OVER, OnGameOver);
    }

    private void Reset()
    {
        StopAllCoroutines();
        
        _playerController.CurrentForce = _playerController.DefaultForce;
        _player.IsShieldActive = false;
        
        powerUpDisplay.Reset();
    }

    IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(powerUpDuration);
        
        Reset();
    }
    
    private void OnGameOver()
    {
        Reset();
    }
}

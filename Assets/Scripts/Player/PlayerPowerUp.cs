using System.Collections;
using pixelook;
using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
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
    
    public void ActivateSpeedUp()
    {
        Reset();
        
        _playerController.CurrentForce *= speedupMultiplier;

        StartCoroutine(WaitAndReset());
    }
    
    public void ActivateShield()
    {
        Reset();

        _player.IsShieldActive = true;

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

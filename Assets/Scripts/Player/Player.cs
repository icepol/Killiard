using System;
using pixelook;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private string playerName;
    [SerializeField] private Player enemy;

    private Rigidbody _rigidbody;
    
    public bool IsShieldActive { get; set; }
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    private void OnEnable()
    {
        EventManager.AddListener(Events.GAME_STARTED, OnGameStarted);
    }

    private void Update()
    {
        if (!GameState.IsGameRunning) return;
        
        transform.LookAt(enemy.transform);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.GAME_STARTED, OnGameStarted);
    }

    private void OnGameStarted()
    {
        if (GameState.IsLevelReady) return;
        
        GameState.IsLevelReady = true;
        
        EventManager.TriggerEvent(Events.LEVEL_READY);
    }

    private void OnCollisionEnter(Collision other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player != null) OnPlayerContact();
        
        var mantinel = other.gameObject.GetComponent<Mantinel>();
        if (mantinel != null) OnMantinelContact();
        
        var ball = other.gameObject.GetComponent<Ball>();
        if (ball != null) OnBallContact();
    }

    private void OnTriggerEnter(Collider other)
    {
        Hole hole = other.GetComponent<Hole>();
        if (hole != null) OnHoleContact();
    }
    
    private void OnHoleContact()
    {
        if (IsShieldActive) return;
        
        GameState.DeadPlayerName = playerName;
        
        EventManager.TriggerEvent(Events.GAME_OVER);
        
        Destroy(gameObject);
    }

    private void OnPlayerContact()
    {
        EventManager.TriggerEvent(_rigidbody.linearVelocity.magnitude < 5f
            ? Events.PLAYER_CONTACT_SLOW
            : Events.PLAYER_CONTACT_FAST);
    }
    
    private void OnMantinelContact()
    {
        EventManager.TriggerEvent(Events.PLAYER_CONTACT_MANTINEL);
    }
    
    private void OnBallContact()
    {
        EventManager.TriggerEvent(Events.PLAYER_CONTACT_BALL);
    }
}

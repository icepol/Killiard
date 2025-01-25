using pixelook;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] string playerName;

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
    
    private void OnDisable()
    {
        EventManager.RemoveListener(Events.GAME_STARTED, OnGameStarted);
    }

    private void OnGameStarted()
    {
        if (GameState.IsLevelReady) return;
        
        EventManager.TriggerEvent(Events.LEVEL_READY);
    }

    private void OnCollisionEnter(Collision other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player != null) OnPlayerContact();
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
        print(_rigidbody.linearVelocity.magnitude);
        
        if (_rigidbody.linearVelocity.magnitude < 5f) return;
        
        EventManager.TriggerEvent(Events.PLAYER_CONTACT);
    }
}

using pixelook;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private string playerName;
    [SerializeField] private Player enemy;
    
    [SerializeField] private ParticleSystem playerDiedParticles;
    [SerializeField] private LiveCam liveCam;

    private Rigidbody _rigidbody;
    
    public bool IsShieldActive { get; set; }
    public LiveCam LiveCam => liveCam;
    
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
        
        // Instantiate(playerDiedParticles, transform.position, Quaternion.identity);
        
        EventManager.TriggerEvent(Events.GAME_OVER);
        
        Destroy(gameObject);
    }

    private void OnPlayerContact()
    {
        var isFastHit = _rigidbody.linearVelocity.magnitude > 2.5f;
        
        EventManager.TriggerEvent( isFastHit
            ? Events.PLAYER_CONTACT_FAST
            : Events.PLAYER_CONTACT_SLOW);

        OnFastHit();
    }
    
    private void OnMantinelContact()
    {
        EventManager.TriggerEvent(Events.PLAYER_CONTACT_MANTINEL);
        
        OnFastHit();
    }
    
    private void OnBallContact()
    {
        EventManager.TriggerEvent(Events.PLAYER_CONTACT_BALL);
        
        OnFastHit();
    }
    
    private void OnFastHit()
    {
        var isFastHit = _rigidbody.linearVelocity.magnitude > 2.5f;

        if (isFastHit)
        {
            liveCam.SetBangFace();
        }
    }
}

using System;
using pixelook;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] KeyCode moveUpKey;
    [SerializeField] KeyCode moveDownKey;
    [SerializeField] KeyCode moveLeftKey;
    [SerializeField] KeyCode moveRightKey;
    
    [SerializeField] float force = 5f;
    [SerializeField] float currentForce;
    
    [SerializeField] ParticleSystem dustParticles;
    [SerializeField] float dustSpawnRate = 0.25f;
    
    [SerializeField] ParticleSystem dustFastParticles;
    [SerializeField] float dustFastSpawnRate = 0.25f;

    private Rigidbody _rigidbody;
    private bool _isMovementEnabled;
    private float _nextDustSpawnTime;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        EventManager.AddListener(Events.LEVEL_READY, OnGameReady);
        EventManager.AddListener(Events.GAME_OVER, OnGameOver);
    }

    private void Start()
    {
        currentForce = force;
    }

    void Update()
    {
        if (!_isMovementEnabled) return;
        
        HandleControls();
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(Events.LEVEL_READY, OnGameReady);
        EventManager.RemoveListener(Events.GAME_OVER, OnGameOver);
    }
    
    public void OnGameReady()
    {
        _isMovementEnabled = true;
    }
    
    public void OnGameOver()
    {
        _isMovementEnabled = false;
        _rigidbody.linearVelocity = Vector3.zero;
    }

    private void HandleControls()
    {
        bool isMoving = false;
        Vector3 forceToApply = new Vector3();
        
        if (Input.GetKey(moveUpKey))
        {
            forceToApply.z += currentForce * Time.deltaTime;
            isMoving = true;
        }
        
        if (Input.GetKey(moveDownKey))
        {
            forceToApply.z -= currentForce * Time.deltaTime;
            isMoving = true;
        }
        
        if (Input.GetKey(moveLeftKey))
        {
            forceToApply.x -= currentForce * Time.deltaTime;
            isMoving = true;
        }
        
        if (Input.GetKey(moveRightKey))
        {
            forceToApply.x += currentForce * Time.deltaTime;
            isMoving = true;
        }

        if (!isMoving) return;
        
        _rigidbody.AddForce(forceToApply, ForceMode.VelocityChange);
        SpawnDust();
    }
    
    private void SpawnDust()
    {
        if (_nextDustSpawnTime > 0)
        {
            _nextDustSpawnTime -= Time.deltaTime;
            return;
        }
        
        var isFast = _rigidbody.linearVelocity.magnitude > 2.5f;

        if (isFast)
        {
            Instantiate(dustFastParticles, transform.position, Quaternion.identity);
            _nextDustSpawnTime = dustFastSpawnRate;
        }
        else
        {
            Instantiate(dustParticles, transform.position, Quaternion.identity);
            _nextDustSpawnTime = dustSpawnRate;
        }
    }
    
    public float DefaultForce
    {
        get => force;
    }
    
    public float CurrentForce
    {
        get => currentForce;
        set => currentForce = value;
    }
}

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
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float speed;
    
    [SerializeField] ParticleSystem dustParticles;
    [SerializeField] float dustSpawnRate = 0.25f;

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
            forceToApply.z += force * Time.deltaTime;
            isMoving = true;
        }
        
        if (Input.GetKey(moveDownKey))
        {
            forceToApply.z -= force * Time.deltaTime;
            isMoving = true;
        }
        
        if (Input.GetKey(moveLeftKey))
        {
            forceToApply.x -= force * Time.deltaTime;
            isMoving = true;
        }
        
        if (Input.GetKey(moveRightKey))
        {
            forceToApply.x += force * Time.deltaTime;
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

        Instantiate(dustParticles, transform.position, Quaternion.identity);
        
        _nextDustSpawnTime = dustSpawnRate;
    }
}

using System;
using pixelook;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] string playerName;
    
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
        EventManager.TriggerEvent(Events.LEVEL_READY);
    }

    private void OnTriggerEnter(Collider other)
    {
        Hole hole = other.GetComponent<Hole>();
        if (hole == null) return;
        
        GameState.DeadPlayerName = playerName;
        
        EventManager.TriggerEvent(Events.GAME_OVER);
        
        Destroy(gameObject);
    }
}

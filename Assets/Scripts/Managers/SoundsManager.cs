using UnityEngine;

namespace pixelook
{
    public class SoundsManager : MonoBehaviour
    {
        [SerializeField] private Transform targetTransform;
        
        [SerializeField] private AudioClip playerHitMantinelSound;
        [SerializeField] private AudioClip ballHitMantinelSound;
        [SerializeField] private AudioClip playerHitPlayerSlowSound;
        [SerializeField] private AudioClip playerHitPlayerFastSound;
        [SerializeField] private AudioClip playerHitBallSound;
        [SerializeField] private AudioClip powerUpSounds;
        [SerializeField] private AudioClip playerDiedSound;

        private void OnEnable()
        {
            EventManager.AddListener(Events.GAME_OVER, OnGameOver);
            EventManager.AddListener(Events.PLAYER_CONTACT_MANTINEL, OnPlayerHitMantinel);
            EventManager.AddListener(Events.PLAYER_CONTACT_SLOW, OnPlayerHitPlayerSlow);
            EventManager.AddListener(Events.PLAYER_CONTACT_FAST, OnPlayerHitPlayerFast);
            EventManager.AddListener(Events.PLAYER_CONTACT_BALL, OnPlayerHitBall);
            EventManager.AddListener(Events.POWER_UP_COLLECTED, OnPowerUpCollected);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener(Events.GAME_OVER, OnGameOver);
            EventManager.RemoveListener(Events.PLAYER_CONTACT_MANTINEL, OnPlayerHitMantinel);
            EventManager.RemoveListener(Events.PLAYER_CONTACT_SLOW, OnPlayerHitPlayerSlow);
            EventManager.RemoveListener(Events.PLAYER_CONTACT_FAST, OnPlayerHitPlayerFast);
            EventManager.RemoveListener(Events.PLAYER_CONTACT_BALL, OnPlayerHitBall);
            EventManager.RemoveListener(Events.POWER_UP_COLLECTED, OnPowerUpCollected);
        }

        private void OnPlayerHitMantinel()
        {
            if (playerHitMantinelSound && Settings.IsSfxEnabled && GameState.IsGameRunning)
                AudioSource.PlayClipAtPoint(playerHitMantinelSound, targetTransform.position);
        }

        private void OnPlayerHitPlayerSlow()
        {
            if (playerHitPlayerSlowSound && Settings.IsSfxEnabled)
                AudioSource.PlayClipAtPoint(playerHitPlayerSlowSound, targetTransform.position);
        }
        
        private void OnPlayerHitPlayerFast()
        {
            if (playerHitPlayerFastSound && Settings.IsSfxEnabled)
                AudioSource.PlayClipAtPoint(playerHitPlayerFastSound, targetTransform.position);
        }
        
        private void OnPowerUpCollected()
        {
            if (powerUpSounds && Settings.IsSfxEnabled)
                AudioSource.PlayClipAtPoint(powerUpSounds, targetTransform.position);
        }
        
        private void OnPlayerHitBall()
        {
            if (playerHitBallSound && Settings.IsSfxEnabled)
                AudioSource.PlayClipAtPoint(playerHitBallSound, targetTransform.position);
        }
        
        private void OnGameOver()
        {
            if (playerDiedSound && Settings.IsSfxEnabled)
                AudioSource.PlayClipAtPoint(playerDiedSound, targetTransform.position);
        }
    }
}
using UnityEngine;

namespace pixelook
{
    public class CameraShake : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        void OnEnable()
        {
            EventManager.AddListener(Events.GAME_OVER, OnGameOver);
            EventManager.AddListener(Events.PLAYER_CONTACT, OnPlayerContact);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener(Events.GAME_OVER, OnGameOver);
            EventManager.RemoveListener(Events.PLAYER_CONTACT, OnPlayerContact);
        }

        private void OnGameOver()
        {
            _animator.SetTrigger("ShakeBig");
        }
        
        private void OnPlayerContact()
        {
            _animator.SetTrigger("ShakeSmall");
        }
    }
}
using pixelook;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private Text _playerDiedText;
    
    void Start()
    {
        _playerDiedText.text = $"{GameState.DeadPlayerName} died...";
    }
}

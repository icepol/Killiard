using UnityEngine;

public class ArenaSpawner : MonoBehaviour
{
    [SerializeField] private Arena[] arenas;

    private void Start()
    {
        foreach (var arena in arenas)
        {
            arena.gameObject.SetActive(false);
        }
        
        arenas[Random.Range(0, arenas.Length)].gameObject.SetActive(true);
    }
}

using UnityEngine;

public class DelayDestroy : MonoBehaviour
{
    [SerializeField] private float delay = 1f;
    
    void Start()
    {
        Destroy(gameObject, delay);
    }
}

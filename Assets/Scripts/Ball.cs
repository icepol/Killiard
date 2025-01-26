using pixelook;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        var mantinel = other.gameObject.GetComponent<Mantinel>();
        if (mantinel != null) OnMantinelContact();
    }
    
    private void OnMantinelContact()
    {
        EventManager.TriggerEvent(Events.PLAYER_CONTACT_MANTINEL);
    }
}

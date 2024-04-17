using UnityEngine;

public class Powerup : MonoBehaviour
{
    public void OnPickedUp()
    {
        Destroy(gameObject);
    }
}

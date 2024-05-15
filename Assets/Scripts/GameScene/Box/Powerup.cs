using UnityEngine;

/// <summary>
/// Very simple controller script for the powerups.
/// </summary>
public class Powerup : MonoBehaviour
{
    public bool IsPickedUp = false;

    public void OnPickedUp()
    {
        IsPickedUp = true;
        Destroy(gameObject);
    }
}

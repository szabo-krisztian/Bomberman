using UnityEngine;

public class Powerup : MonoBehaviour
{
    public bool IsPickedUp = false;

    public void OnPickedUp()
    {
        IsPickedUp = true;
        Destroy(gameObject);
    }
}

using UnityEngine;

/// <summary>
/// This script is responsible for checking the zombie-player collisions.
/// </summary>
public class PlayerHitbox : MonoBehaviour
{
    /// <summary>
    /// We send a message to our player to die.
    /// </summary>
    /// <param name="collision">Collider of a certain GameObject.</param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            transform.parent.gameObject.SendMessage("Die");
        }
    }
}

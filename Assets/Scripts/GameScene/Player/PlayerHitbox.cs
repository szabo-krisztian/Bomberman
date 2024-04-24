using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            transform.parent.gameObject.SendMessage("Die");
        }
    }
}

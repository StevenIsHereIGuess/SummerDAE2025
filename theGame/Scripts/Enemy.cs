using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;

    void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    public void ApplyKnockback(Vector2 knockbackVector)
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; // Reset current motion
            rb.AddForce(knockbackVector, ForceMode2D.Impulse);
        }
    }
}

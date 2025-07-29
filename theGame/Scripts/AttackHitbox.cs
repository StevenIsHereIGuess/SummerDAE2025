using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] private Vector2 knockbackForce = new Vector2(5f, 2f); // Default attack knockback

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                float direction = transform.position.x < enemy.transform.position.x ? 1f : -1f;
                Vector2 finalKnockback = new Vector2(knockbackForce.x * direction, knockbackForce.y);
                enemy.ApplyKnockback(finalKnockback);
            }
        }
    }

    public void SetKnockback(Vector2 newKnockback)
    {
        knockbackForce = newKnockback;
    }
}
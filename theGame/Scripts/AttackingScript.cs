using UnityEngine;
using System.Collections;

public class AttackingScript : MonoBehaviour
{
    public GameObject attackHitbox;
    public float attackDuration = 0.2f;
    private bool isAttacking = false;

    [Header("Knockback Forces")]
    [SerializeField] private Vector2 neutralKnockback = new Vector2(5f, 2f);
    [SerializeField] private Vector2 downKnockback = new Vector2(2f, 6f);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        // Pick which knockback to use
        Vector2 selectedKnockback = Input.GetKey(KeyCode.DownArrow) ? downKnockback : neutralKnockback;

        // Set knockback on the hitbox
        if (attackHitbox != null)
        {
            AttackHitbox hitboxScript = attackHitbox.GetComponent<AttackHitbox>();
            if (hitboxScript != null)
            {
                hitboxScript.SetKnockback(selectedKnockback);
                attackHitbox.SetActive(true);
            }
        }

        yield return new WaitForSeconds(attackDuration);

        if (attackHitbox != null)
            attackHitbox.SetActive(false);

        isAttacking = false;
    }
}

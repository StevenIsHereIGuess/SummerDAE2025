using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed = 5f;   // How fast the camera follows
    public Vector3 offset = new Vector3(0, 0, -10); // Camera offset (keep Z at -10 for 2D)

    private Transform target;

    void Update()
    {
        if (target == null)
        {
            // Try to find the Player if one exists
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
            else
            {
                return; // No player found, do nothing this frame
            }
        }

        // Smoothly follow the target
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}

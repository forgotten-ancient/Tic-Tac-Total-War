using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Projectile needs a Rigidbody component.");
            enabled = false;
        }
    }

    public void Launch(Vector3 initialVelocity)
    {
        if (rb != null)
        {
            rb.linearVelocity = initialVelocity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Calculate the reflection vector
            Vector3 normal = collision.contacts[0].normal;
            Vector3 reflectedVelocity = Vector3.Reflect(rb.linearVelocity.normalized, normal) * rb.linearVelocity.magnitude;

            // Apply the reflected velocity
            rb.linearVelocity = reflectedVelocity;
        }
    }
}
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float LifeTime = 5f;
    private Rigidbody rb;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Projectile needs a Rigidbody component.");
            enabled = false;
        }
    }

    public void Launch(float initialForce)
    {
        if (rb != null)
        {
            rb.AddForce(transform.forward * initialForce);
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

    void Update()
    {
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0) Destroy(gameObject);
    }
}
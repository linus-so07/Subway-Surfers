using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(-4, 4)] public float value;
    [Range(0, 100)] public float speed;
    public float forwardForce = 20f;

    private bool canMove = true;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        if (canMove)
        {
            MoveForward();

            if (Input.GetButtonDown("Left"))
                MoveSideways(-4);

            if (Input.GetButtonDown("Right"))
                MoveSideways(4);
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            // Apply forward force
            rb.AddForce(transform.forward * forwardForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }

    private void MoveForward()
    {
        // Calculate forward velocity
        Vector3 forwardVelocity = new Vector3(0.05f * speed, 0, 0);
        forwardVelocity = transform.TransformDirection(forwardVelocity); // Convert to world space
        forwardVelocity.y = 0;
        rb.velocity = forwardVelocity;
    }

    private void MoveSideways(float direction)
    {
        // Calculate sideways velocity
        float horizontalVelocity = direction * speed;
        Vector3 sidewaysVelocity = new Vector3(0, 0, horizontalVelocity);
        sidewaysVelocity = transform.TransformDirection(sidewaysVelocity); // Convert to world space
        rb.velocity += sidewaysVelocity;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            Debug.Log("Train collided!");
            canMove = false;
        }
    }
}

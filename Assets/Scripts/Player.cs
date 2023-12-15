using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float hitForceMultiplier = 20f;
    public bool canJump = true;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>();

            Vector3 newVelocity = new Vector3(ballRb.velocity.x, hitForceMultiplier, ballRb.velocity.z);

            ballRb.velocity = newVelocity;

            StartCoroutine(DestroyBallAfterDelay(collision.gameObject));
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody playerRb = GetComponent<Rigidbody>();
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
            playerRb.constraints = RigidbodyConstraints.FreezeRotation; // Freeze rotation
        }
    }

    void Update()
    {
        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Jump());
        }
    }

    IEnumerator DestroyBallAfterDelay(GameObject ball)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(ball);
    }

    IEnumerator Jump()
    {
        canJump = false;

        Rigidbody playerRb = GetComponent<Rigidbody>();
        playerRb.AddForce(Vector3.up * 7f, ForceMode.Impulse);

        yield return new WaitForSeconds(0.5f);

        canJump = true;
    }
}

using UnityEngine;

public class DropOnDoorOpen : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator doorAnimator;   // drag the Door here
    [SerializeField] private Rigidbody ballRb;        // drag the Ball here

    private void Awake()
    {
        // Make sure the ball starts frozen
        if (ballRb != null)
        {
            ballRb.isKinematic = true;
            ballRb.useGravity  = false;
        }
    }

    // This is called by Door's trigger collider when the player walks up
    private void OnTriggerEnter(Collider other)
    {
        // OPTIONAL: check tag so only the player triggers the door
        if (!other.CompareTag("Player")) return;

        doorAnimator.SetTrigger("Door_Open");  
        Invoke(nameof(DropBall), 1.5f);          // tiny delay so the door starts moving
    }

    private void DropBall()
    {
        if (ballRb == null) return;

        ballRb.isKinematic = false;   // release it
        ballRb.useGravity  = true;    // let physics take over
    }
}

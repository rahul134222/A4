using UnityEngine;
using System.Collections; 
using Unity.Netcode;
using UnityEngine.UI; 

public class SwipeMovement : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public float speedBoostMultiplier = 2f; // How much the speed is increased by
    public float speedBoostDuration = 5f; // How long the speed boost lasts

    private bool isMoving = false;
    private Vector3 targetPosition;
    private float originalSpeed;
    

    void Awake()
    {
        originalSpeed = moveSpeed;
    }

    void Update()
    {

        if (!IsOwner) return;

        if (!isMoving)
        {
            Vector3 direction = Vector3.zero;

            // Check for each key press and set the direction accordingly
            if (Input.GetKey(KeyCode.W))
            {
                direction = Vector3.up;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                direction = Vector3.down;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                direction = Vector3.left;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                direction = Vector3.right;
            }

            if (direction != Vector3.zero)
            {
                MoveCircle(direction);
            }
        }

        // Move towards the target position if the object is moving
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
    }

    private void MoveCircle(Vector3 direction)
    {
        targetPosition = transform.position + direction;
        isMoving = true;
    }

    public void ActivateSpeedBoost()
    {
        if (moveSpeed == originalSpeed) // Check if not already boosted
        {
            moveSpeed *= speedBoostMultiplier; // Increase the speed
            StartCoroutine(EndSpeedBoostAfterDelay());
        }
    }

    private IEnumerator EndSpeedBoostAfterDelay()
    {
        yield return new WaitForSeconds(speedBoostDuration);
        moveSpeed = originalSpeed; // Reset to the original speed
    }
}

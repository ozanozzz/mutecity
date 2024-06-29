using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private float step = 1f;
    private Vector3 targetPosition;
    private bool isMoving = false;

    // Method to set the target position and start moving
    public void MoveObject(Vector3 target)
    {
        targetPosition = target;
        isMoving = true;
    }

    void Update()
    {
        if (isMoving)
        {
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, distanceToTarget * step * Time.deltaTime);

            // Stop moving when the target position is reached
            if (distanceToTarget <= 0.1f)
            {
                isMoving = false;
            }
        }
    }
}

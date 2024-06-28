using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private float step = 1f;

    public void MoveObject(Vector3 targetPosition)
    {
        if (targetPosition != Vector3.zero)
        {
            float distanceToGround = Vector3.Distance(transform.position, targetPosition);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, distanceToGround * step * Time.deltaTime);
        }
    }
}

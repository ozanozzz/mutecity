using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private LayerMask entityLayer;
    public Vector3 targetPosition;
    [SerializeField] private Vector3GameEvent onTargetEvent;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray groundRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(groundRay, out RaycastHit hit, Mathf.Infinity, entityLayer))
            {
                targetPosition = CalculateGroundPoint();
                RaiseTarget(targetPosition); // Raise the event after calculating the target position
            }
        }
    }

    private Vector3 CalculateGroundPoint()
    {
        Ray groundRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(groundRay, out rayDistance))
        {
            return groundRay.GetPoint(rayDistance);
        }

        return Vector3.zero;
    }

    private void RaiseTarget(Vector3 target)
    {
        if (onTargetEvent != null)
        {
            onTargetEvent.Raise(target); // Raise the trigger move event
        }
    }
}

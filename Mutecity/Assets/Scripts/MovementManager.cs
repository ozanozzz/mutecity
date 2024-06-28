using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject selectedObject;
    [SerializeField] private float step = 1f;
    private Vector3 targetPosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right click to set the target position
        {
            targetPosition = CalculateGroundPoint();
        }

        if (selectedObject != null)
        {
            MoveObject();
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

    private void MoveObject()
    {
        if (targetPosition != Vector3.zero)
        {
            float distanceToGround = Vector3.Distance(selectedObject.transform.position, targetPosition);
            selectedObject.transform.position = Vector3.MoveTowards(selectedObject.transform.position, targetPosition, distanceToGround * step * Time.deltaTime);
        }
    }
}

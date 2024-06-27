using UnityEngine;

public class SelectObject : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Material highlightMaterial; // Material for highlighting

    private GameObject SelectedObject; // Currently selected object
    private Material originalMaterial; // Original material of the selected object
    private Vector3 groundPoint; // Point on the ground where the object will move
    private RaycastHit hit;
    private bool ObjMoving = false; // Indicates if the object is moving
    public float step = 1f;
    public float distanceToGround;

    [SerializeField] private LayerMask entityLayer; // Layer mask for selecting objects

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, entityLayer))
            {
                if (SelectedObject == hit.collider.gameObject)
                {
                    Deselect();
                    ObjMoving = false;
                }
                else
                {
                    Select(hit.collider.gameObject);
                    ObjMoving = true;
                }
            }
            else if(ObjMoving)
            {
                groundPoint = CalculateGroundPoint(); // Calculate ground point when clicked
            }
        }

        if (ObjMoving)
        {
            MoveObject();
        }
    }

    private void Select(GameObject obj)
    {
        // Deselect the previously selected object
        if (SelectedObject != null)
        {
            Deselect();
        }

        SelectedObject = obj;

        // Store the original material
        originalMaterial = SelectedObject.GetComponent<Renderer>().material;

        // Highlight the selected object
        HighlightObject(SelectedObject);
    }

    private void Deselect()
    {
        if (SelectedObject != null)
        {
            // Reset the object's material to the original material
            SelectedObject.GetComponent<Renderer>().material = originalMaterial;
            SelectedObject = null;
        }
    }

    private void HighlightObject(GameObject obj)
    {
        // Change the object's material to the highlight material
        obj.GetComponent<Renderer>().material = highlightMaterial;
    }

    private Vector3 CalculateGroundPoint()
    {
        Ray groundRay = mainCamera.ScreenPointToRay(Input.mousePosition); // Ray from camera to mouse position
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // Invisible plane at Y=0
        float rayDistance;

        if (groundPlane.Raycast(groundRay, out rayDistance))
        {
            return groundRay.GetPoint(rayDistance); // Point on the plane where the ray hits
        }

        return Vector3.zero; // Return zero vector if ray does not hit the plane
    }

    private void MoveObject()
    {
        if (groundPoint != Vector3.zero)
        {
            // Calculate the distance from the current position to the ground point
            distanceToGround = Vector3.Distance(SelectedObject.transform.position, groundPoint);
            // Move the object towards the ground point
            SelectedObject.transform.position = Vector3.MoveTowards(SelectedObject.transform.position, groundPoint, distanceToGround * step * Time.deltaTime);
        }
    }
}

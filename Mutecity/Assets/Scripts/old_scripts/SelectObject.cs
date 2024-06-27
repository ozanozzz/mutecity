using UnityEngine;

public class SelectObject : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject SelectedObject;
    [SerializeField] private Material highlightMaterial; // Add a material for highlighting
    private Material originalMaterial; // Store the original material here
    private Vector3 groundPoint; // Store the point on the ground where the object will move
    private RaycastHit hit;
    public float step = 10f;
    public bool ObjMoving = false;
    public float distanceToGround;

    [SerializeField] private LayerMask entityLayer; // Layer mask to include only the Entity layer for selection

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, entityLayer)) // Raycast using the entity layer mask
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
        }

        if (ObjMoving && Input.GetMouseButton(0)) // i just want to click once and object must move to the desired point, no matter if i hold it or not
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
        // Reset the object's material to the original material
        if (SelectedObject != null)
        {
            SelectedObject.GetComponent<Renderer>().material = originalMaterial;
            SelectedObject = null;
        }
    }

    private void HighlightObject(GameObject obj)
    {
        // Change the object's material to the highlight material
        obj.GetComponent<Renderer>().material = highlightMaterial;
    }

    private void MoveObject()
    {
        Ray groundRay = mainCamera.ScreenPointToRay(Input.mousePosition); // Cast a ray from the camera to the mouse position
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // Define an invisible plane at Y=0
        float rayDistance;

        if (groundPlane.Raycast(groundRay, out rayDistance)) // Cast a ray towards the plane
        {
            groundPoint = groundRay.GetPoint(rayDistance); // Update ground point when mouse is clicked

            // Calculate the distance from the current position to the ground point
            distanceToGround = Vector3.Distance(SelectedObject.transform.position, groundPoint);

            // Move the object towards the ground point
            SelectedObject.transform.position = Vector3.MoveTowards(SelectedObject.transform.position, groundPoint, distanceToGround * step * Time.deltaTime);
        }
    }
}

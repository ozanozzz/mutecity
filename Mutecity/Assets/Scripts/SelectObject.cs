using UnityEngine;

public class SelectObject : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject SelectedObject;
    [SerializeField] private Material highlightMaterial; // Add a material for highlighting
    private Material originalMaterial; // Store the original material here
    private Ray ray;
    private RaycastHit hit;

    [SerializeField] private LayerMask entityLayer; // Layer mask to include only the Entity layer for selection

    void Update()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, entityLayer)) // Raycast using the entity layer mask
            {
                if (SelectedObject == hit.collider.gameObject)
                {
                    Deselect();
                }
                else
                {
                    Select(hit.collider.gameObject);
                }
            }
            else if (SelectedObject != null)
            {
                // Move the selected object if clicked elsewhere
                MoveObject();
            }
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
            Vector3 groundPoint = groundRay.GetPoint(rayDistance); // Get the point on the plane where the ray hits
            SelectedObject.transform.position = groundPoint; // Move the selected object to that point
        }
    }
}

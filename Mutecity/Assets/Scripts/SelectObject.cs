using UnityEngine;

public class SelectObject : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject SelectedObject;
    [SerializeField] private Material highlightMaterial; // Add a material for highlighting
    private Material originalMaterial; // Store the original material here
    private Ray ray;
    private RaycastHit hit;

    void Update()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                Select(hit.collider.gameObject);
            }
        }

        if (SelectedObject != null)
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
        SelectedObject.GetComponent<Renderer>().material = originalMaterial;
        SelectedObject = null;
    }

    private void HighlightObject(GameObject obj)
    {
        // Change the object's material to the highlight material
        obj.GetComponent<Renderer>().material = highlightMaterial;
    }

    private void MoveObject()
    {
        Ray groundRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (new Plane(Vector3.up, Vector3.zero).Raycast(groundRay, out float distance))
        {
            Vector3 groundPoint = groundRay.GetPoint(distance);
            SelectedObject.transform.position = groundPoint;
        }
    }
    
}

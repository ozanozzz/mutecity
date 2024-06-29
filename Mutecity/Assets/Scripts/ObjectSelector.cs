using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask entityLayer;
    [SerializeField] private Material highlightMaterial;
    private GameObject selectedObject;
    private bool isObjectSelected = false;
    private bool isMovable = false;
    private Material originalMaterial;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, entityLayer))
            {
                SelectObject(hit.collider.gameObject);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            DeselectObject();
        }
    }

    private void SelectObject(GameObject obj)
    {
        if (selectedObject != null)
        {
            DeselectObject();
        }

        selectedObject = obj;
        isObjectSelected = true;
        isMovable = true; // Set the object as movable when selected

        // Save the original material and set the highlight material
        var renderer = selectedObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            originalMaterial = renderer.material;
            renderer.material = highlightMaterial;
        }

        Debug.Log("Object Selected: " + selectedObject.name);
    }

    private void DeselectObject()
    {
        if (selectedObject != null)
        {
            isMovable = false; // Set the object as not movable when deselected

            // Restore the original material
            var renderer = selectedObject.GetComponent<Renderer>();
            if (renderer != null && originalMaterial != null)
            {
                renderer.material = originalMaterial;
            }

            Debug.Log("Object Deselected: " + selectedObject.name);
            selectedObject = null;
            isObjectSelected = false;
        }
    }
}

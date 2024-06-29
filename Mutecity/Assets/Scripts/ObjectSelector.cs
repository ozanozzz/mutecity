using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask selectableLayer;
    [SerializeField] private Material highlightMaterial;
    private GameObject selectedObject;
    private Material originalMaterial;

    void Update()
    {
        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, selectableLayer))
            {
                SelectObject(hit.collider.gameObject);
            }
        }

        // Check for right mouse button click
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

        // Save the original material and set the highlight material
        var renderer = selectedObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            originalMaterial = renderer.material;
            renderer.material = highlightMaterial;
        }

        // Set the object as movable
        var objectMover = selectedObject.GetComponent<ObjectMover>();
        if (objectMover != null)
        {
            objectMover.SetMovable(true);
        }

        Debug.Log("Object Selected: " + selectedObject.name);
    }

    private void DeselectObject()
    {
        if (selectedObject != null)
        {
            // Restore the original material
            var renderer = selectedObject.GetComponent<Renderer>();
            if (renderer != null && originalMaterial != null)
            {
                renderer.material = originalMaterial;
            }

            // Set the object as not movable
            var objectMover = selectedObject.GetComponent<ObjectMover>();
            if (objectMover != null)
            {
                objectMover.SetMovable(false);
            }

            Debug.Log("Object Deselected: " + selectedObject.name);
            selectedObject = null;
        }
    }
}

using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask entityLayer;
    [SerializeField] private GameEvent onSelectEvent;
    [SerializeField] private GameEvent onDeselectEvent;

    private GameObject selectedObject;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, entityLayer))
            {
                if (selectedObject == hit.collider.gameObject)
                {
                    DeselectObject();
                }
                else
                {
                    SelectObject(hit.collider.gameObject);
                }
            }
        }
    }

    private void SelectObject(GameObject obj)
    {
        DeselectObject(); // Deselect previously selected object
        selectedObject = obj;
        onSelectEvent.Raise(); // Raise the select event
    }

    private void DeselectObject()
    {
        if (selectedObject != null)
        {
            onDeselectEvent.Raise(); // Raise the deselect event
            selectedObject = null;
        }
    }
}

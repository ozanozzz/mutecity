using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    [SerializeField] private Material highlightMaterial;
    private Material originalMaterial;
    private Renderer objectRenderer;

    void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;
    }

    public void Select()
    {
        objectRenderer.material = highlightMaterial;
    }

    public void Deselect()
    {
        objectRenderer.material = originalMaterial;
    }
}

using UnityEngine;

public class ObjectHighlighter : MonoBehaviour
{
    [SerializeField] private Material highlightMaterial;
    private Material originalMaterial;
    private Renderer objectRenderer;

    void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;
    }

    public void Highlight()
    {
        objectRenderer.material = highlightMaterial;
    }

    public void RemoveHighlight()
    {
        objectRenderer.material = originalMaterial;
    }
}

using UnityEngine;

public class ChangeMaterialColorInScene : MonoBehaviour
{
    private SkinnedMeshRenderer ghostRenderer;  // Skinned Mesh Renderer des Geistes
    private Material ghostMaterial;  // Eine Instanz des Materials für diesen Geist

    public Color sceneColor;  // Die Farbe, auf die das Material in dieser Szene geändert wird

    void Start()
    {
        // Zugriff auf den Skinned Mesh Renderer des Geistes
        ghostRenderer = GetComponent<SkinnedMeshRenderer>();

        if (ghostRenderer != null)
        {
            // Instanziere das Material, um sicherzustellen, dass nur dieser Geist die Farbänderung erhält
            ghostMaterial = ghostRenderer.material;  // Dies erstellt eine Kopie des Materials, nur für diesen Geist
            
            // Ändere die Farbe des Materials in dieser Szene
            ghostMaterial.color = sceneColor;
        }
        else
        {
            Debug.LogError("Skinned Mesh Renderer not found on the Ghost!");
        }
    }
}

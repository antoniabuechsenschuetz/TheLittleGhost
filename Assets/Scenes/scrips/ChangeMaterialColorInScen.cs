using UnityEngine;

public class ChangeGhostColor : MonoBehaviour
{
    private Renderer ghostRenderer;  // Der Renderer des Geistes
    private Material ghostMaterial;  // Eine Instanz des Materials für diesen Geist

    public Color newColor;  // Die neue Farbe, die der Geist annehmen soll

    void Start()
    {
        // Zugriff auf den Renderer des Geistes
        ghostRenderer = GetComponent<Renderer>();

        if (ghostRenderer != null)
        {
            // Instanziere das Material, um sicherzustellen, dass nur dieser Geist die Farbänderung erhält
            ghostMaterial = ghostRenderer.material;  // Dies erstellt eine Kopie des Materials
        }
        else
        {
            Debug.LogError("Renderer not found on the Ghost!");
        }
    }

    // Methode zum Ändern der Farbe
    public void ChangeColor()
    {
        if (ghostMaterial != null)
        {
            // Setze die neue Farbe nur für dieses Material (und nicht global)
            ghostMaterial.color = newColor;
        }
    }
}

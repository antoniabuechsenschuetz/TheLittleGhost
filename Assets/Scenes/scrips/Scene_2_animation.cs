using UnityEngine;

public class Scene_2_animation : MonoBehaviour
{
    public Animator ghostAnimator;  // Der Animator des Geistes

    void Start()
    {
        // Überprüfe, ob der Animator zugewiesen ist
        if (ghostAnimator != null)
        {
            // Spiele die Animation "Scene_2" ab, wenn die Szene gestartet wird
            ghostAnimator.Play("Scene_2");
        }
        else
        {
            Debug.LogError("Animator not assigned!");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene4 : MonoBehaviour
{
    // Referenz zum Animator, der die Animationen steuert
    public Animator ghostAnimator; // Animator für den Geist

    // Der Name der Animation, die beim Start abgespielt werden soll
    public string animationName = "Scene4";

    void Start()
    {
        // Überprüfen, ob der Animator zugewiesen ist
        if (ghostAnimator != null)
        {
            // Spiele die Animation ab, wenn die Szene geladen wird
            ghostAnimator.Play(animationName);
        }
        else
        {
            Debug.LogError("Animator not assigned!");
        }
    }
}
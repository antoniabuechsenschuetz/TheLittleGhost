using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Für TextMeshPro

public class Uhr_ : MonoBehaviour
{
    // Reference to the ghost GameObject and its Animator
    public GameObject ghost;
    public Animator ghostAnimator;  // Animator for ghost animations

    // TextMeshPro Elements for changing the story text
    public GameObject storyPannel;           // The "Storypannel" GameObject (Parent)
    private TextMeshProUGUI storyTextTMP;    // The actual Text (TMP) component under Storypannel

    public string firstText = "The ghost is hidden...";
    public string secondText = "The ghost flies out of the clock!";

    private bool isGhostVisible = false; // Tracks if the ghost is visible

    // Reference to the Next_Button
    public GameObject nextButton;

    void Start()
    {
        // Make the ghost invisible at the start
        if (ghost != null)
        {
            ghost.SetActive(false);
        }
        else
        {
            Debug.LogError("Ghost object not assigned.");
        }

        // Initialize the TextMeshPro component
        if (storyPannel != null)
        {
            storyTextTMP = storyPannel.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            if (storyTextTMP == null)
            {
                Debug.LogError("Text (TMP) component not found in Storypannel!");
            }
            else
            {
                // Set initial text
                storyTextTMP.text = firstText;
            }
        }
        else
        {
            Debug.LogError("StoryPannel is null!");
        }

        // Make sure the Next_Button is invisible at the start
        if (nextButton != null)
        {
            nextButton.SetActive(false); // Hide the button at the start
        }
        else
        {
            Debug.LogError("Next_Button not assigned.");
        }
    }

    // Method called when the clock is clicked
    void OnMouseDown()
    {
        Debug.Log("Clock was clicked!");
        if (ghost != null)
        {
            // Toggle the ghost visibility
            isGhostVisible = !isGhostVisible;

            if (isGhostVisible)
            {
                Debug.Log("Ghost found, making it visible.");
                // Make the ghost visible and trigger the fly-out animation
                ghost.SetActive(true);

                if (ghostAnimator != null)
                {
                    Debug.Log("Setting FlyOut trigger.");
                    ghostAnimator.SetTrigger("FlyOut");
                    
                    // Start coroutine to wait for the animation to finish
                    StartCoroutine(WaitForAnimationToEnd());
                }
                else
                {
                    Debug.LogWarning("Ghost Animator not assigned.");
                }

                // Change the story text to the second text
                if (storyTextTMP != null)
                {
                    storyTextTMP.text = secondText;
                }
            }
            else
            {
                Debug.Log("Hiding the ghost again.");
                // Make the ghost invisible
                ghost.SetActive(false);

                // Change the story text back to the first text
                if (storyTextTMP != null)
                {
                    storyTextTMP.text = firstText;
                }
            }
        }
        else
        {
            Debug.LogError("Ghost object not assigned.");
        }
    }

    // Coroutine to wait for the animation to finish before showing the Next_Button
    IEnumerator WaitForAnimationToEnd()
    {
        // Wait for the length of the current animation state
        if (ghostAnimator != null)
        {
            AnimatorStateInfo stateInfo = ghostAnimator.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(stateInfo.length);
            
            // After the animation ends, show the Next_Button
            if (nextButton != null)
            {
                nextButton.SetActive(true);  // Make the Next_Button visible
            }
        }
    }
}

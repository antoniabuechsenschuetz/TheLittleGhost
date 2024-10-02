using UnityEngine;
using TMPro;  // For TextMeshPro
using System.Collections;  // For Coroutine

public class scene_3_animation : MonoBehaviour
{
    // Animator variable for ghost animation
    public Animator ghostAnimator;

    // Reference to the Next_Button GameObject
    public GameObject nextButton;

    // Reference to the StoryPannel and Text (TMP)
    public GameObject storyPannel;
    private TextMeshProUGUI storyTextTMP;

    // Text to be displayed before and after the animation
    public string beforeAnimationText = "The adventure begins...";
    public string afterAnimationText = "The adventure continues!";

    void Start()
    {
        // If the Animator is not assigned, try to get it from the current GameObject
        if (ghostAnimator == null)
        {
            ghostAnimator = GetComponent<Animator>();
        }

        // Check if the Animator is assigned
        if (ghostAnimator != null)
        {
            // Play the animation "scene3" at the start of the scene
            ghostAnimator.Play("scene3");

            // Start Coroutine to wait for the animation to end
            StartCoroutine(WaitForAnimationToEnd());
        }
        else
        {
            Debug.LogError("Animator not assigned!");
        }

        // Set up the TextMeshPro component from the StoryPannel
        if (storyPannel != null)
        {
            storyTextTMP = storyPannel.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            if (storyTextTMP == null)
            {
                Debug.LogError("Text (TMP) component not found in StoryPannel!");
            }
            else
            {
                // Set the text before the animation starts
                storyTextTMP.text = beforeAnimationText;
            }
        }
        else
        {
            Debug.LogError("StoryPannel is not assigned!");
        }

        // Ensure the Next_Button is hidden at the start
        if (nextButton != null)
        {
            nextButton.SetActive(false);
        }
        else
        {
            Debug.LogError("Next_Button is not assigned!");
        }
    }

    // Coroutine to wait for the animation to end
    IEnumerator WaitForAnimationToEnd()
    {
        // Wait until the animation has finished playing
        while (ghostAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;  // Wait for the animation to complete
        }

        // After the animation ends, show the Next_Button and update the text
        if (nextButton != null)
        {
            nextButton.SetActive(true);  // Make the Next_Button visible
        }

        // Change the text in StoryPannel -> Text (TMP) after the animation
        if (storyTextTMP != null)
        {
            storyTextTMP.text = afterAnimationText;
        }
    }
}

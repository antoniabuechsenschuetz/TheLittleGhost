using System.Collections;
using UnityEngine;
using TMPro;  // For TextMeshPro

public class Scene5 : MonoBehaviour
{
    // Reference to the Animator for the wood box animation
    public Animator boxAnimator;
    public string animationClipName = "scene5"; // The name of the animation to play

    // Reference to the ghost that will fade out
    public GameObject ghostObject;
    public float fadeDuration = 2f; // Duration of the fade-out effect

    // Reference to the Next_Button GameObject
    public GameObject nextButton;

    // Reference to the StoryPannel GameObject (Parent of Text (TMP))
    public GameObject storyPannel;
    private TextMeshProUGUI storyTextTMP;

    // Text to be displayed before and after the animation
    public string beforeAnimationText = "The ghost hides in the box...";
    public string afterAnimationText = "The ghost is now gone. What happens next?";

    void Start()
    {
        // Ensure the Next_Button is hidden at the start
        if (nextButton != null)
        {
            nextButton.SetActive(false);
        }
        else
        {
            Debug.LogError("Next_Button is not assigned!");
        }

        // Get the TextMeshPro component from StoryPannel -> Text (TMP)
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
    }

    // This method is called when the object with the Box Collider is clicked
    private void OnMouseDown()
    {
        // Play the box animation directly by its name
        if (boxAnimator != null)
        {
            boxAnimator.Play(animationClipName); // Play the animation clip
            StartCoroutine(WaitForAnimationToEnd());
        }
        else
        {
            Debug.LogError("Box Animator is not assigned!");
        }

        // Start fading out the ghost as the animation plays
        if (ghostObject != null)
        {
            StartCoroutine(FadeOutGhost(ghostObject, fadeDuration));
        }
        else
        {
            Debug.LogError("Ghost object is not assigned!");
        }
    }

    // Coroutine to wait for the animation to end before showing the Next_Button and updating the text
    IEnumerator WaitForAnimationToEnd()
    {
        // Wait until the animation has finished playing
        while (boxAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
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

    // Coroutine to fade out the ghost over time
    IEnumerator FadeOutGhost(GameObject obj, float duration)
    {
        Renderer objRenderer = obj.GetComponent<Renderer>();
        if (objRenderer != null)
        {
            Material objMaterial = objRenderer.material;  // Get the material of the object
            Color initialColor = objMaterial.color;        // Get the current color of the object
            float elapsedTime = 0f;

            // Ensure the material supports transparency
            if (objMaterial.HasProperty("_Color"))
            {
                while (elapsedTime < duration)
                {
                    elapsedTime += Time.deltaTime;
                    float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration); // Fade from fully opaque to fully transparent
                    objMaterial.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha); // Set the new color with fading alpha
                    yield return null;
                }

                // Ensure the object is fully transparent at the end
                objMaterial.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
            }
            else
            {
                Debug.LogError("The material doesn't support color property!");
            }
        }
        else
        {
            Debug.LogError("Renderer not found on the object!");
        }
    }
}

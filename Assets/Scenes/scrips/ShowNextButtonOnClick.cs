using UnityEngine;
using TMPro; // For TextMeshPro
using System.Collections;  // For Coroutine

public class ShowNextButtonOnClick : MonoBehaviour
{
    // Reference to the Next_Button GameObject
    public GameObject nextButton;

    // Reference to the StoryPannel GameObject (Parent of Text (TMP))
    public GameObject storyPannel;
    private TextMeshProUGUI storyTextTMP;

    // Text that will be displayed before and after clicking
    public string beforeClickText = "The ghost is waiting for an adventure!";
    public string afterClickText = "The ghost has started a new adventure!";

    // Reference to the Animator for the animation
    public Animator castleAnimator; // Animator attached to the castle or another object
    public string animationClipName = "GhostFlyOut"; // The name of the animation clip

    // Reference to the object that will fade out
    public GameObject objectToFade;
    public float fadeDuration = 8.0f; // Duration of the fade-out effect

    // Color change settings
    public Color ghostNewColor = Color.green;  // New color to apply to the ghost
    public Renderer ghostRenderer;  // Reference to the Renderer of the ghost

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
                // Set the initial text (before clicking)
                storyTextTMP.text = beforeClickText;
            }
        }
        else
        {
            Debug.LogError("StoryPannel is not assigned!");
        }
    }

    // This method is called when the object with this script is clicked
    private void OnMouseDown()
    {
        // Play the animation directly by its name
        if (castleAnimator != null)
        {
            castleAnimator.Play(animationClipName); // Play the animation clip directly

            // Start fading the object immediately as the animation starts
            if (objectToFade != null)
            {
                StartCoroutine(FadeOut(objectToFade, fadeDuration));
            }

            // Show the Next_Button after the fade-out completes
            StartCoroutine(ShowNextButtonAfterFade(fadeDuration));
        }
        else
        {
            Debug.LogError("Castle Animator is not assigned!");
        }

        // Change the color of the ghost when clicked
        if (ghostRenderer != null)
        {
            ghostRenderer.material.color = ghostNewColor;  // Apply the new color to the ghost
        }
        else
        {
            Debug.LogError("Ghost Renderer is not assigned!");
        }

        // Change the text in the StoryPannel -> Text (TMP) to afterClickText
        if (storyTextTMP != null)
        {
            storyTextTMP.text = afterClickText;
        }
        else
        {
            Debug.LogError("Story Text (TMP) is not assigned or not found!");
        }
    }

    // Coroutine to show the Next_Button after the fade-out is complete
    IEnumerator ShowNextButtonAfterFade(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (nextButton != null)
        {
            nextButton.SetActive(true);  // Make the Next_Button visible after fading out
        }
    }

    // Coroutine to fade out the object over time
    IEnumerator FadeOut(GameObject obj, float duration)
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

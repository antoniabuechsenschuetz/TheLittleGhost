using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Für TextMeshPro

public class ChangeBackgroundImageOnClick : MonoBehaviour
{
    public Image backgroundImage;      // The UI Image component for the background
    public Sprite firstBackground;     // First background image
    public Sprite secondBackground;    // Second background image

    public GameObject ghost;           // The ghost object to change color (3D ghost with MeshRenderer)
    public Color firstGhostColor = Color.white;   // First color for the ghost
    public Color secondGhostColor = Color.red;    // Second color for the ghost

    public Color firstObjectColor = Color.green;  // First color for the clicked object
    public Color secondObjectColor = Color.blue;  // Second color for the clicked object

    public GameObject storyPannel;           // The "Storypannel" GameObject (Parent)
    private TextMeshProUGUI storyTextTMP;    // The actual Text (TMP) component under Storypannel

    public string firstText = "The little ghost is ready for adventure!";
    public string secondText = "The night grows quiet as the ghost rests.";

    private bool isUsingFirstBackground = true;   // Keeps track of which background and color is active

    // Reference to the Next_Button
    public GameObject nextButton;

    private void Start()
    {
        // Hole dir die TextMeshPro-Komponente des "Storypannel" GameObjects
        if (storyPannel != null)
        {
            storyTextTMP = storyPannel.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            if (storyTextTMP == null)
            {
                Debug.LogError("Text (TMP) component not found in Storypannel!");
            }
        }
        else
        {
            Debug.LogError("Storypannel is null!");
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

    private void OnMouseDown()
    {
        // Ensure the background image, ghost, and story text are properly assigned
        if (backgroundImage == null || ghost == null || storyTextTMP == null)
        {
            Debug.LogError("Background, Ghost, or Story Panel Text (TMP) not assigned or found!");
            return;
        }

        // Toggle between the two backgrounds, ghost colors, object colors, and story text
        if (isUsingFirstBackground)
        {
            // Switch to the second background and second ghost color
            backgroundImage.sprite = secondBackground;
            ghost.GetComponent<Renderer>().material.color = secondGhostColor;  // Change ghost color
            
            // Change the color of the clicked object (the object this script is attached to)
            GetComponent<Renderer>().material.color = secondObjectColor;  // Change object color

            // Change the story text to the second text
            storyTextTMP.text = secondText;
        }
        else
        {
            // Switch back to the first background and first ghost color
            backgroundImage.sprite = firstBackground;
            ghost.GetComponent<Renderer>().material.color = firstGhostColor;  // Change ghost color back

            // Change the color of the clicked object (the object this script is attached to)
            GetComponent<Renderer>().material.color = firstObjectColor;  // Change object color back

            // Change the story text to the first text
            storyTextTMP.text = firstText;
        }

        // After switching, make the Next_Button visible
        if (nextButton != null)
        {
            nextButton.SetActive(true);  // Make the Next_Button visible
        }

        // Flip the state to toggle next time
        isUsingFirstBackground = !isUsingFirstBackground;
    }
}

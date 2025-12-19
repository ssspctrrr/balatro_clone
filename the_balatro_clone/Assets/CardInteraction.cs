using UnityEngine;
using UnityEngine.UI;

public class CardInteraction : MonoBehaviour
{
    public bool isSelected = false;
    private Image cardImage;

    void Start()
    {
        // Get the Image component so we can change color
        cardImage = GetComponent<Image>();
        
        // Auto-setup the button click
        GetComponent<Button>().onClick.AddListener(ToggleSelection);
    }

    // This runs when you click the card
    public void ToggleSelection()
    {
        isSelected = !isSelected; // Flip true/false

        if (isSelected)
        {
            // Visual: Turn slightly gray/green to show selection
            cardImage.color = Color.gray; 
            
            // OPTIONAL: If you want the "Pop Up" effect later, we do it here
            // transform.localPosition += Vector3.up * 20; 
        }
        else
        {
            // Visual: Reset color
            cardImage.color = Color.white;
            
            // transform.localPosition -= Vector3.up * 20;
        }
    }
}
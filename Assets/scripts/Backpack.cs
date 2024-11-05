using UnityEngine;
using TMPro;

public class BackpackUI : MonoBehaviour
{
    public TextMeshProUGUI backpackText; // Reference to TextMeshProUGUI component
    public GameObject backpackPanel; // Panel that contains the UI

    private void Start()
    {
        backpackPanel.SetActive(false); // Ensure the backpack UI is hidden initially
    }

    private void Update()
    {
        // Toggle the backpack UI visibility with E key
        if (Input.GetKeyDown(KeyCode.E))
        {
            backpackPanel.SetActive(!backpackPanel.activeSelf);
            UpdateBackpackUI();
        }
    }

    // Update the UI to display backpack contents
    private void UpdateBackpackUI()
    {
        if (GameManager.Instance != null)
        {
            backpackText.text = "Backpack:\n";
            foreach (string item in GameManager.Instance.backpackItems)
            {
                backpackText.text += item + "\n";
            }
        }
    }
}

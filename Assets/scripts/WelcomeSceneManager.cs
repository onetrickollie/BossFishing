using UnityEngine;
using UnityEngine.UI;

public class WelcomeSceneManager : MonoBehaviour
{
    [SerializeField] private Button startButton; // Assign this in the Inspector

    private void Start()
    {
        if (startButton != null)
        {
            // Remove any existing listeners to avoid duplicate calls
            startButton.onClick.RemoveAllListeners();
            // Add the desired listener
            startButton.onClick.AddListener(OnStartButtonClicked);
        }
        else
        {
            Debug.LogWarning("Start button reference is missing.");
        }
    }

    private void OnStartButtonClicked()
    {
        Debug.Log("Start button clicked");
        GameManager.Instance.StartGame(); // Assuming GameManager handles scene transition
    }
}
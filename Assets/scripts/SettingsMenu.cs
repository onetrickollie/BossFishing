using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioSource audioSource; // Reference to the main AudioSource for volume control
    [SerializeField] private GameObject settingsPanel; // Reference to the settings/pause menu panel

    private bool isPaused = false; // Track whether the game is paused

    private void Start()
    {
        // Initialize slider value to match the audio source volume
        if (audioSource != null && volumeSlider != null)
        {
            volumeSlider.value = audioSource.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume); // Attach listener to slider
        }

        // Ensure the settings panel is hidden at the start
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }

    // Method to set volume based on slider value
    public void SetVolume(float value)
    {
        if (audioSource != null)
        {
            audioSource.volume = value;
        }
    }

    // Method to handle "Back to Main Menu" button click
    public void BackToMainMenu()
    {
        // Resume game time before switching scenes
        Time.timeScale = 1f;
        SceneManager.LoadScene("welcome"); // Replace "welcome" with the actual name of your main menu scene
    }

    // Method to toggle the pause menu
    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(isPaused);
        }

        // Pause or resume the game time
        Time.timeScale = isPaused ? 0f : 1f;
    }

    // Method to quit/resume the pause menu
    public void QuitPauseMenu()
    {
        isPaused = false;
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        // Resume game time
        Time.timeScale = 1f;
    }
}

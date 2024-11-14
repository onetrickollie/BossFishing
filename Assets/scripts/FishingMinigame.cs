using UnityEngine;
using UnityEngine.UI;
using System;

public class FishingMinigame : MonoBehaviour
{
    [SerializeField] private Scrollbar progressBar; // The Scrollbar for the minigame
    [SerializeField] private RectTransform targetIndicator; // Reference to the target indicator UI element
    [SerializeField] private float baseFillSpeed = 0.1f; // Base speed for filling
    [SerializeField] private float decayRate = 0.05f; // Speed at which the handle decays
    [SerializeField] private float minTargetFill = 0.7f; // Minimum target position as a fraction (0 to 1)
    [SerializeField] private float maxTargetFill = 1f; // Maximum target position as a fraction (0 to 1)
    [SerializeField] private GameObject fishingPanel; // The panel containing the minigame UI
    [SerializeField] private float minigameDuration = 5f; // Maximum duration of the minigame in seconds

    public event Action OnMinigameWin; // Event for when the minigame is won
    public event Action OnMinigameLose; // Event for when the minigame is lost

    private float targetFillAmount; // Target value for the Scrollbar
    private bool isMinigameActive = false;
    private float elapsedTime = 0f; // Track time elapsed
    private float currentFillSpeed; // Current fill speed modified by the rod

    private void Start()
    {
        progressBar.value = 0; // Start with no progress
        fishingPanel.SetActive(false); // Hide the panel initially
        SetRandomTargetIndicator(); // Set the initial target indicator position
    }

    private void Update()
    {
        if (isMinigameActive)
        {
            // Detect mouse button clicks to increase progress
            if (Input.GetMouseButton(0)) // Left mouse button
            {
                progressBar.value += currentFillSpeed * Time.deltaTime;
            }
            else
            {
                progressBar.value -= decayRate * Time.deltaTime;
            }

            // Clamp the value to ensure it stays between 0 and 1
            progressBar.value = Mathf.Clamp01(progressBar.value);

            // Track elapsed time
            elapsedTime += Time.deltaTime;

            // Check win/loss conditions
            if (progressBar.value >= targetFillAmount)
            {
                WinMinigame();
            }
            else if (elapsedTime >= minigameDuration || progressBar.value <= 0)
            {
                LoseMinigame();
            }
        }
    }

    public void StartMinigame()
    {
        progressBar.value = 0; // Reset progress
        targetFillAmount = UnityEngine.Random.Range(minTargetFill, maxTargetFill);
        UpdateTargetIndicator(); // Update the indicator position based on the new target
        fishingPanel.SetActive(true); // Show the panel
        isMinigameActive = true;
        elapsedTime = 0f; // Reset timer

        // Set the fill speed based on the equipped rod
        SetFillSpeedBasedOnRod();
    }

    private void WinMinigame()
    {
        isMinigameActive = false;
        fishingPanel.SetActive(false); // Hide the panel
        OnMinigameWin?.Invoke(); // Trigger win event
        Debug.Log("Minigame won!");
    }

    private void LoseMinigame()
    {
        isMinigameActive = false;
        fishingPanel.SetActive(false); // Hide the panel
        OnMinigameLose?.Invoke(); // Trigger lose event
        Debug.Log("Minigame lost!");
    }

    private void SetRandomTargetIndicator()
    {
        targetFillAmount = UnityEngine.Random.Range(minTargetFill, maxTargetFill);
        UpdateTargetIndicator();
    }

    private void UpdateTargetIndicator()
    {
        if (targetIndicator != null && progressBar != null)
        {
            // Adjust targetIndicator's position to reflect targetFillAmount
            float progressBarWidth = progressBar.GetComponent<RectTransform>().rect.width;
            float targetPositionX = targetFillAmount * progressBarWidth;
            targetIndicator.anchoredPosition = new Vector2(targetPositionX, targetIndicator.anchoredPosition.y);
        }
    }

    private void SetFillSpeedBasedOnRod()
    {
        int rodIndex = GameManager.Instance.GetEquippedRodIndex(); // Get the current rod index from GameManager
        currentFillSpeed = baseFillSpeed * (1 + rodIndex * 0.2f); // Adjust fill speed based on rod index
        Debug.Log($"Fill speed adjusted for rod index {rodIndex}: {currentFillSpeed}");
    }

    public void SetFillSpeed(float multiplier)
    {
        currentFillSpeed = baseFillSpeed * multiplier; // Dynamically adjust the fill speed if needed
    }
}

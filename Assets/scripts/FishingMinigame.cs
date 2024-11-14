using UnityEngine;
using UnityEngine.UI;
using System;

public class FishingMinigame : MonoBehaviour
{
    [SerializeField] private Scrollbar progressBar; // The Scrollbar for the minigame
    [SerializeField] private RectTransform targetIndicator; // Reference to the target indicator UI element
    [SerializeField] private float baseFillSpeed = 0.1f; // Base speed for filling
    [SerializeField] private float decayRate = 0.02f; // Speed at which the handle decays
    [SerializeField] private float minTargetFill = 0.7f; // Minimum target position as a fraction (0 to 1)
    [SerializeField] private float maxTargetFill = 1f; // Maximum target position as a fraction (0 to 1)
    [SerializeField] private GameObject fishingPanel; // The panel containing the minigame UI
    [SerializeField] private float minigameDuration = 10f; // Maximum duration of the minigame in seconds
    [SerializeField] private float gracePeriod = 1f; // Grace period before decay starts
    [SerializeField] private float decayCooldown = 0.5f; // Cooldown period after stopping clicks before decay starts

    public event Action OnMinigameWin; // Event for when the minigame is won
    public event Action OnMinigameLose; // Event for when the minigame is lost

    private float targetFillAmount; // Target value for the Scrollbar
    private bool isMinigameActive = false;
    private float elapsedTime = 0f; // Track time elapsed
    private float currentFillSpeed; // Current fill speed modified by the rod
    private float graceTimeRemaining; // Tracks remaining grace time
    private float lastClickTime; // Tracks the time of the last mouse click

    private void Start()
    {
        progressBar.value = 0.2f; // Set initial progress to a starting value (20%)
        fishingPanel.SetActive(false); // Hide the panel initially
        SetRandomTargetIndicator(); // Set the initial target indicator position
    }

    private void Update()
    {
        if (isMinigameActive)
        {
            // Reduce grace time if active
            if (graceTimeRemaining > 0)
            {
                graceTimeRemaining -= Time.deltaTime;
            }

            // Detect mouse button clicks to increase progress
            if (Input.GetMouseButtonDown(0)) // Detects individual mouse clicks
            {
                float increaseAmount = currentFillSpeed * 2 * Time.deltaTime;
                progressBar.value += increaseAmount;
                lastClickTime = Time.time; // Update last click time
                Debug.Log($"Click detected. Increased ProgressBar by: {increaseAmount}, New Value: {progressBar.value}");
            }
            else if (graceTimeRemaining <= 0 && Time.time - lastClickTime >= decayCooldown) // Decay only after grace period and cooldown
            {
                // Decay the progress when the mouse is not being clicked
                progressBar.value -= decayRate * Time.deltaTime;
                Debug.Log($"ProgressBar decayed. New Value: {progressBar.value}");
            }

            // Clamp the value to ensure it stays between 0 and 1
            progressBar.value = Mathf.Clamp01(progressBar.value);

            // Track elapsed time
            elapsedTime += Time.deltaTime;

            // Check win condition
            if (progressBar.value >= targetFillAmount)
            {
                WinMinigame(); // Call win logic and ensure the minigame stops
            }
            // Check lose condition
            else if (elapsedTime >= minigameDuration || progressBar.value <= 0)
            {
                LoseMinigame();
            }
        }
    }

    public void StartMinigame()
    {
        progressBar.value = 0.2f; // Reset progress to initial value (20%)
        targetFillAmount = UnityEngine.Random.Range(minTargetFill, maxTargetFill);
        Debug.Log($"Target Fill Amount set to: {targetFillAmount}");
        UpdateTargetIndicator(); // Update the indicator position based on the new target
        fishingPanel.SetActive(true); // Show the panel
        isMinigameActive = true;
        elapsedTime = 0f; // Reset timer
        graceTimeRemaining = gracePeriod; // Set initial grace period
        lastClickTime = Time.time; // Set initial last click time

        // Set the fill speed based on the equipped rod
        SetFillSpeedBasedOnRod();
        Debug.Log("Minigame started.");
    }

    private void WinMinigame()
    {
        if (!isMinigameActive) return; // Prevent multiple wins
        isMinigameActive = false; // Stop the minigame
        fishingPanel.SetActive(false); // Hide the panel
        OnMinigameWin?.Invoke(); // Trigger win event
        Debug.Log("Minigame won!");
    }

    private void LoseMinigame()
    {
        if (!isMinigameActive) return; // Prevent multiple losses
        isMinigameActive = false; // Stop the minigame
        fishingPanel.SetActive(false); // Hide the panel
        OnMinigameLose?.Invoke(); // Trigger lose event
        Debug.Log("Minigame lost!");
    }

    private void SetRandomTargetIndicator()
    {
        targetFillAmount = UnityEngine.Random.Range(minTargetFill, maxTargetFill);
        Debug.Log($"Random Target Fill Amount set to: {targetFillAmount}");
        UpdateTargetIndicator();
    }

    private void UpdateTargetIndicator()
    {
        if (targetIndicator != null && progressBar != null)
        {
            // Ensure the targetIndicator position is relative to the progress bar's width
            float progressBarWidth = progressBar.GetComponent<RectTransform>().rect.width;

            // Calculate the correct position for the target indicator
            float targetPositionX = targetFillAmount * progressBarWidth - (progressBarWidth / 2f); // Offset for centering within progress bar bounds
            targetIndicator.anchoredPosition = new Vector2(targetPositionX, targetIndicator.anchoredPosition.y);
            Debug.Log($"Target indicator position updated to: {targetIndicator.anchoredPosition}");
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

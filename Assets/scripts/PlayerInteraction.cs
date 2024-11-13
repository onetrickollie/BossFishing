using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class PlayerInteraction : MonoBehaviour
{
    private bool canFish = false;
    private bool isFishing = false; // Prevent multiple fishing attempts
    private bool fishBiting = false; // Track if the fish is currently biting

    [SerializeField]
    private List<FishData> fishList = new List<FishData>(); // List of fish data (ScriptableObjects)
    [SerializeField]
    private InventoryManager inventoryManager;
    [SerializeField]
    private AudioSource mainAudioSource; // Reference to an AudioSource for playing general sounds
    [SerializeField]
    private AudioSource fishBiteAudioSource; // Separate AudioSource for fish bite sound
    // Sound when fishing starts
    [SerializeField]
    private AudioClip fishBiteSound; // Sound when fish bites
    [SerializeField]
    private GameObject exclamationMark; // Reference to the exclamation mark GameObject
    [SerializeField]
    private Animator animator; // Reference to the Animator component
    
    [SerializeField]
    private TMP_Text InGameMessage;
    private CatchMessageUI catchMessageUI;

    private void Start()
    {
        catchMessageUI = FindObjectOfType<CatchMessageUI>();
        if (inventoryManager == null)
        {
            Debug.LogWarning("No InventoryManager found in the scene. Make sure to add one to the scene.");
        }

        if (exclamationMark != null)
        {
            exclamationMark.SetActive(false); // Ensure the exclamation mark is hidden initially
        }

        if (mainAudioSource == null)
        {
            Debug.LogWarning("No AudioSource assigned for general sound playback.");
        }
        if (fishBiteAudioSource == null)
        {
            Debug.LogWarning("No AudioSource assigned for fish bite sound playback.");
        }
    }

    private void Update()
    {
        if (canFish && Input.GetMouseButtonDown(0) && !isFishing)
        {
            StartCoroutine(FishingRoutine()); // Start the fishing coroutine
        }

        if (fishBiting && Input.GetMouseButtonDown(0))
        {
            fishBiting = false; // Stop the biting state
            if (exclamationMark != null)
            {
                exclamationMark.SetActive(false);
            }
            Fish(); // Catch the fish
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            canFish = true;
            InGameMessage.text = "You can fish here! :3";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            canFish = false;
            InGameMessage.text = "You can't fish here anymore :(";
        }
    }

    private IEnumerator FishingRoutine()
    {
        isFishing = true;

        // Play fishing animation
        if (animator != null)
        {
            animator.SetTrigger("StartFishing"); // Assumes you have a trigger parameter called "StartFishing"
        }

        // Wait for a random duration before a fish is on the hook
        float waitTime = Random.Range(3f, 5f); // Random wait between 3 to 5 seconds
        yield return new WaitForSeconds(waitTime);

        // Play fish bite sound and display exclamation mark
        if (fishBiteAudioSource != null && fishBiteSound != null)
        {
            fishBiteAudioSource.clip = fishBiteSound;
            fishBiteAudioSource.Play();
            StartCoroutine(StopAudioAfterDelay(fishBiteAudioSource, 1f)); // Stop the sound after 0.5 seconds (adjust as needed)
        }

        if (exclamationMark != null)
        {
            exclamationMark.SetActive(true);
        }

        // Set state for the fish biting
        fishBiting = true;

        // Wait for a maximum of 1 second for the player to respond
        yield return new WaitForSeconds(1f);

        // Check if the player missed the response window
        if (fishBiting)
        {
            fishBiting = false;
            if (exclamationMark != null)
            {
                exclamationMark.SetActive(false);
            }
            InGameMessage.text = "Missed the fish! :o";
        }

        isFishing = false;
    }

    private void Fish()
    {
        int randomIndex = Random.Range(0, fishList.Count);
        FishData caughtFish = fishList[randomIndex];

        // Display the catch message
        if (catchMessageUI != null)
        {
            catchMessageUI.DisplayCatchMessage(caughtFish.fishName);
        }

        // Create an Item and add to inventory
        if (inventoryManager != null)
        {
            int fishPrice = caughtFish.price;
            Item fishItem = new Item(caughtFish.fishName, 1, caughtFish.fishSprite, caughtFish.fishDescription, fishPrice, true);
            inventoryManager.AddItem(fishItem);
        }
    }

    private IEnumerator StopAudioAfterDelay(AudioSource source, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (source.isPlaying)
        {
            source.Stop(); // Stop the audio after the delay
        }
    }
}

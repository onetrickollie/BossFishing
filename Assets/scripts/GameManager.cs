using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Vector3 playerPosition = Vector3.zero;
    private Dictionary<string, Vector3> sceneSpawnPoints = new Dictionary<string, Vector3>(); // Store spawn points for each scene
    public string currentScene; // Track the current scene
    public List<Item> inventory = new List<Item>(); // Persistent inventory list
    public int playerGold = 0; // Wallet system to track gold

    private int equippedRodIndex = 0; // Main variable for tracking the equipped rod

    public event Action<int> OnGoldChanged; // Event for gold changes
    public event Action<float> OnVolumeChanged; // Event for volume changes

    private float volume = 1f; // Default volume level (0 to 1)

    // New Rod-related variables
    // Removed redundant currentRodIndex for clarity
    public List<RodData> availableRods; // List of available rods (assignable in the Inspector)

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to switch the player's rod
    public bool BuyRod(int rodIndex)
    {
        if (rodIndex >= 0 && rodIndex < availableRods.Count)
        {
            RodData selectedRod = availableRods[rodIndex];
            if (playerGold >= selectedRod.price)
            {
                DeductGold(selectedRod.price);
                EquipRod(rodIndex);
                Debug.Log($"Bought and equipped {selectedRod.rodName}!");
                return true;
            }
            else
            {
                Debug.Log("Not enough gold to buy this rod.");
                return false;
            }
        }
        return false;
    }

    // Method to set the spawn point for a specific scene
    public void SetSpawnPointForScene(string sceneName, Vector3 spawnPoint)
    {
        if (sceneSpawnPoints.ContainsKey(sceneName))
        {
            sceneSpawnPoints[sceneName] = spawnPoint;
        }
        else
        {
            sceneSpawnPoints.Add(sceneName, spawnPoint);
        }
    }

    // Method to get the spawn point for the current scene
    public Vector3 GetSpawnPointForScene(string sceneName)
    {
        if (sceneSpawnPoints.ContainsKey(sceneName))
        {
            return sceneSpawnPoints[sceneName];
        }
        return Vector3.zero; // Default position if no specific spawn point is set
    }

    public void SetCurrentScene(string sceneName)
    {
        currentScene = sceneName;
    }

    // Inventory management methods
    public void AddItemToInventory(Item item)
    {
        if (!inventory.Contains(item))
        {
            inventory.Add(item);
            Debug.Log($"Added {item.itemName} to the inventory.");
        }
    }

    public List<Item> GetInventory()
    {
        return inventory;
    }

    // Wallet methods
    public void AddGold(int amount)
    {
        playerGold += amount;
        OnGoldChanged?.Invoke(playerGold);
        Debug.Log($"Gold added. Current balance: {playerGold}");
    }

    public void DeductGold(int amount)
    {
        playerGold = Mathf.Max(0, playerGold - amount);
        OnGoldChanged?.Invoke(playerGold);
        Debug.Log($"Gold deducted. Current balance: {playerGold}");
    }

    // Save player position
    public void SavePlayerPosition(Vector3 position)
    {
        playerPosition = position;
        Debug.Log($"Player position saved: {playerPosition}");
    }

    // Volume control methods
    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume); // Clamp the volume between 0 and 1
        OnVolumeChanged?.Invoke(volume);
        Debug.Log($"Volume set to: {volume}");
    }

    public int GetEquippedRodIndex()
    {
        Debug.Log($"GetEquippedRodIndex called: {equippedRodIndex}");
        return equippedRodIndex;
    }

    public void EquipRod(int rodIndex)
    {
        equippedRodIndex = rodIndex;
        Debug.Log($"Equipped rod index set to: {rodIndex}");
    }

    public float GetVolume()
    {
        return volume;
    }

    // Method to handle returning to the main menu
    public void ReturnToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("welcome");
    }
}

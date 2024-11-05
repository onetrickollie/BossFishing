using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BackpackUI : MonoBehaviour
{
    public GameObject backpackPanel; // Main panel for the backpack UI
    public Transform backpackContentParent; // Parent for the item entries
    public GameObject backpackItemTemplate; // Template prefab for each item

    private void Start()
    {
        backpackPanel.SetActive(false); // Hide backpack initially
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Toggle backpack with 'E'
        {
            backpackPanel.SetActive(!backpackPanel.activeSelf);
            if (backpackPanel.activeSelf)
            {
                UpdateBackpackUI();
            }
            else
            {
                ClearBackpackUI();
            }
        }
    }

    private void UpdateBackpackUI()
    {
        ClearBackpackUI();

        // Loop through each item in the backpack
        foreach (ItemData item in GameManager.Instance.backpackItems)
        {
            if (item is FishData fishData)
            {
                // Instantiate a new item UI from the template
                GameObject itemUI = Instantiate(backpackItemTemplate, backpackContentParent);

                // Set the name and icon of the caught fish
                TextMeshProUGUI itemNameText = itemUI.GetComponentInChildren<TextMeshProUGUI>();
                Image itemIconImage = itemUI.GetComponentInChildren<Image>();

                itemNameText.text = fishData.itemName; // Set the fish name from FishData
                itemIconImage.sprite = fishData.itemIcon; // Set the fish icon from FishData

                // Ensure the item UI is active (in case the prefab is initially disabled)
                itemUI.SetActive(true);
            }
        }
    }

    private void ClearBackpackUI()
    {
        foreach (Transform child in backpackContentParent)
        {
            Destroy(child.gameObject);
        }
    }
}

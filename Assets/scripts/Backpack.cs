using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BackpackUI : MonoBehaviour
{
    public GameObject backpackPanel;
    public GameObject backpackItemPrefab;
    public Transform backpackContentParent;

    private void Start()
    {
        backpackPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
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

        foreach (ItemData item in GameManager.Instance.backpackItems)
        {
            GameObject itemUI = Instantiate(backpackItemPrefab, backpackContentParent);

            TextMeshProUGUI itemNameText = itemUI.GetComponentInChildren<TextMeshProUGUI>();
            Image itemIconImage = itemUI.GetComponentInChildren<Image>();

            itemNameText.text = item.itemName;
            itemIconImage.sprite = item.itemIcon;
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

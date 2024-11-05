using UnityEngine;
using UnityEngine.UI;

public class BackpackUI : MonoBehaviour
{
    public Text backpackText;

    private void Update()
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

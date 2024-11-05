using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif

// this script is used to create a new asset in the project window
// even for multiple sprites in a sprite sheet hell yeah
[CreateAssetMenu(fileName = "New Fish", menuName = "Inventory/Fish")]
public class FishData : ItemData
{
    // Add any fish-specific properties here if needed

#if UNITY_EDITOR
    [MenuItem("Tools/Create Fish Data Assets")]
    public static void CreateFishDataAssets()
    {
        // Specify the path to the folder with your fish sprites
        string spritesFolderPath = "Assets/FishSprites";
        string assetsFolderPath = "Assets/FishDataAssets";

        // Ensure the output folder exists
        if (!AssetDatabase.IsValidFolder(assetsFolderPath))
        {
            AssetDatabase.CreateFolder("Assets", "FishDataAssets");
        }

        // Load all assets (including multiple sprites in a sprite sheet)
        string[] guids = AssetDatabase.FindAssets("t:Sprite", new[] { spritesFolderPath });
        foreach (string guid in guids)
        {
            string spritePath = AssetDatabase.GUIDToAssetPath(guid);
            Object[] sprites = AssetDatabase.LoadAllAssetsAtPath(spritePath);

            // Loop through each sprite within the sprite sheet or file
            foreach (Object obj in sprites)
            {
                if (obj is Sprite sprite)
                {
                    // Create a new FishData asset for each sprite
                    FishData fishData = ScriptableObject.CreateInstance<FishData>();
                    fishData.itemName = sprite.name; // Set the name from the sprite's name
                    fishData.itemIcon = sprite; // Set the icon to the sprite

                    // Save the FishData asset
                    string assetPath = Path.Combine(assetsFolderPath, sprite.name + ".asset");
                    AssetDatabase.CreateAsset(fishData, assetPath);
                }
            }
        }

        // Refresh the AssetDatabase to show new assets in the Project window
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("FishData assets created successfully!");
    }
#endif
}

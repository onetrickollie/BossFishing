using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerInteraction : MonoBehaviour
{
    public Tilemap interactableTilemap; // Assign your interactable Tilemap in the Inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Assuming player is tagged as "Player"
        {
            Vector3Int tilePosition = interactableTilemap.WorldToCell(transform.position);
            TileBase tile = interactableTilemap.GetTile(tilePosition);

            if (tile != null)
            {
                // Check for tile-specific interactions based on tile type or name
                Debug.Log("Interacted with tile: " + tile.name);
                // Add interaction-specific code here based on tile type, name, or properties
            }
        }
    }
}

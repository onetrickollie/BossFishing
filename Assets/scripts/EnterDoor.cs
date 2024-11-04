using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnterDoor : MonoBehaviour
{
    public string sceneName;
    public Vector3 indoorPosition;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
         Debug.Log("OnTriggerEnter2D activated");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Enter Door");
            SceneManager.LoadScene(sceneName);
            collision.transform.position = indoorPosition;
        }
    }
}

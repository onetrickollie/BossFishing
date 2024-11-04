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
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(sceneName);
            collision.transform.position = indoorPosition;
        }
    }
}

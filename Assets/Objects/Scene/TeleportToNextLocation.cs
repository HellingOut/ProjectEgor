using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToNextLocation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.name);
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("FirstTestLevel");

        }
    }
}

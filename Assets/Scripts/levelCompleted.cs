using UnityEngine;
using UnityEngine.SceneManagement;

public class levelCompleted : MonoBehaviour
{
    private GameObject lvlManager;
    private levelManager lvlTracker;
    private int sceneIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lvlManager = GameObject.Find("levelManager");
        lvlTracker = lvlManager.GetComponent<levelManager>();
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            lvlTracker.lvlUnlock[sceneIndex - 1] = true;
            SceneManager.LoadScene("LevelCleared");
        }
    }
}

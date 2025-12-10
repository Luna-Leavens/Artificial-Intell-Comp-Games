using UnityEngine;
using UnityEngine.SceneManagement;

public class gameoverReturn : MonoBehaviour
{
    private GameObject lvlManager;   
    private levelManager lvlTracker;

    void Start()
    {
        lvlManager = GameObject.Find("levelManager");
        lvlTracker = lvlManager.GetComponent<levelManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {   
            SceneManager.LoadScene(lvlTracker.curLvlIndex);
        } else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class unlockAll : MonoBehaviour
{
    private GameObject lvlManager;
    private levelManager lvlTracker;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lvlManager = GameObject.Find("levelManager");
        lvlTracker = lvlManager.GetComponent<levelManager>();
    }

    void OnMouseDown()
    {
        for (int i = 0; i < lvlTracker.lvlUnlock.Length; i++)
        {
            lvlTracker.lvlUnlock[i] = true;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}

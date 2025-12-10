using UnityEngine;
using UnityEngine.SceneManagement;

public class levelCompReturn : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {   
            SceneManager.LoadScene("LevelSelect");
        } else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}

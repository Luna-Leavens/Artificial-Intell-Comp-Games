using UnityEngine;
using UnityEngine.SceneManagement;

public class goToMenu : MonoBehaviour
{
    [SerializeField] private int sceneIndex;
    
    void OnMouseDown()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}

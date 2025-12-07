using UnityEngine;
using UnityEngine.SceneManagement;

public class goToLvl : MonoBehaviour
{
    [SerializeField] private int lvlIndex;
    [SerializeField] private Sprite unlockSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private GameObject lvlManager;
    private levelManager lvlTracker;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lvlManager = GameObject.Find("levelManager");
        lvlTracker = lvlManager.GetComponent<levelManager>();
        if (lvlTracker.lvlUnlock[lvlIndex-2])
        {
            spriteRenderer.sprite = unlockSprite;
        }
    }
    void OnMouseDown()
    {
        SceneManager.LoadScene(lvlIndex);
    }
}

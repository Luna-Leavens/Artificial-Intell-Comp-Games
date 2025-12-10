using UnityEngine;

public class boxToggleAuto : MonoBehaviour
{
    [SerializeField] private float time;
    private float timeReset;
    [SerializeField] private BoxCollider2D col;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite solidSprite;
    [SerializeField] private Sprite emptySprite;
    [SerializeField] private bool red;
    private bool activeRed;
    
    void Start()
    {
        timeReset = time;
        activeRed = false;
    }
    
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            activeRed = !activeRed;
            time = timeReset;
        }
        
        if (activeRed)
        {
            if (red)
            {
                col.enabled = true;
                spriteRenderer.sprite = solidSprite;
            } else
            {
                col.enabled = false;
                spriteRenderer.sprite = emptySprite;
            }
        } else
        {
            if (red)
            {
                col.enabled = false;
                spriteRenderer.sprite = emptySprite;
            } else {
                col.enabled = true;
                spriteRenderer.sprite = solidSprite;
            }
        }
    }
}

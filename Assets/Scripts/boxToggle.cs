using UnityEngine;

public class boxToggle : MonoBehaviour
{
    [SerializeField] private pressButton triggercheck;
    [SerializeField] private BoxCollider2D col;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite solidSprite;
    [SerializeField] private Sprite emptySprite;
    [SerializeField] private bool red;
    
    void Update()
    {
        if (triggercheck.trigger)
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

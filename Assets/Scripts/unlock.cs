using UnityEngine;

public class unlock : MonoBehaviour
{
    [SerializeField] private pressButton triggercheck;
    [SerializeField] private BoxCollider2D col;
    
    [SerializeField] private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (triggercheck.trigger)
        {
            col.enabled = false;
            spriteRenderer.enabled = false;
        } else
        {
            
            col.enabled = true;
            spriteRenderer.enabled = true;
        }
    }
}

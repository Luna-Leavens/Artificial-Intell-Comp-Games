using UnityEngine;

public class pressButton : MonoBehaviour
{
    private GameObject currentInteractionObject;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [SerializeField] public Sprite pressedSprite;
    [SerializeField] public Sprite unpressedSprite;
    [SerializeField] public bool trigger;
    
    [SerializeField] private bool permanentPress;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        trigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentInteractionObject != null)
        {
            trigger = true;
            spriteRenderer.sprite = pressedSprite;
        } else if (!permanentPress)
        {
            trigger = false;
            spriteRenderer.sprite = unpressedSprite;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Companion") )
        {
            currentInteractionObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == currentInteractionObject)
        {
            currentInteractionObject = null;
        }
    }
}

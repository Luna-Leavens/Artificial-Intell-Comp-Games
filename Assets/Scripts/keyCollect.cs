using UnityEngine;

public class keyCollect : MonoBehaviour
{
    private GameObject currentInteractionObject;
    [SerializeField] public int keysCollected;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            currentInteractionObject = collision.gameObject;
            Destroy(currentInteractionObject);
            currentInteractionObject = null;
            keysCollected++;
        }
    }
}

using UnityEngine;

public class coinCollect : MonoBehaviour
{
    private GameObject currentInteractionObject;
    [SerializeField] public int coinsCollected;
    [SerializeField] private AudioSource coinSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            currentInteractionObject = collision.gameObject;
            Destroy(currentInteractionObject);
            currentInteractionObject = null;
            coinsCollected++;
            coinSound.Play();
        }
    }
}

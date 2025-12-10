using UnityEngine;

public class unlock : MonoBehaviour
{
    [SerializeField] private keyCollect keyCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(keyCount.keysCollected >= 1)
            {
                keyCount.keysCollected--;
                Destroy(gameObject);
            }
        }
    }
}

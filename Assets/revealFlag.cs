using UnityEngine;

public class revealFlag : MonoBehaviour
{
    [SerializeField] private GameObject flag;

    public void activateFlag()
    {
        flag.SetActive(true);
        Destroy(gameObject);
    }
}

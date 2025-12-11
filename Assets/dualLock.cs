using UnityEngine;

public class dualLock : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite locked;
    [SerializeField] private Sprite lockedHalf;
    [SerializeField] private Sprite unlocked;
    [SerializeField] private pressButton unlock1;
    [SerializeField] private pressButton unlock2;
    [SerializeField] private revealFlag unlockGoal;

    // Update is called once per frame
    void Update()
    {
        if (unlock1.trigger || unlock2.trigger)
        {
            spriteRenderer.sprite = lockedHalf;
        } else
        {
            spriteRenderer.sprite = locked;
        }
        if (unlock1.trigger & unlock2.trigger)
        {
            spriteRenderer.sprite = unlocked;
            unlockGoal.activateFlag();
            this.enabled = false;
        }
    }
}

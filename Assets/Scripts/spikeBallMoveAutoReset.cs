using UnityEngine;

public class spikeBallMoveAutoReset : MonoBehaviour
{
    [SerializeField] private pressButton moveCheck;
    [SerializeField] private pressButton resetCheck;
    private Vector3 resetPos;
    [SerializeField] private Vector3 movePos;
    private Vector3 targetPos;

    void Start()
    {
        resetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCheck.trigger)
        {
            targetPos = movePos;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 5f * Time.deltaTime);
        } else {
            targetPos = resetPos;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 1f * Time.deltaTime);
        }
        
        
    }
}

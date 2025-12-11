using UnityEngine;

public class autoSpikeBallMove : MonoBehaviour
{
    [SerializeField] private pressButton stopCheck;
    [SerializeField] private Vector3 homePos;
    [SerializeField] private Vector3 resetPos;

    // Update is called once per frame
    void Update()
    {
        if (!stopCheck.trigger)
        {
        transform.position = Vector3.MoveTowards(transform.position, resetPos, 5f * Time.deltaTime);

        if (Vector3.Distance(transform.position, resetPos) <= 0.05f)
        {
            transform.position = homePos;
        }
        }
    }
}

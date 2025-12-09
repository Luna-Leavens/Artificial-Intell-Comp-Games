using UnityEngine;

public class companionMovement : MonoBehaviour
{
    private Vector3 waypoint;
    [SerializeField] GameObject player;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private BoxCollider2D playerCol;
    [SerializeField] private bool landed;
    [SerializeField] private bool following;
    [SerializeField] private Rigidbody2D rb;

    private AStarGrid moveGrid;
    private List openList;
    private List closedList;
    private List movementList;
    private GridBlocks startingBlock;
    private GridBlocks goalBlock;

    void Start()
    {
        following = true;
        landed = false;
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), playerCol, true);
    }
    void Update()
    {
        if (!following && !landed)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoint, 4.5f * Time.deltaTime);
        } else if (!Physics2D.OverlapCircle(transform.position, .5f, playerLayer) && !landed)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 4.5f * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            landed = !landed;
            if (landed)
            {
                rb.gravityScale = 1f;
            }
            else
            {
                rb.gravityScale = 0f;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            waypoint = Input.mousePosition;
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(waypoint);
            waypoint = worldPosition;
            following = false;
            landed = false;
            rb.gravityScale = 0f;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            landed = false;
            rb.gravityScale = 0f;
            following = true;
        }

    }

    private void SetStartBlock() {
        if (startingBlock != null) {
            return;
        }

        foreach (GridBlocks start in moveGrid.blocks) {
            Vector3 sphereUse = new Vector3(start.getX(), start.getY(), 0.0f);

            Collider[] findStart = Physics.OverlapSphere(sphereUse, 1);

            foreach (var objectCompare in findStart) {
                if (objectCompare.tag == "Companion") {
                    startingBlock = start;
                }
            }
        }
    }

    private void SetGoalBlock() {
        
    }
    
    private void AStarFind() {
        
    }
}

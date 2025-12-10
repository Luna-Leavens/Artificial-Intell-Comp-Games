using System;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

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

    private void SetGoalBlock(Vector3 goal) {
        foreach (GridBlocks goalFind in moveGrid.blocks) {
            float compareX = goalFind.getX() / goal.x;
            float compareY = goalFind.getY() / goal.y;
            
            if ((0.5f < compareX) && (compareX < 1.5f) && (0.5f < compareY) && (compareY < 1.5f)) {
                goalBlock = goalFind;
            }
        }
    }
    
    private void AStarFind(Vector3 pos) {
        SetStartBlock();
        SetGoalBlock(pos);

        GridBlocks current;
        GridBlocks parentSet;
        GridBlocks currentNeighbor;

        // Sets details for starting block before main AStar loop

        int g = 0;
        parentSet = startingBlock;
        int h = Mathf.Abs(startingBlock.row - goalBlock.row) + Mathf.Abs(startingBlock.column - goalBlock.column);
        int f = g + h;

        startingBlock.setManhatten(f, g, h, parentSet);

        openList.push(startingBlock);

        g++;

        // Main 4 Way AStar Loop (May update with diagonal movement later)

        current = openList.pop();

        while (current.row != goalBlock.row && current.column != goalBlock.column) {
            if (current != startingBlock) {
                current = openList.pop();
            }

            if (current.row == goalBlock.row && current.column == goalBlock.column) {
                break;
            }

            for (int i = 0; i < 4; i++) {
                int neighborF;
                int neighborH;
                bool flag = false;

                currentNeighbor = moveGrid.getNeighbor(current, i);

                foreach (GridBlocks closed in closedList.list) {
                    if (currentNeighbor.row == closed.row && currentNeighbor.column == closed.column) {
                        flag = true;
                    }

                    if (flag) {
                        continue;
                    }

                    neighborH = Mathf.Abs(currentNeighbor.row - goalBlock.row) + Mathf.Abs(currentNeighbor.column - goalBlock.column);
                    neighborF = neighborH + g;

                    currentNeighbor.setManhatten(neighborF, g, neighborH, current);

                    openList.push(currentNeighbor);
                }
            }

            // Sorts OpenList in order from blocks of highest F to lowest F

            while (openList.list[0].getF() < openList.list[1].getF() && openList.list[openList.length() - 1].getF() > openList.list[openList.length() - 2].getF()) {
				foreach (GridBlocks swapping in openList.list) {
                	openList.swap(swapping);
            	}
			}

            // Pushes the current block to the closed list and updates G value;

            closedList.push(current);

			g = g + current.getG();
        }

        // Traverses parents of the nodes to add them to the list used for moving companion

        while (current.getG() != 0) {
            movementList.push(current);

			current = current.getParent();
        }
    }
}

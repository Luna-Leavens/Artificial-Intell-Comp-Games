using System;
using System.Collections;
using System.Numerics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class companionMovement : MonoBehaviour
{
    private Vector3 waypoint;
    [SerializeField] GameObject player;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private BoxCollider2D playerCol;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int gridLength;
	[SerializeField] private int gridWidth;
    [SerializeField] private Grid useGrid;
    private bool landed;
    private bool following;
    private AStarGrid moveGrid;
    private List openList;
    private List closedList;
    private List movementList;
    private GridBlocks startingBlock;
    private GridBlocks goalBlock;
    private GridBlocks[] gridBlocks;
	private int gridRow;
	private int gridColumn;

    void Start()
    {
        following = true;
        landed = false;
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), playerCol, true);

        // Populates Grid Once

        int blockSize = (int) useGrid.cellSize.x;

		gridRow = gridWidth / blockSize;
		gridColumn = gridLength / blockSize;
        gridBlocks = new GridBlocks[gridRow * gridColumn];

		populateGrid(blockSize);

		if (gridBlocks != null) {
			moveGrid = new AStarGrid(gridLength, gridWidth, useGrid, gridBlocks);
		}
    }
    void Update()
    {
        if (!following && !landed)
        {
            // transform.position = Vector3.MoveTowards(transform.position, waypoint, 4.5f * Time.deltaTime);

            GridBlocks currentMove = movementList.pop();

            Vector3 moveTo = new Vector3(currentMove.getX(), currentMove.getY(), transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, moveTo, 4.5f * Time.deltaTime);
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

            /*
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(waypoint);
            waypoint = worldPosition;
            following = false;
            landed = false;
            rb.gravityScale = 0f;
            */

            AStarFind(waypoint);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            landed = false;
            rb.gravityScale = 0f;
            following = true;
        }

    }

    private void SetStartBlock() {
        for (int i = 0; i < moveGrid.blocks.Length; i++) {
            Vector2 sphereUse = new Vector2(moveGrid.blocks[i].getX(), moveGrid.blocks[i].getY());

            Collider2D findStart = Physics2D.OverlapCircle(sphereUse, 0.5f);

            if (findStart != null) {
                if (findStart.gameObject.tag == "Companion") {
                    startingBlock = moveGrid.blocks[i];
                }
            }
        }
    }

    private void SetGoalBlock(Vector3 goal) {
        foreach (GridBlocks goalFind in gridBlocks) {
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
                    if (currentNeighbor.row == closed.row && currentNeighbor.column == closed.column || currentNeighbor.obstacle == true) {
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

    void populateGrid (int blockSize) {
		float startingX = gridLength / 2 * -1;
		float startingY = gridWidth / 2 * -1; 
		int arrayIt = 0;

		for (int i = 0; i < gridRow; i++) {
			for (int j = 0; j < gridColumn; j++) {
                float saveX = startingX + (blockSize * j);
                float saveY = startingY + (blockSize * i);

                Vector2 findOtherObjects = new Vector2(saveX, saveY);

                Collider2D test = Physics2D.OverlapCircle(findOtherObjects, 0.5f);

                if (test != null) {

				    if (test.gameObject.tag == "Companion Avoid") {
					    GridBlocks input = new GridBlocks(saveX, saveY, true, i, j);

					    gridBlocks[arrayIt] = input;

					    arrayIt++;
				    }

				    else {
					    GridBlocks input = new GridBlocks(saveX, saveY, false, i, j);

					    gridBlocks[arrayIt] = input;

					    arrayIt++;
				    }
                }
			}
		}
	}
}

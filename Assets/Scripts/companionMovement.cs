using System;
using System.Collections;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
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
    [SerializeField] private Tilemap tiles;
    private bool landed;
    private bool following;
    private bool arrived;
    private bool unpathable;
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
        arrived = false;
        unpathable = false;
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), playerCol, true);
        movementList = new List();

        Debug.Log(tiles.size.x);
        Debug.Log(tiles.size.y);

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
        if (!following && !landed && !arrived && !unpathable)
        {
            if (movementList.length() == 0)
            {
                arrived = true;
                
                return;
            }

            GridBlocks currentMove = movementList.removeStart();

            Vector3 moveTo = new Vector3(currentMove.getX(), currentMove.getY(), transform.position.z);

            while (transform.position != moveTo)
            {
                transform.position = Vector3.MoveTowards(transform.position, moveTo, Time.deltaTime / 100);
            }
        } 
        
        else if (!Physics2D.OverlapCircle(transform.position, .5f, playerLayer) && !landed && !arrived)
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
                Vector3 goals = new Vector3(goalBlock.getX(), goalBlock.getY());

                while (transform.position != goals)
                {
                    transform.position = Vector3.MoveTowards(transform.position, goals, Time.deltaTime / 100);
                }

                rb.gravityScale = 0f;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            arrived = false;
            unpathable = false;

            waypoint = Input.mousePosition;
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(waypoint);
            waypoint = worldPosition;
            following = false;
            landed = false;
            rb.gravityScale = 0f;

            AStarFind(waypoint);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            landed = false;
            arrived = false;
            unpathable = false;
            rb.gravityScale = 0f;
            following = true;
        }
    }

    private void SetStartBlock() {
        
        for (int i = 0; i < moveGrid.blocks.Length; i++) {
            Vector2 sphereUse = new Vector2(moveGrid.blocks[i].getX(), moveGrid.blocks[i].getY());
            Vector2 size = new Vector2(0.4f, 0.4f);

            Collider2D findStart = Physics2D.OverlapBox(sphereUse, size, 0.0f);

            if (findStart != null) {
                if (findStart.gameObject.tag == "Companion") {
                    startingBlock = moveGrid.blocks[i];
                }
            }
        }
    }

    private void SetGoalBlock(Vector2 goal) {
        foreach (GridBlocks goalFind in gridBlocks) {
            float compareX = Mathf.Abs(goalFind.getX() - goal.x);
            float compareY = Mathf.Abs(goalFind.getY() - goal.y);
            
            if ((0.0f <= compareX) && (compareX <= 0.5f) && (0.0f <= compareY) && (compareY <= 0.5f)) {
                goalBlock = goalFind;

                break;
            }
        }
    }
    
    private void AStarFind(Vector2 pos) {
        SetStartBlock();
        SetGoalBlock(pos);

        GridBlocks current;
        GridBlocks parentSet;
        GridBlocks currentNeighbor;

        int checks = 0;

        openList = new List();
        closedList = new List();

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

        while (current.getH() != 0) {
            if (checks != 0) {
                if (openList.length() == 0)
                {
                    break;
                }

                if (closedList.length() == gridLength * gridWidth)
                {
                    break;
                }

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

                if (closedList.length() != 0) {

                    for (int j = 0; j < closedList.length(); j++) {
                        if (currentNeighbor.row == closedList.list[j].row && currentNeighbor.column == closedList.list[j].column) {
                            flag = true;
                        }
                    }
                }

                for (int j = 0; j < openList.length(); j++)
                {
                    if (currentNeighbor.row == openList.list[j].row && currentNeighbor.column == openList.list[j].column) {
                            flag = true;
                        }
                }

                if (currentNeighbor.obstacle == true)
                {
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

            // Sorts OpenList in order from blocks of highest F to lowest F

            if (openList.length() >= 2) {
                openList.sort();
            }

            // Pushes the current block to the closed list and updates G value;

            closedList.push(current);

			g++;

            checks++;
        }

        // Traverses parents of the nodes to add them to the list used for moving companion
        
        while (current.getG() != 0) {
            if (openList.length() == 0 || closedList.length() == gridLength * gridWidth)
            {
                unpathable = true;
                
                break;
            }

            movementList.push(current);

			current = current.getParent();
        }
    }

    void populateGrid (int blockSize) {

		float startingX = tiles.origin.x + 0.5f;
		float startingY = tiles.origin.y + 0.5f;
        int otherX = tiles.origin.x;
        int otherY = tiles.origin.y;
        Vector3Int findOtherTiles;
		int arrayIt = 0;

		for (int i = 0; i < gridRow; i++) {
			for (int j = 0; j < gridColumn; j++) {
                float saveX = startingX + (blockSize * j);
                float saveY = startingY + (blockSize * i);
                int anotherX = otherX + (blockSize * j);
                int anotherY = otherY + (blockSize * i);

                Vector2 findOtherObjects = new Vector2(saveX, saveY);
                Vector2 size = new Vector2(0.2f, 0.2f);
                findOtherTiles = new Vector3Int(anotherX, anotherY, 0);

                Collider2D test = Physics2D.OverlapBox(findOtherObjects, size, 0.0f);

                if (test != null) {

				    if (test.gameObject.tag == "Companion Avoid") {
					    GridBlocks input = new GridBlocks(saveX, saveY, true, i, j);

					    gridBlocks[arrayIt] = input;

					    arrayIt++;
				    }

                    else if (tiles.HasTile(findOtherTiles)){
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

                else {
                    if (tiles.HasTile(findOtherTiles))
                    {
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


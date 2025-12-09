using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateGrid : MonoBehaviour {
	[SerializeField] private int length;
	[SerializeField] private int width;
	private GridBlocks[] blocks;
	public AStarGrid grid;
	public Grid useGrid;
	private int row;
	private int column;

	public AStarGrid getGrid()
    {
        return grid;
    }
	
	// Use this for initialization
	void Start () {
		int blockSize = (int) useGrid.cellSize.x;

		Debug.Log(useGrid.cellSize.x);

		row = width / blockSize;
		column = length / blockSize;

		populateGrid(blockSize);

		if (blocks != null) {
			grid = new AStarGrid(length, width, useGrid, blocks);
		}
	}

	void populateGrid (int blockSize) {
		float startingX = length / 2 * -1;
		float startingY = width / 2 * -1; 
		int arrayIt = 0;

		for (int i = 0; i < row; i++) {
			for (int j = 0; j < column; j++) {
				float currentX = startingX + (blockSize * j);
				float currentY = startingY + (blockSize * i);

				Vector3 currentPos = new Vector3(currentX, currentY, 0.0f);

				Collider[] collide = Physics.OverlapSphere(currentPos, blockSize);

				foreach (var detect in collide) {
					if (detect.tag == "Companion Avoid") {
						GridBlocks input = new GridBlocks(currentX, currentY, true, i, j);

						blocks[arrayIt] = input;

						arrayIt++;
					}

					else {
						GridBlocks input = new GridBlocks(currentX, currentY, false, i, j);

						blocks[arrayIt] = input;

						arrayIt++;
					}
				}
			}
		}
	}
}

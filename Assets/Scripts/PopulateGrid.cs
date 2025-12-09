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

		row = width / blockSize;
		column = length / blockSize;

		populateGrid();

		if (blocks != null) {
			grid = new AStarGrid(length, width, useGrid, blocks);
		}
	}

	/* Needs Rewrite */ void populateGrid () {
		
	}
}

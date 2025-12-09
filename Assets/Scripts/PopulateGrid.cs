using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateGrid : MonoBehaviour {
	[SerializeField] private int length;
	[SerializeField] private int width;
	[SerializeField] private int gridSize;
	private GridBlocks[] blocks;
	public AStarGrid grid;
	private int row;
	private int column;

	public AStarGrid getGrid()
    {
        return grid;
    }
	
	// Use this for initialization
	void Start () {
		row = width / gridSize;
		column = length / gridSize;

		populateGrid();

		if (blocks != null) {
			// Will Fix | grid = new AStarGrid(length, width, gridSize, blocks);
		}
	}

	/* Needs Rewrite */ void populateGrid () {
		
	}
}

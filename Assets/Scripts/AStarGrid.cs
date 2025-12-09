using System;
using UnityEngine;

public class AStarGrid
{
    public int length; // Length of Grid
    public int width; // Width of Grid
    public int gridSize; // Size of Blocks in Grid
    public Grid baseGrid; // Grid used for the AStarGrid
    public GridBlocks[] blocks; // Array of GridBlocks with all the coordinates and boolean values

    public AStarGrid(int length, int width, int gridSize, Grid baseGrid, GridBlocks[] blocks)
    {
        if (length == 0 || width == 0 || gridSize == 0)
        {
            Console.WriteLine("Invalid Numbers");
        }
        else
        {
            this.length = length;

            this.width = width;

            this.gridSize = gridSize;
            
            this.baseGrid = baseGrid;

            this.blocks = blocks;
        }
    }

    public bool getBlockAt(int row, int column)
    {
        foreach (GridBlocks returnBlocks in blocks)
        {
            if (returnBlocks.row == row && returnBlocks.column == column)
            {
                return returnBlocks.obstacle;
            }
        }

        return false;
    }

    public float getX(int row, int column) 
    {
        foreach (GridBlocks returnBlocks in blocks)
        {
            if (returnBlocks.row == row && returnBlocks.column == column)
            {
                return returnBlocks.x;
            }
        }

        return 0f;
    }

    public float getY(int row, int column) 
    {
        foreach (GridBlocks returnBlocks in blocks)
        {
            if (returnBlocks.row == row && returnBlocks.column == column)
            {
                return returnBlocks.y;
            }
        }

        return 0f;
    }
    
    public void setBlockAt(float x, float y, bool obstacle, int row, int column)
    {
        int i = 0;

        foreach (GridBlocks setBlocks in blocks)
        {
            if (setBlocks.row == row && setBlocks.column == column)
            {
                blocks[i] = new GridBlocks(x, y, obstacle, row, column);
            }

            i++;
        }
    }

    public GridBlocks getNeighbor(GridBlocks block, string direction)
    {
        if (direction == "up")
        {
            if (block.row >= 0 && block.row < width / gridSize)
            {
                foreach (GridBlocks neighbor in blocks)
                {
                    if (neighbor.row == block.row - 1 && neighbor.column == block.column && !neighbor.obstacle)
                    {
                        return neighbor;
                    }
                }
            }
        }

        if (direction == "left")
        {
            if (block.column >= 0 && block.column < length / gridSize)
            {
                foreach (GridBlocks neighbor in blocks)
                {
                    if (neighbor.row == block.row && neighbor.column == block.column - 1 && !neighbor.obstacle)
                    {
                        return neighbor;
                    }
                }
            }
        }

        if (direction == "down")
        {
            if (block.row >= 0 && block.row < width / gridSize)
            {
                foreach (GridBlocks neighbor in blocks)
                {
                    if (neighbor.row == block.row + 1 && neighbor.column == block.column && !neighbor.obstacle)
                    {
                        return neighbor;
                    }
                }
            }
        }

        if (direction == "right")
        {
            if (block.column >= 0 && block.column < length / gridSize)
            {
                foreach (GridBlocks neighbor in blocks)
                {
                    if (neighbor.row == block.row && neighbor.column == block.column + 1 && !neighbor.obstacle)
                    {
                        return neighbor;
                    }
                }
            }
        }

        return block;
    }

}
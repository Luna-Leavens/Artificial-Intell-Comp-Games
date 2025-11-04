using System;

class AStarGrid
{
    public int height; // Height of Grid
    public int width; // Width of Grid
    public GridBlocks[] blocks; // Array of GridBlocks with all the coordinates and boolean values

    public AStarGrid(int height, int width, GridBlocks[] blocks)
    {
        if (height == 0 || width == 0)
        {
            Console.WriteLine("Invalid Numbers");
        }
        else
        {
            this.height = height;

            this.width = width;

            this.blocks = blocks;
        }
    }

    public bool getBlockAt(int x, int y)
    {
        foreach (GridBlocks returnBlocks in blocks)
        {
            if (returnBlocks.x == x && returnBlocks.y == y)
            {
                return returnBlocks.fly;
            }
        }

        return false;
    }
    
    public void setBlockAt(int x, int y, bool fly)
    {
        int i = 0;

        foreach (GridBlocks setBlocks in blocks)
        {
            if (setBlocks.x == x && setBlocks.y == y)
            {
                blocks[i] = new GridBlocks(x, y, fly);
            }

            i++;
        }
    }

}
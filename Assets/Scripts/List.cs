using System;

public class List
{
    public GridBlocks[] list;

    private int i = 0;

    public List()
    {}

    // Adds block to the start of the list
    public void push(GridBlocks current)
    {
        if (i == 0)
        {
            list[i] = current;

            i++;
        }
        else
        {
            int moveI = i;
            
            while (moveI != 0)
            {
                GridBlocks move = list[moveI];

                list[moveI+1] = move;

                moveI--;
            }

            list[0] = current;

            i++;
        }
    }

    // Returns the length of the list
    public int length()
    {
        return i + 1;
    }

    // Removes top block on the list
    public GridBlocks pop()
    {
        GridBlocks popped = list[i];

        list[i] = null;

        i--;

        return popped;
    }

    // Removes Block from start of list
    public GridBlocks removeStart()
    {
        GridBlocks removed = list[0];

        for (int j = 0; j < i; j++)
        {
            list[j] = list[j + 1];
        }

        list[i] = null;

        i--;

        return removed;
    }

    // Swaps block with lower f in the list with block with higher f in the list
    public void swap(GridBlocks swap)
    {
        int j = 1;

        foreach (GridBlocks potential in list)
        {   
            if (potential == swap)
            {
                if (j == 1)
                {
                    break;
                }

                if (list[j] == null)
                {
                    break;
                }

                if (potential.getF() < list[j].getF())
                {
                    GridBlocks save = list[j];

                    list[j] = potential;

                    list[j-1] = save;

                    break;
                }
            }

            j++;
        }
    }
}
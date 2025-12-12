using System;
using System.Diagnostics;

public class List
{
    public GridBlocks[] list;

    private int i = 0;

    public List()
    {
        list = new GridBlocks[10000];
    }

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
                GridBlocks move = list[moveI-1];

                list[moveI] = move;

                moveI--;
            }

            list[0] = current;

            i++;
        }
    }

    // Returns the length of the list
    public int length()
    {
        return i;
    }

    // Removes top block on the list
    public GridBlocks pop()
    {
        GridBlocks popped = list[i-1];

        list[i-1] = null;

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

    // Sorts blocks in order from lowest to highest f
    public void sort()
    {
        int largest = 0;
        int smallest = 10000000;

        for (int j = 0; j < i; j++)
        {
            if (largest < list[j].getF())
            {
                largest = list[j].getF();
            }

            if (smallest > list[j].getF())
            {
                smallest = list[j].getF();
            }
        }

        while(largest != list[0].getF() || smallest != list[i-1].getF())
        {
            for (int j = 0; j < i - 1; j++)
            {
                if (list[j].getF() < list[j+1].getF())
                {
                    GridBlocks save = list[j+1];

                    list[j+1] = list[j];

                    list[j] = save;
                }
            }
        }
    }
}
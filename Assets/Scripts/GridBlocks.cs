public class GridBlocks
{
    public float x; // X Coordinate of Block
    public float y; // y Coordinate of Block
    public bool obstacle; // If the Block has an obstacle to or not

    public int row;

    public int column;
    public int f;
    public int h;
    public int g;
    public GridBlocks parent;

    public GridBlocks(float x, float y, bool obstacle, int row, int column) {
        this.x = x;

        this.y = y;

        this.obstacle = obstacle;

        this.row = row;

        this.column = column;
    }

    public void setManhatten(int f, int g, int h, GridBlocks parent)
    {
        this.f = f;
        this.g = g;
        this.h = h;
        this.parent = parent;
    }

    public float getX()
    {
        return x;
    }

    public float getY()
    {
        return y;
    }
    public int getF()
    {
        return f;
    }

    public int getH()
    {
        return h;
    }

    public int getG()
    {
        return g;
    }

    public GridBlocks getParent()
    {
        return parent;
    }
}
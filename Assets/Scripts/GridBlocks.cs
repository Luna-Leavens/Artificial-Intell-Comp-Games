class GridBlocks
{
    public int x; // X Coordinate of Block
    public int y; // Y Coordinate of Block
    public bool fly; // If the Block is flyable to or not

    public GridBlocks(int x, int y, bool fly) {
        this.x = x;

        this.y = y;

        this.fly = fly;
    }
}
namespace Data
{
    public abstract class BoardFactory
    {
        public static IBoard CreateBoard(int width,int height)
        {
            return new Board(width, height); 
        }
    }
}

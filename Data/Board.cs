namespace Data
{
    internal class Board(int width, int height) : IBoard
    {
        public int Width { get; set; } = width;
        public int Height { get; set; } = height;
    }
  
}

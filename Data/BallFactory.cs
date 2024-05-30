namespace Data
{
    public abstract class BallFactory
    {
        public static IBall CreateBall(double radius, double x, double y)
        {
            Ball ball = new(radius)
            {
                X = x,
                Y = y
            };
            return ball;
        }
    }
}

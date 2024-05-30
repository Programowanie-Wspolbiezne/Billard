namespace Logic
{
    public class BallFactory
    {
        public static IBall CreateBall(double x, double y, double radius)
        {
            IBall ball = new Ball(Data.BallFactory.CreateBall(radius, x, y));
            return ball;
        }
    }
}

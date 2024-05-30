using Data;
using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicTests
{
    [TestClass]
     public class CollisionDetectorTest
    {


        private void OnLeftBallPropertyChangeTest(object sender, PropertyChangedEventArgs e)
        {
            Assert.IsNotNull((Logic.IBall)sender);
            Assert.IsInstanceOfType(sender, typeof(Logic.IBall));
            Logic.IBall ball = (Logic.IBall)sender;
            Assert.IsTrue(ball.Velocity.X < 0);

        }
        private void OnRightBallPropertyChangeTest(object sender, PropertyChangedEventArgs e)
        {
            Assert.IsNotNull((Logic.IBall)sender);
            Assert.IsInstanceOfType(sender, typeof(Logic.IBall));
            Logic.IBall ball = (Logic.IBall)sender;
            Assert.IsTrue(ball.Velocity.X > 0);
        }


        [TestMethod]
        public void testCollison()
        {
            Logic.IBall leftBall = Logic.BallFactory.CreateBall(49, 100, 10);
            leftBall.Velocity = new System.Numerics.Vector2(1, 0);
            leftBall.Start();
            Logic.IBall rightBall = Logic.BallFactory.CreateBall(61, 100, 10);
            rightBall.Velocity = new System.Numerics.Vector2(-1, 0);
            rightBall.Start();

            IBoard board = Data.BoardFactory.CreateBoard(1000, 1000);

            var are = new AutoResetEvent(false);
            leftBall.PropertyChanged += (s, e) => { are.Set(); };
            rightBall.PropertyChanged += (s, e) => { are.Set(); };


            ColisionDetector detector = new ColisionDetector(LoggerProvider.GetLogger());
            detector.Board = board;
            detector.AddBall(leftBall);
            detector.AddBall(rightBall);
            detector.Activate();

            var wasSignaled = are.WaitOne(timeout: TimeSpan.FromSeconds(5));
            Assert.IsTrue(wasSignaled);

        }
    }
}

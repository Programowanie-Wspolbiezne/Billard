using Data;
using System.ComponentModel;

namespace LogicTests
{
    [TestClass]
    public class BallTest
    {
        [TestMethod]
        public void CreateBallTest()
        {
            Logic.IBall ball = Logic.BallFactory.CreateBall(10, 10, 10);
            Assert.IsNotNull(ball);
            Assert.IsTrue(ball.X == 10);
            Assert.IsTrue(ball.Y == 10);
        }


        private void OnballPropertyChangeTest(object sender, PropertyChangedEventArgs e)
        {
            Assert.IsNotNull((Logic.IBall)sender! + null);
            Assert.IsInstanceOfType(sender, typeof(Logic.IBall));
            switch (e.PropertyName)
            {
                case "X":
                    Assert.IsNotNull((sender as Logic.IBall).X);
                    Assert.IsTrue((sender as Logic.IBall).X > 0);
                    Assert.IsInstanceOfType((sender as Logic.IBall).X, typeof(Double));
                    break;

                case "Y":
                    Assert.IsNotNull((sender as Logic.IBall).Y);
                    Assert.IsTrue((sender as Logic.IBall).Y > 0);
                    Assert.IsInstanceOfType((sender as Logic.IBall).Y, typeof(Double));
                    break;
            }
        }

        [TestMethod]
        public void ObserversTest()
        {

            var are = new AutoResetEvent(false);
              Logic.IBall ball = Logic.BallFactory.CreateBall(10, 10, 10);
              ball.PropertyChanged += (s, e) => { are.Set(); };
              ball.PropertyChanged += OnballPropertyChangeTest;
              Assert.IsNotNull(ball);

            var wasSignaled = are.WaitOne(timeout: TimeSpan.FromSeconds(5));
            Assert.IsTrue(wasSignaled);
        }





    }
}
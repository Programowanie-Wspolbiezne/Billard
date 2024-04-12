using Data;
using System.ComponentModel;

namespace DataTests
{
    [TestClass]
    public class BallTest
    {
        [TestMethod]
        public void CreateBallTest()
        {
            Data.IBall dball = BallFactory.createBall(8, 10, 10);
            Assert.IsTrue(dball != null);
            Assert.IsTrue(dball.X == 10);
            Assert.IsTrue(dball.Y == 10);
        }



        private void OnDballPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            Assert.IsNotNull((IBall)sender! + null);
            Assert.IsInstanceOfType(sender, typeof(IBall));
            switch (e.PropertyName)
            {
                case "X":
                    Assert.IsNotNull((sender as IBall).X);
                    Assert.IsInstanceOfType((sender as IBall).X, typeof(Double));
                    break;

                case "Y":
                    Assert.IsNotNull((sender as IBall).Y);
                    Assert.IsInstanceOfType((sender as IBall).Y, typeof(Double));
                    break;
            }
        }

        [TestMethod]
        public void ObserversTest()
        {
            Data.IBall dball = BallFactory.createBall(8, 10, 10);
            dball.PropertyChanged += OnDballPropertyChange;
            dball.X = 5; dball.Y = 10;
        }


        [TestMethod]
        public void SetterGetterTest()
        {
            Data.IBall dball = BallFactory.createBall(8, 10, 10);
            dball.X = 5; dball.Y = 10;
            Assert.IsTrue(dball.X == 5);
            Assert.IsTrue(dball.Y == 10);
        }
    }
}
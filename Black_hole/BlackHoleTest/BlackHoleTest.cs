using System;
using System.Threading.Tasks;
using Black_hole.Model;
using Black_hole.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlackHoleTest
{
    [TestClass]
    public class BlackHoleTest
    {
        private BlackHoleModel model = new BlackHoleModel(); // a tesztelendõ modell
        [TestInitialize]
        public void Initialize()
        {
            model.newGame(5);
            
        }

        [TestMethod]
        public void InitializeFine()
        {
            Assert.AreEqual(-1,model.table[2, 2]); // jó helyen van a fekete lyuk;
            int osszesPiroshajo = 0;
            foreach (int i in model.table)
            {
                if (i == 1)
                {
                    osszesPiroshajo++;
                }
            }
            Assert.AreEqual(4,osszesPiroshajo);//4 hajónak kéne lennie
            model.newGame(9);
            int osszeskekhajo = 0;
            foreach (int i in model.table)
            {
                if (i == 1)
                {
                    osszeskekhajo++;
                }
            }
            Assert.AreEqual(8,osszeskekhajo);//8 hajónak kéne lennie

            model.newGame(5);
            //jó helyeken vannak a hajók?
            Assert.AreEqual(1,model.table[0, 0]);
            Assert.AreEqual(1,model.table[1, 1]);
            Assert.AreEqual(1, model.table[0, 4]);
            Assert.AreEqual(1, model.table[1, 3]);
        }

        [TestMethod]
        public void TestStep()
        {
            model.newGame(5);

            model.Step(1, 1, 1, Direction.Horizontal); // leptetese jobbra
            Assert.AreEqual(0,model.table[1,1]);//kiuritette a helyet
            Assert.AreEqual(1,model.table[1,2]);//jó helyre rakta

            //most belemegyunk a feketelyukba
            model.Step(1, 2, 1, Direction.Vertical);
            Assert.AreEqual(0,model.table[1, 2]);
            //megnézzük, hogy kevesebb ûrhajó van-e a pályán
            int piroshajok = 0;
            foreach(int i in model.table)
            {
                if(i == 1)
                {
                    piroshajok++;
                }
            }
            Assert.AreEqual(3,piroshajok);
            //megnézzük, hogy a pont növekedett-e
            Assert.AreEqual(1, model.scores[0]);
        }
    }
}

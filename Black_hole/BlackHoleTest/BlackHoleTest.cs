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
        private BlackHoleModel model = new BlackHoleModel(); // a tesztelend� modell
        [TestInitialize]
        public void Initialize()
        {
            model.newGame(5);
            
        }

        [TestMethod]
        public void InitializeFine()
        {
            Assert.AreEqual(-1,model.table[2, 2]); // j� helyen van a fekete lyuk;
            int osszesPiroshajo = 0;
            foreach (int i in model.table)
            {
                if (i == 1)
                {
                    osszesPiroshajo++;
                }
            }
            Assert.AreEqual(4,osszesPiroshajo);//4 haj�nak k�ne lennie
            model.newGame(9);
            int osszeskekhajo = 0;
            foreach (int i in model.table)
            {
                if (i == 1)
                {
                    osszeskekhajo++;
                }
            }
            Assert.AreEqual(8,osszeskekhajo);//8 haj�nak k�ne lennie

            model.newGame(5);
            //j� helyeken vannak a haj�k?
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
            Assert.AreEqual(1,model.table[1,2]);//j� helyre rakta

            //most belemegyunk a feketelyukba
            model.Step(1, 2, 1, Direction.Vertical);
            Assert.AreEqual(0,model.table[1, 2]);
            //megn�zz�k, hogy kevesebb �rhaj� van-e a p�ly�n
            int piroshajok = 0;
            foreach(int i in model.table)
            {
                if(i == 1)
                {
                    piroshajok++;
                }
            }
            Assert.AreEqual(3,piroshajok);
            //megn�zz�k, hogy a pont n�vekedett-e
            Assert.AreEqual(1, model.scores[0]);
        }
    }
}

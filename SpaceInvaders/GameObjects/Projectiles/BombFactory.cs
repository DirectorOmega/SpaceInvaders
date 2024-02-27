using SpaceInvaders.GameState;
using SpaceInvaders.GraphicalObjects;
using System.Diagnostics;

namespace SpaceInvaders.GameObjects.Projectiles
{
    internal sealed class BombFactory
    {
        private static BombFactory pInstance;
        private Roller pRollerStrat;
        private ZigZag pZigZagStrat;
        private ThirdStrat pThirdStratStrat;

        private BombFactory()
        {
            pRollerStrat = new Roller();
            pZigZagStrat = new ZigZag();
            pThirdStratStrat = new ThirdStrat();
        }


        public static BombFactory getInstance()
        {
            if(pInstance == null)
            {
                pInstance = new BombFactory();
            }
            return pInstance;
        }

        public static Bomb getBomb()
        {
            BombFactory bf = BombFactory.getInstance();

            Bomb toReturn;
            toReturn = (Bomb)GhostManager.Find(GameObjectType.Bomb).Detatch();
            if(toReturn != null)
            {
                //Debug.Print("GhostManager Returned a Bomb!\n");
            }
            if (toReturn == null)
            {
                toReturn = new Bomb(SpriteID.Missile);
                toReturn.setName(GameObjectType.Bomb);
                int stratnum = GameStateManager.getRandomNumber(1, 4);
                switch (stratnum)
                {
                    case 1:
                        toReturn.setStrategy(bf.pRollerStrat);
                        break;
                    case 2:
                        toReturn.setStrategy(bf.pZigZagStrat);
                        break;
                    case 3:
                        toReturn.setStrategy(bf.pThirdStratStrat);
                        break;
                    default:
                        Debug.Assert(false);
                        toReturn.setStrategy(bf.pZigZagStrat);
                        break;
                }
            }
            toReturn.ResetCount();
            toReturn.ClearMark();
            return toReturn;
        }
    }
}


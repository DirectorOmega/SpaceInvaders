using SpaceInvaders.GraphicalObjects;

namespace SpaceInvaders.GameObjects
{
    internal sealed class ThirdStrat : BombStrat
    {

        readonly GameSprite f1, f2, f3, f4;

        public ThirdStrat()
        {
            f1 = GameSpriteManager.Find(SpriteID.ThirdShotF1);
            f2 = GameSpriteManager.Find(SpriteID.ThirdShotF2);
            f3 = GameSpriteManager.Find(SpriteID.ThirdShotF3);
            f4 = GameSpriteManager.Find(SpriteID.ThirdShotF4);
        }

        public override void Fall(Bomb b)
        {
            int count = b.getCount();
            if (count == 1)
            {
                b.getPSprite().swapSprite(f2);
            }
            else if (count == 16)
            {
                b.getPSprite().swapSprite(f3);
            }
            else if (count == 31)
            {
                b.getPSprite().swapSprite(f4);
            }
            else if (count == 46)
            {
                b.getPSprite().swapSprite(f1);
                b.ResetCount();
            }
        }
    }
}


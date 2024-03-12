using SpaceInvaders.GraphicalObjects;

namespace SpaceInvaders.GameObjects
{
    internal sealed class ZigZag : BombStrat
    {

        readonly GameSprite f1, f2, f3, f4;

        public ZigZag()
        {
            f1 = GameSpriteManager.Find(SpriteID.ZigZagF1);
            f2 = GameSpriteManager.Find(SpriteID.ZigZagF2);
            f3 = GameSpriteManager.Find(SpriteID.ZigZagF3);
            f4 = GameSpriteManager.Find(SpriteID.ZigZagF4);
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

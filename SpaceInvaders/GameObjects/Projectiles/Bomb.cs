using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GraphicalObjects;

namespace SpaceInvaders.GameObjects
{
    internal sealed class Bomb : GameObject
    {

        BombStrat fallStrat;
        public int cur;
       // public bool falling;

        public Bomb(SpriteID id, float posX = 0, float posY = 0) : base(id, posX, posY)
        {
            fallStrat = new NullFallStrat();
            //falling = false;
            cur = 0;
        }

        public void setStrategy(BombStrat bs)
        {
            fallStrat = bs;
        }

        public override void Update()
        {
            cur++;
            incrementY(-5.0f);
            fallStrat.Fall(this);
            base.Update();
        }

        public override void Accept(ColVistor other)
        {
            other.VisitBomb(this);
        }

        public void ResetCount()
        {
            cur = 0;
        }

        public int getCount()
        {
            return cur;
        }

        public override void VisitBottomWall(BottomWall bw)
        {
            Reactions.Reaction(this, bw);
        }

        public override void cClean()
        {
            
        }
    }
}

using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GraphicalObjects;

namespace SpaceInvaders.GameObjects
{
    internal sealed class Crab : Alien
    {
        public Crab(SpriteID crab, float posX, float posY)
            : base(crab, posX, posY)
        {

        }

        public override void Accept(ColVistor other)
        {
            other.VisitCrab(this);
        }

        public override int getScore() => 20;

        //public override void VisitShield(Shield s)
        //{
        //    Reactions.Reaction(this, s);
        //}

        //public override void VisitShieldColumn(ShieldColumn sc)
        //{
        //    Reactions.Reaction(this,)
        //}
        //public override Enum getName()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
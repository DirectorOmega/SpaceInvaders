using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GraphicalObjects;

namespace SpaceInvaders.GameObjects
{

    internal sealed class Missile : GameObject
    {
        float speed;
        public Missile(SpriteID id, float posX = 0, float posY = 0) : base(id, posX, posY)
        {
            speed = 12.0f;
        }

        //public override void Remove()
        //{
        //    this.poColObj.getColRect().Set(0, 0, 0, 0);
        //    base.Update();

        //    base.Remove();
        //}

        public override void Accept(ColVistor other)
        {
            other.VisitMissile(this);
        }

        public override void VisitShieldRoot(ShieldRoot shieldRoot)
        {
            Reactions.Reaction(this, shieldRoot);
        }

        public override void VisitUFORoot(UFORoot ur)
        {
            Reactions.Reaction(this, ur);
        }

        public override void VisitBombRoot(BombRoot br)
        {
            Reactions.Reaction(br, this);
        }

        public override void VisitBomb(Bomb b)
        {
            Reactions.Reaction(b, this);
        }

        public override void VisitTopwall(Topwall t)
        {
            Reactions.Reaction(this, t);
        }

        public override void VisitGrid(Grid g)
        {
            Reactions.Reaction(g, this);
        }

        public override void VisitColumn(Column c)
        {
            Reactions.Reaction(c, this);
        }

        public override void VisitOcto(Octo o)
        {
            Reactions.Reaction(this, o);
        }

        public override void VisitSquid(Squid s)
        {
            Reactions.Reaction(this, s);
        }

        public override void VisitCrab(Crab c)
        {
            Reactions.Reaction(c, this);
        }

        public override void Update()
        {
            this.incrementY(speed);
            base.Update();
        }

        public override void cClean()
        {
           
        }
    }

}

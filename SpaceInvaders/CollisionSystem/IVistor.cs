using System.Diagnostics;
using SpaceInvaders.GameObjects;
using SpaceInvaders.GameObjects.Projectiles;
using SpaceInvaders.PCS;

namespace SpaceInvaders.CollisionSystem
{
    abstract class ColVistor : PCSNode
    {
        abstract public void Accept(ColVistor other);

        public virtual void VisitNullGameObject(NullGameObject n)
        {
            Debug.WriteLine("Vist by NullGameObject, Something is wrong");
            Debug.Assert(false);
        }

        public virtual void VisitNoiseRoot(GNoiseRoot gNoiseRoot)
        {
            Debug.WriteLine("Vist by GNoiseRoot, Something is wrong");
            Debug.Assert(false);
        }

        public virtual void VisitNoisePoint(GNoisePoint gnp)
        {
            Debug.WriteLine("Vist by GNoisepoint, Something is wrong");
            Debug.Assert(false);
        }

        //internal void VisitRightBumper(RightBumper rightBumper)
        //{
        //    throw new NotImplementedException();
        //}

        public virtual void VisitBombRoot(BombRoot br)
        {
            Debug.WriteLine("Vist by BombRoot not Implemented");
            Debug.Assert(false);
        }
        public virtual void VisitBomb(Bomb b)
        {
            Debug.WriteLine("Visit by Bomb not Implemented");
            Debug.Assert(false);
        }

        //internal void VisitLeftBumper(LeftBumper leftBumper)
        //{
        //    throw new NotImplementedException();
        //}
        public virtual void VisitUFORoot(UFORoot ur)
        {
            Debug.WriteLine("Visit by UFORoot not Implemented");
            Debug.Assert(false);
        }
        public virtual void VisitUFO(UFO u)
        {
            Debug.WriteLine("Visit by UFO not Implemented");
            Debug.Assert(false);
        }

        public virtual void VisitSidewall(Sidewall s)
        {
            Debug.WriteLine("Visit by Sidewall not Implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShield(Shield s)
        {
            Debug.WriteLine("Visit by Shield not Implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShieldColumn(ShieldColumn sc)
        {
            Debug.Assert(false);
        }

        public virtual void VisitShieldBrick(ShieldBrick sb)
        {
            Debug.Assert(false);
        }

        public virtual void VisitShieldRoot(ShieldRoot shieldRoot)
        {
            Debug.Assert(false);
        }

        public virtual void VisitShip(Ship p)
        {
            Debug.WriteLine("Visit by Ship not Implemented");
            Debug.Assert(false);
        }

        public virtual void VisitTopwall(Topwall t)
        {
            Debug.WriteLine("Visit by Topwall not Implemented");
            Debug.Assert(false);
        }

        public virtual void VisitBottomWall(BottomWall bw)
        {
            Debug.WriteLine("Visit byBottomwall not Implemented");
            Debug.Assert(false);
        }

        public virtual void VisitBumper(Bumper b)
        {
            Debug.WriteLine("Visit by Bumper not Implemented");
            Debug.Assert(false);
        }

        public virtual void VisitCrab(Crab c)
        {
            Debug.WriteLine("Visit by Crab not Implemented");
            Debug.Assert(false);
        }

        public virtual void VisitGrid(Grid g)
        {
            Debug.WriteLine("Visit by Grid not Implemented");
            Debug.Assert(false);
        }

        public virtual void VisitColumn(Column c)
        {
            Debug.WriteLine("Visit by Column not Implemented");
            Debug.Assert(false);
        }

        public virtual void VisitMissile(Missile m)
        {
            Debug.WriteLine("Visit by Missile not Implemented");
            Debug.Assert(false);
        }

        public virtual void VisitOcto(Octo o)
        {
            Debug.WriteLine("Visit by Octo not Implemented");
            Debug.Assert(false);
        }

        public virtual void VisitSquid(Squid s)
        {
            Debug.WriteLine("Visit by Squid not Implemented");
            Debug.Assert(false);
        }

        public virtual void VisitPlayer(PlayerAvatar p)
        {
            Debug.WriteLine("Visit by Player not Implemented");
            Debug.Assert(false);
        }
    }
}

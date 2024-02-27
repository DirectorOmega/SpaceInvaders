using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GraphicalObjects;
using SpaceInvaders.GameObjects.Player.PlayerStates;
using SpaceInvaders.GameObjects.Player.PlayerStates.MoveState;

namespace SpaceInvaders.GameObjects
{
    internal sealed class Ship : GameObject
    {
      //  ShipManager.State curState;
        float speed;
        MissileState MiState;
        MvState mvState;

        public Ship(SpriteID id, float posX = 0, float posY = 0) : base(id, posX, posY)
        {
            speed = 3.0f;
            MiState = new NullMiState();
            mvState = new NullMvState();
        }
        public void setSpeed(float newSpeed)
        {
            speed = newSpeed;
        }
        public float getSpeed()
        {
            return speed;
        }
        public override void Accept(ColVistor other)
        {
            other.VisitShip(this);
        }

        public override void VisitBombRoot(BombRoot br)
        {
            Reactions.Reaction(br, this);
        }

        public override void VisitBomb(Bomb b)
        {
            Reactions.Reaction(b, this);
        }

        public override void VisitBumper(Bumper b)
        {
            Reactions.Reaction(b, this);
        }


        internal void MoveLeft()
        {
            this.mvState.moveLeft(this);
            //this.incrementX(-speed);
        }

        internal void MoveRight()
        {
            this.mvState.moveRight(this);
            //this.incrementX(speed);
        }

        internal void ShootMissile()
        {
            this.MiState.ShootMissile(this);
        }

        public override void cClean()
        {

        }

        public override void Remove()
        {
             base.Remove();
        }

        public void SetMiState(ShipManager.eMiState instate)
        {
            this.MiState = ShipManager.GetMiState(instate);
        }

        public void SetMvState(ShipManager.eMvState instate)
        {
            this.mvState = ShipManager.GetMvState(instate);
        }
    }
}

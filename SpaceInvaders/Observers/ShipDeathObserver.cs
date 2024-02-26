using SpaceInvaders.CollisionSystem;
using SpaceInvaders.Commands;
using SpaceInvaders.GameObjects;
using SpaceInvaders.GameState;
using SpaceInvaders.GraphicalObjects;
using SpaceInvaders.Time;

namespace SpaceInvaders.Observers
{
    class ShipDeathObserver : ColObserver
    {
        Ship toRemove;
        IrrKlang.ISoundSource expSFX;
        AnimationSprite aniCMD;

        public ShipDeathObserver()
        {
            expSFX = SndEngine.getSoundSource("explosion.wav");
            aniCMD = new AnimationSprite();
            aniCMD.Attach(GameSpriteManager.Find(SpriteID.Ship));

            aniCMD.Attach(ImageManager.Find(ImageID.HeroExp1));
            aniCMD.Attach(ImageManager.Find(ImageID.HeroExp2));

        }

        public ShipDeathObserver(Ship pShip)
        {
            toRemove = pShip;
        }

        public override void dClean()
        {
            expSFX = null;
        }

        public override void Notify()
        {
            Ship ship = (Ship)pSubject.getB();
            
            GameStateManager.getActiveBombRoot().ClearChildren();
            SndEngine.Play2D(expSFX);

            TimerManager.Clear(TimeEventID.bombDrop);

            TimerManager.Clear(TimeEventID.create);

            TimerManager.Clear(TimeEventID.GridMove);

            TimerManager.Clear(TimeEventID.GridMoveSFX);

            TimerManager.Clear(TimeEventID.Anim);

            //TODO: consider removing this resume command, it's in to make the transition quicker without P2 but it might
            //cause bugs.
            GameStateManager.ResumeCommands();

            if(!ship.getMarked())
            {
                ship.markForDeath();
                ShipManager.DeactivateShip();
                ShipDeathObserver pOb = new ShipDeathObserver(ship);
                DelayedObjectManager.Attach(pOb);
            }


        }

        public override void Execute()
        {
            toRemove.clearMark();
            ShipManager.DeathAnim(3.0f);
            toRemove.Remove();


        }
    }
}

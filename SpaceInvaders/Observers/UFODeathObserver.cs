using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GameObjects;
using SpaceInvaders.GameState;
using SpaceInvaders.Time;
using SpaceInvaders.Commands;

namespace SpaceInvaders.Observers
{
    internal sealed class UFORemoveObserver : ColObserver
    {
        UFO toRemove;
        public UFORemoveObserver() { }

        public UFORemoveObserver(UFO a) => toRemove = a;

        public override void dClean()
        {

        }

        public override void Notify()
        {
            //todo: reset UFO command.
            //TODO: stop sound
            //Debug.WriteLine("KillAlienObserver: {0} {1}", this.pSubject.getA(), this.pSubject.getB());

            // OK do some magic
            UFO a = (UFO)this.pSubject.GetB();
            //a.stopSound();
            //this.pSubject.getA().Remove();
            if (!a.IsMarked())
            {
                a.MarkForDeath();
                //   Delay
                //TODO: clean up this new
                UFORemoveObserver pObserver = new UFORemoveObserver(a);
                DelayedObjectManager.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            toRemove.Remove();
            toRemove.ClearMark();
            TimerManager.Add(TimeEventID.create, new UFOSpawnCMD((GameObject)GameObjectManager.Find(GameObjectType.UFORoot).GetGameObject()), GameStateManager.getRandomNumber(12, 38));
        }
    }


    class UFODeathSoundObserver : ColObserver
    {

        IrrKlang.ISoundSource deathSound;
        public UFODeathSoundObserver()
        {
            deathSound = SndEngine.GetSoundSource("ufo_lowpitch.wav");
        }

        public override void dClean()
        {

        }

        public override void Notify()
        {
            //todo: reset UFO command.
            //TODO: stop sound
            //Debug.WriteLine("KillAlienObserver: {0} {1}", this.pSubject.getA(), this.pSubject.getB());

        
            //this.pSubject.getA().Remove();
            SndEngine.Play2D(deathSound);
        }

    }
}

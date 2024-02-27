using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GameState;

namespace SpaceInvaders.Observers
{
    class playAlienDeathSoundObserver : ColObserver
    {
        //private IrrKlang.ISoundEngine sndEngine;
        private IrrKlang.ISoundSource alienExpSFX;
        public playAlienDeathSoundObserver(SndEngine e)
        { 
            alienExpSFX = SndEngine.getSoundSource("invaderKilled.wav");
        }

        public override void dClean()
        {
           // throw new NotImplementedException();
        }

        public override void Notify()
        {
            SndEngine.Play2D(alienExpSFX);
        }
    }
}

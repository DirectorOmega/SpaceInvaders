using SpaceInvaders.CollisionSystem;
using SpaceInvaders.GameState;
using SpaceInvaders.GraphicalObjects;

namespace SpaceInvaders.GameObjects
{
    class UFO : Alien
    {
        IrrKlang.ISoundSource life;
        IrrKlang.ISound lifeHandle;

        public UFO(SpriteID id, float posX = 0, float posY = 0) : base(id, posX, posY)
        {
           // startSound();
        }


        public void startSound()
        {
            life = SndEngine.getSoundSource("ufo_highpitch.wav");
            lifeHandle = SndEngine.Play2D(life);
            lifeHandle.Looped = true;
        }

        public void stopSound()
        {
            lifeHandle.Stop();
        }

        public override void Update()
        {
            incrementX(-2.0f);
            base.Update();
           
        }

        public override void Accept(ColVistor other)
        {
            other.VisitUFO(this);
        }

        public override void VisitMissile(Missile m)
        {
            Reactions.Reaction(m, this);
        }

        public override void VisitSidewall(Sidewall s)
        {
            Reactions.Reaction(s, this);
        }

        public override void Remove()
        {
            stopSound();
            base.Remove();
        }

        public override void cClean()
        {
        }

        public override int getScore()
        {
            return 100;
        }

    }
}

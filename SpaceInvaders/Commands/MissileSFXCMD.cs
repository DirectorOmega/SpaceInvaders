using SpaceInvaders.GameState;

namespace SpaceInvaders.Commands
{
    class MissileSFXCMD : Command
    {
        IrrKlang.ISoundSource shotSFX;

        public MissileSFXCMD()
        {
            
            shotSFX = SndEngine.getSoundSource("Missile.wav");
        }

        public override void execute(float deltaTime=0f)
        {
            SndEngine.Play2D(shotSFX);
        }
    }
}

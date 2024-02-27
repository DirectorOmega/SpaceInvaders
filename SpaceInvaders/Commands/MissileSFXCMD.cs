using SpaceInvaders.GameState;

namespace SpaceInvaders.Commands
{
    internal sealed class MissileSFXCMD : Command
    {
        IrrKlang.ISoundSource shotSFX;

        public MissileSFXCMD() 
            => shotSFX = SndEngine.GetSoundSource("Missile.wav");

        public override void execute(float deltaTime = 0f) 
            => SndEngine.Play2D(shotSFX);
    }
}

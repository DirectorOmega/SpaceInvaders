using SpaceInvaders.GameState;
using SpaceInvaders.Time;
using System.Diagnostics;

namespace SpaceInvaders.Commands
{
    internal sealed class MvSoundCMD : Command
    {
        int count;
        private readonly IrrKlang.ISoundSource sndV1; 
        private readonly IrrKlang.ISoundSource sndV2; 
        private readonly IrrKlang.ISoundSource sndV3; 
        private readonly IrrKlang.ISoundSource sndV4;
       // private Grid pGrid;
        public MvSoundCMD()
        {
         count = 0;
        // this.pGrid = _pGrid;

         sndV1 =  SndEngine.GetSoundSource("fastinvader1.wav");
         sndV2 =  SndEngine.GetSoundSource("fastinvader2.wav");
         sndV3 =  SndEngine.GetSoundSource("fastinvader3.wav");
         sndV4 =  SndEngine.GetSoundSource("fastinvader4.wav");
        }

        //todo get rid of the nasty switch statement.
        public override void execute(float deltaTime)
        {
            switch (count)
            {
                case 0:
                    SndEngine.Play2D(sndV1);
                    break;
                case 1:
                    SndEngine.Play2D(sndV2);
                    break;
                case 2:
                    SndEngine.Play2D(sndV3);
                    break;
                case 3:
                    SndEngine.Play2D(sndV4);
                    break;                   
                default:
                    Debug.Assert(false);
                    break;
            }
            
            count++;
            if (count == 4)
                count = 0;

            if (!GameStateManager.GridEmpty())
                TimerManager.Add(TimeEventID.Anim, this, GameStateManager.getTimeDelta());
        }
    }
}

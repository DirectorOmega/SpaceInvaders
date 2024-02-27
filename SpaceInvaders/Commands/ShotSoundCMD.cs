//using System;

//namespace SpaceInvaders.Commands
//{
//    class ShotSoundCMD : Command
//    {
//        private IrrKlang.ISoundEngine sndEngine;
//        private IrrKlang.ISoundSource shotSound;

//        public ShotSoundCMD(IrrKlang.ISoundEngine sndEngine)
//        {
//            this.sndEngine = sndEngine;

//            shotSound = sndEngine.GetSoundSource("missile", true);
//        }

//        public override void execute(float deltaTime)
//        {
//            sndEngine.Play2D(shotSound, false, false, false);
//        }
//    }
//}

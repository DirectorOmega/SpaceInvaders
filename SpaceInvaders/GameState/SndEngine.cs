namespace SpaceInvaders.GameState
{
    internal sealed class SndEngine
    {
        private static SndEngine pInstance;
        private IrrKlang.ISoundEngine sndEngine;
        
        private SndEngine()
        {
            sndEngine = new IrrKlang.ISoundEngine();
            sndEngine.SoundVolume = 0.01f;
        }

        public static SndEngine getInstance()
        {
            if(pInstance == null)
                pInstance = new SndEngine();
            return pInstance;
        }

        public static IrrKlang.ISoundEngine getIKEngine()
        {
            SndEngine pE = SndEngine.getInstance();
            return pE.sndEngine;
        }

        public static IrrKlang.ISoundSource GetSoundSource(string filepath) => SndEngine.getIKEngine().GetSoundSource(filepath, true);
        public static IrrKlang.ISound Play2D(IrrKlang.ISoundSource source) => getIKEngine().Play2D(source, false, false, false);
        public static void Update() => getIKEngine().Update();
    }
}

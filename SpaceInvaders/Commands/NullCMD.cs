namespace SpaceInvaders.Commands
{
    class NullCMD : Command
    {
        public static NullCMD pInstance;

        NullCMD()
        {
        }

        public static NullCMD getInstance()
        {
            if(pInstance == null)
            {
                pInstance = new NullCMD();
            }

            return pInstance;
        }

        public override void execute(float deltaTime)
        {
            System.Diagnostics.Debug.WriteLine("NullCMD Executed");
        }
    }
}

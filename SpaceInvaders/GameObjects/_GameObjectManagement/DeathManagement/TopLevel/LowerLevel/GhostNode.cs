namespace SpaceInvaders.GameObjects
{
    class GhostNode : GhLink
    {
        private GameObject pGameObj;

        public GhostNode()
        {

        }

        ~GhostNode()
        {
            pGameObj = null;
        }

        public System.Enum getName()
        {
            return pGameObj.getName();
        }

        public GameObject getGameObject()
        {
            return pGameObj;
        }

        public override void dClean()
        {
            pGameObj = null;
        }

        internal void Set(GameObject gO)
        {
            pGameObj = gO;
        }

        internal void Update()
        {
            pGameObj.Update();
        }

    }
}

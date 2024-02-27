namespace SpaceInvaders.GameObjects
{
    internal sealed class GhostNode : GhLink
    {
        private GameObject pGameObj;

        public GhostNode() { }

        ~GhostNode()
        {
            pGameObj = null;
        }

        public Enum GetName() => pGameObj.getName();
        public GameObject GetGameObject() => pGameObj;
        public override void dClean() => pGameObj = null;
        internal void Set(GameObject gO) => pGameObj = gO;
        internal void Update() => pGameObj.Update();
    }
}

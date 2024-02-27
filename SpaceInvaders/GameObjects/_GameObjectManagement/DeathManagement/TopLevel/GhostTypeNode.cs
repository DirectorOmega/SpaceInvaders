namespace SpaceInvaders.GameObjects
{
    internal sealed class GhostTypeNode : GTNLink
    {
        private GameObjectType Name;
        GhostTypeNodeManager poNodeMan;

        public GhostTypeNode()
        {
            Name = GameObjectType.Undef;
            poNodeMan = new GhostTypeNodeManager();
        }

        public void Set(GameObjectType name)
        {
            Name = name;
            poNodeMan.Set(name);
        }

        public GameObjectType GetName() => Name;
        public override void dClean() => Name = GameObjectType.Undef;
        internal void Attach(GameObject toAttach) => poNodeMan.Attach(toAttach);
        internal GameObject Detatch() => poNodeMan.Detatch();
    }
}

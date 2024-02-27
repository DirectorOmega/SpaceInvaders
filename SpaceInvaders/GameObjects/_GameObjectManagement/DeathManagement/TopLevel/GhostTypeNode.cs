namespace SpaceInvaders.GameObjects
{
    internal class GhostTypeNode : GTNLink
    {
        private GameObjectTypeEnum Name;
        GhostTypeNodeManager poNodeMan;

        public GhostTypeNode()
        {
            Name = GameObjectTypeEnum.Undef;
            poNodeMan = new GhostTypeNodeManager();
        }

        public void Set(GameObjectTypeEnum name)
        {
            Name = name;
            poNodeMan.Set(name);
        }

        public GameObjectTypeEnum getName()
        {
            return Name;
        }

        public override void dClean()
        {
            Name = GameObjectTypeEnum.Undef;
        }

        internal void Attach(GameObject toAttach)
        {
            poNodeMan.Attach(toAttach);
        }

        internal GameObject detatch()
        {
            return poNodeMan.Detatch();
        }
    }

}

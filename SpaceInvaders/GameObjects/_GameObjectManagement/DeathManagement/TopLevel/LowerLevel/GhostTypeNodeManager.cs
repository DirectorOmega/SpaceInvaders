using SpaceInvaders.Manager;
using System.Diagnostics;

namespace SpaceInvaders.GameObjects
{
    internal sealed class GhostTypeNodeManager : GTNMan
    {
        private GameObjectType Name;
        private GhostNode poSpriteBatchRef = new GhostNode();

        public GhostTypeNodeManager() => Name = GameObjectType.Undef;
        public GhostTypeNodeManager(GameObjectType name) => Name = name;

        public void Attach(GameObject toAttach)
        {
            GhostNode pNode = (GhostNode)this.baseAdd();
            pNode.Set(toAttach);
        }

        public GameObject Detatch()
        {
            GhostNode toReturn = (GhostNode)getActiveHead();
            if (toReturn != null)
            {
                baseRemove(toReturn);
                return toReturn.GetGameObject();
            }

            return null;
        }

        //bad smell, doesn't do anything currently.
        protected override void dClearNode(DLink pLink)
        {
        }

        protected override bool dCompareNodes(DLink pLinkA, DLink pLinkB)
        {
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            GhostNode left = (GhostNode)pLinkA;
            GhostNode right = (GhostNode)pLinkB;

           return left.GetName() == right.GetName();
        }

        internal void Set(GameObjectType id) => Name = id;

        protected override DLink dCreateNode()
        {
            DLink newNode = new GhostNode();
            Debug.Assert(newNode != null);
            return newNode;
        }
    }
}

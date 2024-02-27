using System.Diagnostics;
using SpaceInvaders.Manager;

namespace SpaceInvaders.GameObjects
{
    internal sealed class GhostManager : GhMan
    {
        private static GhostManager pInstance;
        private static GhostTypeNode poRefNode;

        private GhostManager(int numStart = 5, int deltaAdd = 3)
            : base(numStart, deltaAdd)
        {
            poRefNode = new GhostTypeNode();
        }

        private void addDefaults()
        {
            //others will be added automatically
            Add(GameObjectType.Crab);
            Add(GameObjectType.Hero);
            Add(GameObjectType.Grid);
            Add(GameObjectType.Octo);
            Add(GameObjectType.Squid);
            Add(GameObjectType.Undef);
        }

        public static GhostTypeNode Add(GameObjectType id)
        {
            GhostManager psbMan = GhostManager.getInstance();
            Debug.Assert(psbMan != null);

            GhostTypeNode pNode = (GhostTypeNode)psbMan.baseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(id);

            return pNode;
        }


        public static void Create(int numStart = 5, int deltaAdd = 3)
        {
            Debug.Assert(numStart > 0);
            Debug.Assert(deltaAdd > 0);
            Debug.Assert(pInstance == null);

            if (pInstance == null)
            {
                pInstance = new GhostManager(numStart, deltaAdd);
                pInstance.addDefaults();
            }
        }

        protected override void dClearNode(DLink pLink)
        {
            GhostTypeNode p = (GhostTypeNode)pLink;
            p.dClean();
        }

        protected override bool dCompareNodes(DLink pLinkA, DLink pLinkB)
        {
            //Debug.Assert(false);
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            GhostTypeNode left = (GhostTypeNode)pLinkA;
            GhostTypeNode right = (GhostTypeNode)pLinkB;

            //currently proxy objects can only be identical by reference.
            return left.GetName() == right.GetName();
        }

        protected override DLink dCreateNode()
        {
            DLink newNode = new GhostTypeNode();
            Debug.Assert(newNode != null);

            return newNode;
        }

        public static GameObject Rez(GameObjectType typeID)
        {
            GhostManager sMan = GhostManager.getInstance();

            GhostTypeNode targetref = GhostManager.toFind(typeID);
            GhostTypeNode target = (GhostTypeNode) sMan.baseFind(targetref);

            return target.Detatch();
        }

        //TOOD: remove the find universally so I can get rid of names or at the very least from everything above game sprite.
        public static GhostTypeNode Find(GameObjectType typeID)
        {
            GhostManager sMan = GhostManager.getInstance();

            GhostTypeNode target = GhostManager.toFind(typeID);

            GhostTypeNode found = (GhostTypeNode)sMan.baseFind(target);

            if(null == found)
                found = Add(typeID);

            return found;
        }

        //for find
        private static GhostTypeNode toFind(GameObjectType typeID)
        {
            poRefNode.Set(typeID);
            return poRefNode;
        }

        public static GhostManager getInstance()
        {
            if (pInstance == null)
                GhostManager.Create();
            return pInstance;
        }

        internal static void Attach(GameObject pObject)
        {
            GhostManager pTMan = GhostManager.getInstance();
            Debug.Assert(pTMan != null);

            GhostTypeNode pNode = Find((GameObjectType)pObject.getName());
            Debug.Assert(pNode != null);

            pNode.Attach(pObject);
        }
    }
}

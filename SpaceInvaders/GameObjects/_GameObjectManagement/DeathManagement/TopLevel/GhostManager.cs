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

        private static void AddDefaults()
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
            GhostManager psbMan = GhostManager.GetInstance();
            Debug.Assert(psbMan != null);

            GhostTypeNode pNode = (GhostTypeNode)psbMan.BaseAdd();
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
                AddDefaults();
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
            GhostManager sMan = GhostManager.GetInstance();

            GhostTypeNode targetref = GhostManager.ToFind(typeID);
            GhostTypeNode target = (GhostTypeNode) sMan.BaseFind(targetref);

            return target.Detatch();
        }

        //TOOD: remove the find universally so I can get rid of names or at the very least from everything above game sprite.
        public static GhostTypeNode Find(GameObjectType typeID)
        {
            GhostManager sMan = GhostManager.GetInstance();

            GhostTypeNode target = GhostManager.ToFind(typeID);

            GhostTypeNode found = (GhostTypeNode)sMan.BaseFind(target);

            if(null == found)
                found = Add(typeID);

            return found;
        }

        //for find
        private static GhostTypeNode ToFind(GameObjectType typeID)
        {
            poRefNode.Set(typeID);
            return poRefNode;
        }

        public static GhostManager GetInstance()
        {
            if (pInstance == null)
                Create();
            return pInstance;
        }

        internal static void Attach(GameObject pObject)
        {
            GhostManager pTMan = GhostManager.GetInstance();
            Debug.Assert(pTMan != null);

            GhostTypeNode pNode = Find((GameObjectType)pObject.getName());
            Debug.Assert(pNode != null);

            pNode.Attach(pObject);
        }
    }
}

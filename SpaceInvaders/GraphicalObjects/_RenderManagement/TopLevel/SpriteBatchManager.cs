using System.Diagnostics;
using SpaceInvaders.Manager;

namespace SpaceInvaders.GraphicalObjects
{
    class SpriteBatchManager : SBMan
    {
        private static SpriteBatchManager pInstance;
        private static SpriteBatch poSpriteBatchRef = new SpriteBatch();

        private SpriteBatchManager(int numStart = 5, int deltaAdd = 3)
                : base(numStart, deltaAdd)
        {
        }

        internal static void Toggle(SpriteBatchID name)
        {
            SpriteBatchManager.Find(name).Toggle();
        }

        public static SpriteBatch Add(SpriteBatchID id)
        {
            SpriteBatchManager psbMan = SpriteBatchManager.getInstance();
            Debug.Assert(psbMan != null);

            SpriteBatch pNode = (SpriteBatch)psbMan.baseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(id);

            return pNode;
        }

        public static void StorePlayerA()
        {
            SpriteBatchManager SBM = SpriteBatchManager.getInstance();
            Debug.Assert(SBM != null);
            //  pSpriteBatchRef = pActiveHead;
            SpriteBatch cur = (SpriteBatch)SBM.getActiveHead();
            while (cur != null)
            {
                cur.StorePlayerA();
                cur = (SpriteBatch)cur.pNext;
            }
        }


        public static void SetPlayerA()
        {
            SpriteBatchManager SBM = SpriteBatchManager.getInstance();
            Debug.Assert(SBM != null);
            //  pSpriteBatchRef = pActiveHead;
            SpriteBatch cur = (SpriteBatch)SBM.getActiveHead();
            while (cur != null)
            {
                cur.SetPlayerA();
                cur = (SpriteBatch)cur.pNext;
            }
        }

        public static void StorePlayerB()
        {
            SpriteBatchManager SBM = SpriteBatchManager.getInstance();
            Debug.Assert(SBM != null);
            //  pSpriteBatchRef = pActiveHead;
            SpriteBatch cur = (SpriteBatch)SBM.getActiveHead();
            while (cur != null)
            {
                cur.StorePlayerB();
                cur = (SpriteBatch)cur.pNext;
            }
        }

        public static void SetPlayerB()
        {
            SpriteBatchManager SBM = SpriteBatchManager.getInstance();
            Debug.Assert(SBM != null);
            //  pSpriteBatchRef = pActiveHead;
            SpriteBatch cur = (SpriteBatch)SBM.getActiveHead();
            while (cur != null)
            {
                cur.SetPlayerB();
                cur = (SpriteBatch)cur.pNext;
            }
        }

        public static void ClearStored()
        {
            SpriteBatchManager SBM = SpriteBatchManager.getInstance();
            Debug.Assert(SBM != null);
            //  pSpriteBatchRef = pActiveHead;
            SpriteBatch cur = (SpriteBatch)SBM.getActiveHead();
            while (cur != null)
            {
                cur.ClearStored();
                cur = (SpriteBatch)cur.pNext;
            }
        }

        public static void Create(int numStart = 5, int deltaAdd = 3)
        {
            Debug.Assert(numStart > 0);
            Debug.Assert(deltaAdd > 0);
            Debug.Assert(pInstance == null);

            if (pInstance == null)
            {
                pInstance = new SpriteBatchManager(numStart, deltaAdd);
            }
        }


        protected override void dClearNode(DLink pLink)
        {
            SpriteBatch p = (SpriteBatch)pLink;
            p.dClean();
        }

        protected override bool dCompareNodes(DLink pLinkA, DLink pLinkB)
        {
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            SpriteBatch left = (SpriteBatch)pLinkA;
            SpriteBatch right = (SpriteBatch)pLinkB;

            if (left.getName() == right.getName())
            {
                return true;
            }
            return false;
        }

        protected override DLink dCreateNode()
        {
            DLink newNode = new SpriteBatch();
            Debug.Assert(newNode != null);

            return newNode;
        }

        public static SpriteBatch Find(SpriteBatchID batchName)
        {
            SpriteBatchManager bsMan = SpriteBatchManager.getInstance();
            Debug.Assert(bsMan != null);


            SpriteBatch target = SpriteBatchManager.toFind(batchName);
            return (SpriteBatch)bsMan.baseFind(target);

        }

        //for find
        private static SpriteBatch toFind(SpriteBatchID name)
        {
            poSpriteBatchRef.Set(name);
            return poSpriteBatchRef;
            // return new SpriteBatch(name);
        }

        public static SpriteBatchManager getInstance()
        {
            if (pInstance == null)
            {

                SpriteBatchManager.Create();
            }
            return pInstance;
        }

        public static void Draw()
        {
            SpriteBatchManager SBM = SpriteBatchManager.getInstance();
            Debug.Assert(SBM != null);
            //  pSpriteBatchRef = pActiveHead;
            SpriteBatch cur = (SpriteBatch)SBM.getActiveHead();
            while (cur != null)
            {
                cur.Draw();
                cur = (SpriteBatch)cur.pNext;
            }
        }
    }
}

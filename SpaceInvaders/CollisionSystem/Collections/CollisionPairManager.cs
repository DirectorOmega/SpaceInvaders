using SpaceInvaders.Manager;
using System.Diagnostics;
using SpaceInvaders.GameObjects;

namespace SpaceInvaders.CollisionSystem
{
    internal sealed class CollisionPairManager : CPMan
    {
        private static CollisionPairManager pInstance;
        private static CollisionPair poRef;
        private CollisionPair pActiveColPair;

        public static void ClearStored()
        {
            CollisionPairManager pMan = getInstance();
            CollisionPair cur = (CollisionPair)pMan.BaseGetPAHead();

            //this check because single player doesn't store it's state.
            if (cur != null)
            {
                if (cur.GetName() == CollisionPairName.reserveList)
                {
                    pMan.baseSetPlayerB();
                    Clear();
                }
                else
                {
                    pMan.baseSetPlayerA();
                    Clear();
                }
            }
            pMan.nullHeads();
        }

        public static void StorePlayerA() => getInstance().baseStorePlayerA();
        public static void SetPlayerA() => getInstance().baseSetPlayerA();

        public static void StorePlayerB() => getInstance().baseStorePlayerB();
        public static void SetPlayerB() => getInstance().baseSetPlayerB();

        private CollisionPairManager(int numStart = 5, int deltaAdd = 3)
            : base(numStart, deltaAdd)
        {
            poRef = new CollisionPair();
        }

        public static CollisionPairManager getInstance()
        {
            if (pInstance == null)
                Create();
            return pInstance;
        }

        public static CollisionPair getActiveColPair()
        {
            CollisionPairManager pMan = getInstance();
            return pMan.pActiveColPair;
        }

        public static void Remove(CollisionPair pNode)
        {
            Debug.Assert(pNode != null);
            CollisionPairManager pMan = getInstance();
            pMan.baseRemove(pNode);
        }

        public static void Process()
        {
            // get the singleton
            CollisionPairManager pColPairMan = getInstance();

            CollisionPair pColPair = (CollisionPair)pColPairMan.GetActiveHead();

            while (pColPair != null)
            {
                //this is what you used and I'm not sure I like it, If active col pair gets called
                //out of turn it might cause problems,
                //I'm gonna try to rework to be more passive, where the info that's needed gets pushed down.
                pColPairMan.pActiveColPair = pColPair;
                // do the check for a single pair
                pColPair.Process();

                // advance to next
                pColPair = (CollisionPair)pColPair.pNext;
            }
        }

        internal static void Clear()
        {
            CollisionPairManager t = getInstance();
            CollisionPair e = (CollisionPair) t.GetActiveHead();
            while (e != null)
            {
                if (e.pNext != null)
                {
                    e = (CollisionPair) e.pNext;
                    t.baseRemove(e.pPrev);
                }
                else
                {
                    t.baseRemove(e);
                    e = null;
                }
            }
        }

        public static void Create(int numStart = 5, int deltaAdd = 3)
        {
            Debug.Assert(numStart > 0);
            Debug.Assert(deltaAdd > 0);
            Debug.Assert(pInstance == null);

            if (pInstance == null)
                pInstance = new CollisionPairManager(numStart, deltaAdd);
        }

        public static CollisionPair Add(CollisionPairName Name, GameObject treeRootA, GameObject treeRootB)
        {
            CollisionPairManager pTMan = getInstance();
            Debug.Assert(pTMan != null);

            CollisionPair pNode = (CollisionPair)pTMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.dClean();
            pNode.Set(Name,treeRootA,treeRootB);

            return pNode;
        }

        protected override DLink dCreateNode()
        {
            DLink newNode = new CollisionPair();
            Debug.Assert(newNode != null);
            return newNode;
        }

        public static CollisionPair Find(CollisionPairName name)
        {
            CollisionPairManager mrT = getInstance();
            Debug.Assert(mrT != null);
            CollisionPair target = toFind(name);
            return (CollisionPair)mrT.BaseFind(target);
        }

        private static CollisionPair toFind(CollisionPairName name)
        {
            poRef.SetName(name);
            return poRef;
        }

        protected override bool dCompareNodes(DLink pLinkA, DLink pLinkB)
        {
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            CollisionPair left = (CollisionPair)pLinkA;
            CollisionPair right = (CollisionPair)pLinkB;

            return left.GetName() == right.GetName();
        }

        protected override void dClearNode(DLink pLink)
        {
            CollisionPair p = (CollisionPair)pLink;
            p.dClean();
        }
    }
}

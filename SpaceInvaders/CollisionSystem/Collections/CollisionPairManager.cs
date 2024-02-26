using SpaceInvaders.Manager;
using System.Diagnostics;
using SpaceInvaders.GameObjects;

namespace SpaceInvaders.CollisionSystem
{
    class CollisionPairManager : CPMan
    {
        private static CollisionPairManager pInstance;
        private static CollisionPair poRef;
        private CollisionPair pActiveColPair;

        public static void ClearStored()
        {
            CollisionPairManager pMan = CollisionPairManager.getInstance();
            CollisionPair cur;
            cur = (CollisionPair)pMan.baseGetPAHead();
            //this check because singleplayer doesn't store it's state.
            if (cur != null)
            {
                if (cur.getName() == CollisionPairName.reserveList)
                {
                    pMan.baseSetPlayerB();

                    CollisionPairManager.Clear();
                }
                else
                {
                    pMan.baseSetPlayerA();
                    CollisionPairManager.Clear();
                }
            }
            pMan.nullHeads();
        }

        public static void StorePlayerA()
        {
            CollisionPairManager.getInstance().baseStorePlayerA();
        }


        public static void SetPlayerA()
        {
            CollisionPairManager.getInstance().baseSetPlayerA();
        }

        public static void StorePlayerB()
        {
            CollisionPairManager.getInstance().baseStorePlayerB();
        }

        public static void SetPlayerB()
        {
            CollisionPairManager.getInstance().baseSetPlayerB();
        }

        private CollisionPairManager(int numStart = 5, int deltaAdd = 3)
            : base(numStart, deltaAdd)
        {
            poRef = new CollisionPair();
        }


        public static CollisionPairManager getInstance()
        {
            if (pInstance == null)
            {
                CollisionPairManager.Create();
            }
            return pInstance;
        }

        public static CollisionPair getActiveColPair()
        {
            CollisionPairManager pMan = CollisionPairManager.getInstance();
            return pMan.pActiveColPair;
        }

        public static void Remove(CollisionPair pNode)
        {
            Debug.Assert(pNode != null);
            CollisionPairManager pMan = CollisionPairManager.getInstance();
            pMan.baseRemove(pNode);
        }

        public static void Process()
        {
            // get the singleton
            CollisionPairManager pColPairMan = CollisionPairManager.getInstance();

            CollisionPair pColPair = (CollisionPair)pColPairMan.getActiveHead();

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
            CollisionPairManager t = CollisionPairManager.getInstance();
            CollisionPair e = (CollisionPair)t.getActiveHead();
            CollisionPair ePrev;
            while (e != null)
            {
                if (e.pNext != null)
                {
                    e = (CollisionPair)e.pNext;
                    ePrev = (CollisionPair)e.pPrev;

                    t.baseRemove(e.pPrev);
                }
                else
                {

                    t.baseRemove(e);
                    e = null;

                    // e = (TimeEvent)e.pNext;
                }
            }
        }

        public static void Create(int numStart = 5, int deltaAdd = 3)
        {
            Debug.Assert(numStart > 0);
            Debug.Assert(deltaAdd > 0);
            Debug.Assert(pInstance == null);

            if (pInstance == null)
            {
                pInstance = new CollisionPairManager(numStart, deltaAdd);
            }
        }

#if DEBUG
        public static CollisionPair Add(CollisionPairName Name, GameObject treeRootA, GameObject treeRootB)
        {
            CollisionPairManager pTMan = CollisionPairManager.getInstance();
            Debug.Assert(pTMan != null);

            CollisionPair pNode = (CollisionPair)pTMan.baseAdd();
            Debug.Assert(pNode != null);

            pNode.dClean();
            pNode.Set(Name,treeRootA,treeRootB);

            return pNode;
        }


#else
        public static CollisionPair Add(CollisionPairName Name, GameObject treeRootA, GameObject treeRootB)
        {
            CollisionPair pNode = (CollisionPair) CollisionPairManager.getInstance().baseAdd();
            pNode.Set(Name, treerootA,TreeRootB);
            return pNode;
        }

#endif


#if DEBUG
        protected override DLink dCreateNode()
        {
            DLink newNode = new CollisionPair();
            Debug.Assert(newNode != null);
            return newNode;
        }
#else
        protected override DLink dCreateNode()
        {
            return (DLink) new CollisionPair();
        }
#endif

#if DEBUG
        public static CollisionPair Find(CollisionPairName name)
        {
            CollisionPairManager mrT = CollisionPairManager.getInstance();
            Debug.Assert(mrT != null);
            CollisionPair target = CollisionPairManager.toFind(name);
            return (CollisionPair)mrT.baseFind(target);
        }
#else
        public static CollisionPair Find(CollisionPairName name)
        {
          return (CollisionPair) CollisionPairManager.getInstance().baseFind(CollisionPairManager.toFind(name));
        }
#endif

        private static CollisionPair toFind(CollisionPairName name)
        {
            poRef.setName(name);
            return poRef;
        }

        protected override bool dCompareNodes(DLink pLinkA, DLink pLinkB)
        {
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            CollisionPair left = (CollisionPair)pLinkA;
            CollisionPair right = (CollisionPair)pLinkB;

            if (left.getName() == right.getName())
            {
                return true;
            }
            return false;
        }

        protected override void dClearNode(DLink pLink)
        {
            CollisionPair p = (CollisionPair)pLink;
            p.dClean();
        }
    }
}

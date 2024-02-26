using System.Diagnostics;
using SpaceInvaders.Manager;
using SpaceInvaders.PCS;
using SpaceInvaders.GraphicalObjects;
using System;

namespace SpaceInvaders.GameObjects
{
    class GameObjectManager : GoMan
    {
        private static GameObjectManager pInstance;
        private PCSTree pRoot;
        private static GameObjectNode poRefNode;

        //private PCSIterator pTreeIterator;
        //private static GameObjectNode PlayerAHead;
        //private static GameObjectNode PlayerBHead;

       private void replaceSprites()
        {   
            //todo: modify this to grab the sbmn for each node and then iterate down and place every sprite back on the managers.
            GameObjectManager pMan = GameObjectManager.getInstance();

            GameObjectNode pRoot = (GameObjectNode)pMan.getActiveHead();
            GameObjectNode pTmp;
            while (pRoot != null)
            {
                pTmp = pRoot;
                pRoot = (GameObjectNode)pRoot.pNext;
                pTmp.Update();
            }

        }

       // probably the worst thing. I had this bug pop up right before the deadline
       //so when the game is over one of the two players is not clean and the other points to the reserve list. which is bad.
        public static void ClearStored()
        {
            GameObjectManager pMan = GameObjectManager.getInstance();

            GameObjectNode cur = (GameObjectNode)pMan.getActiveHead();

                //this means things aren't getting cleaned up properly.
                if(cur != null) { Debug.Assert(false); }


            cur = (GameObjectNode) pMan.baseGetPAHead();
            //this check because singleplayer will clean itself properly, and not store it's state.
            if (cur != null)
            {
                if (cur.getGameObject() == null)
                {
                    pMan.baseSetPlayerB();
                    SpriteBatchManager.SetPlayerB();
                    GameObjectManager.Clear();
                }
                else
                {
                    pMan.baseSetPlayerA();
                    SpriteBatchManager.SetPlayerA();
                    GameObjectManager.Clear();
                }
            }
            pMan.nullHeads();
        }
        public static void StorePlayerA()
        {
            GameObjectManager.getInstance().baseStorePlayerA();
        }

        public static void SetPlayerA()
        {
            GameObjectManager.getInstance().baseSetPlayerA();
        }

        public static void StorePlayerB()
        {
            GameObjectManager.getInstance().baseStorePlayerB();
        }

        public static void SetPlayerB()
        {
            GameObjectManager.getInstance().baseSetPlayerB();
        }

        private GameObjectManager(int numStart = 5, int deltaAdd = 3)
            : base(numStart, deltaAdd)
        {
            pRoot = new PCSTree();
            poRefNode = new GameObjectNode();
            poRefNode.Set(new Squid(SpriteID.NullSprite,0,0),null);
        }

        public static void Create(int numStart = 5, int deltaAdd = 3)
        {
            Debug.Assert(numStart > 0);
            Debug.Assert(deltaAdd > 0);
            Debug.Assert(pInstance == null);

            if (pInstance == null)
            {
                pInstance = new GameObjectManager(numStart, deltaAdd);
            }
        }

        protected override void dClearNode(DLink pLink)
        {
            GameObjectNode p = (GameObjectNode)pLink;
            p.dClean();
        }

        protected override bool dCompareNodes(DLink pLinkA, DLink pLinkB)
        {
            //Debug.Assert(false);
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            GameObjectNode left = (GameObjectNode)pLinkA;
            GameObjectNode right = (GameObjectNode)pLinkB;

            //currently GameObject Nodes can only be identical by reference.
            if (left.getName() == right.getName())
            {
                return true;
            }
            return false;
        }

        protected override DLink dCreateNode()
        {
            DLink newNode = new GameObjectNode();
            Debug.Assert(newNode != null);

            return newNode;
        }

         public static GameObjectNode Find(GameObjectTypeEnum sprite)
        {
            GameObjectManager sMan = GameObjectManager.getInstance();

            GameObjectNode target = GameObjectManager.toFind(sprite);
            return (GameObjectNode)sMan.baseFind(target);

        }

        //for find
        private static GameObjectNode toFind(GameObjectTypeEnum name)
        {
            poRefNode.setName(name);
            return poRefNode;
            // return new GameSprite(name);
        }

        public static GameObjectManager getInstance()
        {
            if (pInstance == null)
            {
                GameObjectManager.Create();
            }
            return pInstance;
        }

        internal static void AttachTree(GameObject pAlien, PCSTree pTree = null)
        {
            GameObjectManager pTMan = GameObjectManager.getInstance();
            Debug.Assert(pTMan != null);

            GameObjectNode pNode = (GameObjectNode)pTMan.baseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(pAlien,pTree);
        }
        //TODO: remove the pRoot from this class, I want to have this pass a gameObject node for parent
        //so I can get the tree. Unless this breaks when I add columns.
        public static void Insert(GameObject pGameObj, GameObject pParent = null)
        {
            GameObjectManager pMan = GameObjectManager.getInstance();
            Debug.Assert(pGameObj != null);

            if (pParent == null)
            {
                GameObjectManager.AttachTree(pGameObj);
            }
            else
            {
                // Can be null
                Debug.Assert(pParent != null);

                pMan.pRoot.SetRoot(pParent);
                pMan.pRoot.Insert(pGameObj, pParent);
            }
        }


        internal static void Remove(GameObjectNode go)
        {
            Debug.Assert(go != null);
            GameObjectManager pTMan = GameObjectManager.getInstance();
            Debug.Assert(pTMan != null);

            pTMan.baseRemove(go);
            go.dClean();
           // DLink k = (DLink)go;
            //pTMan.removeFromActive(ref k);
        }

        //I removed ur hack hack from here but it's integrated into the other two methods still.
        //It's on my todo: list
        public static void Remove(GameObject pNode)
        {
            Debug.Assert(pNode != null);
            GameObjectManager pMan = GameObjectManager.getInstance();

            GameObject pSafetyNode = pNode;

            // OK so we have a linked list of trees (Remember that)

            // 1) find the tree root (we already know its the most parent)

            GameObject pTmp = pNode;
            GameObject pRoot = null;
            while (pTmp != null)
            {
                pRoot = pTmp;
                pTmp = (GameObject)pTmp.getParent();
            }

            // 2) pRoot is the tree we are looking for
            // now walk the active list looking for pRoot

            GameObjectNode pTree = (GameObjectNode)pMan.getActiveHead();

            while (pTree != null)
            {
                if (pTree.getGameObject() == pRoot)
                {
                    // found it
                    break;
                }
                // Goto Next tree
                pTree = (GameObjectNode)pTree.pNext;
            }

            // 3) pTree is the tree that holds pNode
            //  Now remove it

            Debug.Assert(pTree != null);
            Debug.Assert(pTree.getGameObject() != null);

            if(pTree.getGameObject() == pNode)
            {
                if (pTree.getTree() != null)
                {
                    pTree.getTree().Remove(pNode);
                }
                GameObjectManager.Remove(pTree);
            } else 
            if (pTree.getTree() != null)
            {
                pTree.getTree().Remove(pNode);
            }
            else { GameObjectManager.Remove(pTree); }

            //add check for empty tree so I can recycle the game object node.
        }

        internal static void Update()
        {
            //TODO: fix the iteartor to do what I need.
            GameObjectManager pMan = GameObjectManager.getInstance();

            GameObjectNode pRoot = (GameObjectNode)pMan.getActiveHead();
            GameObjectNode pTmp;
            while (pRoot != null)
            {
                pTmp = pRoot;
                pRoot = (GameObjectNode)pRoot.pNext;
                pTmp.Update();
            }
                // OK at this point, I have a Root tree,
                // need to walk the tree completely before moving to next tree
                //I really need to remove this new. and allow the iterator root to be switched.
              //  PCSIterator pIterator = new PCSReverseIterator(pRoot.getGameObject());
               // PCSIterator pIterator = new PCSTreeIterator(pRoot.getGameObject());
                // Initialize
               // GameObject pGameObj = (GameObject)pIterator.first();

             //   while (!pIterator.isDone())
               // {
               //     pGameObj.Update();

                    // Advance
               //     pGameObj = (GameObject)pIterator.next();
                //}

                // Goto Next tree
               // pRoot = (GameObjectNode)pRoot.pNext;
           // }

            //GameObjectManager pGMan = GameObjectManager.getInstance();
            //Debug.Assert(pGMan != null);

            //GameObjectNode n = (GameObjectNode)pGMan.getActiveHead();
            //while (n != null)
            //{
            //    n.Update();
            //    n = (GameObjectNode)n.pNext;
            //}
        }

        internal static void Clear()
        {

            GameObjectManager g = GameObjectManager.getInstance();
            GameObjectNode e = (GameObjectNode)g.getActiveHead();
            GameObjectNode ePrev;
            GameObject toRemove;
            while (e != null)
            {
                if (e.pNext != null)
                {
                    e = (GameObjectNode)e.pNext;
                    ePrev = (GameObjectNode)e.pPrev;
                    toRemove = (GameObject)ePrev.getGameObject();

                    //GameObjectManager.Remove(ePrev);
                    toRemove.Remove();
                }
                else
                {
                    toRemove = (GameObject)e.getGameObject();

                    // GameObjectManager.Remove(e);
                    toRemove.Remove();
                    e = null;
                    g.pRoot = new PCSTree();
                    //e = (GameObjectNode)e.pNext;
                }
            }
        }
    }
}

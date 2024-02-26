using System.Diagnostics;

namespace SpaceInvaders.Manager
{
    public abstract class DLink
    {
        public DLink pNext;
        public DLink pPrev;

        public DLink()
        {
            pNext = null;
            pPrev = null;
        }

        public abstract void dClean();
        private void Clear()
        {
            pNext = null;
            pPrev = null;
            dClean();
        }

        public static void addToFront(ref DLink pHead, DLink pNode)
        {
         
            Debug.Assert(pNode != null);

            pNode.pPrev = null;
            if (pHead != null)
            {
                pHead.pPrev = pNode;
            }
       
            pNode.pNext = pHead;
            pHead = pNode;
           
            Debug.Assert(pHead != null);
        }

        //TODO: probably rename to get(), to decouple the operation from the name
        public static DLink PullFromFront(ref DLink pHead)
        {
            // There should always be something on list
            Debug.Assert(pHead != null);

            // return node
            DLink pNode = pHead;

            // Update head (OK if it points to NULL)
            pHead = pHead.pNext;
            if (pHead != null)
            {
                pHead.pPrev = null;
            }

            // remove any lingering links
            pNode.Clear();

            return pNode;
        }

        //Bad smell here I know, I didn't want to force every class to implement this.
        //I want to create virtual comparison operations for all the nodes
        //but I don't want to force the user to implement them if they don't use them.
        virtual public bool greaterThan(DLink that)
        {
            //if this is greater than that return true;
            //woah woah you have to implement this before use.
            Debug.Assert(false);
            
            return false;
        }

        //TODO: optimize more
        internal static DLink baseSortAdd(ref DLink pActiveHead, DLink toAdd)
        {
            //add to front OR add only
            //just hoping for that early out
            if(null == pActiveHead || pActiveHead.greaterThan(toAdd))
            {
                addToFront(ref pActiveHead, toAdd);
            } else
            {
                DLink cur = pActiveHead;
                while (null != cur) 
                {
                    //add end or middle
                    if(cur.pNext == null || cur.pNext.greaterThan(toAdd))
                    {
                        if(null != cur.pNext)
                        {
                            cur.pNext.pPrev = toAdd;
                        }
                        toAdd.pNext = cur.pNext;
                        cur.pNext = toAdd;
                        toAdd.pPrev = cur;
                        break;
                    }
                    cur = cur.pNext;
                }
            }
            return toAdd;
        }

        public static void RemoveNode(ref DLink pHead, DLink pNode)
        {
            // protection
            Debug.Assert(pNode != null);

            // 4 different conditions... 
            if (pNode.pPrev != null)
            {	// middle or last node
                pNode.pPrev.pNext = pNode.pNext;
            }
            else
            {  // first
                pHead = pNode.pNext;
            }

            if (pNode.pNext != null)
            {	// middle node
                pNode.pNext.pPrev = pNode.pPrev;
            }
        }

        virtual public int dCompareNodes(DLink toCompare)
        {
            Debug.Assert(false);
            return -1;
        }
    }
}
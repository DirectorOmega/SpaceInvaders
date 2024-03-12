//This needs some optimization and cleaning, but I've been very busy with moving so I haven't been able to sit down and do a ton of work.
using System.Diagnostics;

namespace SpaceInvaders.Manager
{
    public abstract class baseManager //: iManager
    {
   
        DLink pReserveHead;
        DLink pActiveHead;

        protected DLink P1Head { get; set; }
        protected DLink P2Head { get; set; }

        protected int mDeltaAdd { get; private set; }
        public int mNumActive { get; protected set; }
        public int mNumReserved { get; protected set; }

        public int GetTotal() => mNumActive + mNumReserved;

        protected DLink BaseGetPAHead() => P1Head;

        protected void nullHeads()
        {
            P1Head = null;
            P2Head = null;
        }

        protected void baseStorePlayerA()
        {
            P1Head = this.GetActiveHead();
            this.setActiveHead(null);
        }

        protected bool AHeadEmpty() => null == P1Head;

        protected void baseSetPlayerA()
        {
            this.setActiveHead(P1Head);
        }

        protected void baseStorePlayerB()
        {
            P2Head = this.GetActiveHead();
            this.setActiveHead(null);
        }

        protected void baseSetPlayerB() => this.setActiveHead(P2Head);

        protected DLink GetActiveHead() => pActiveHead;

        protected void setActiveHead(DLink pHead)
        {
            pActiveHead = pHead;
        }
       
        protected baseManager(int numStart=5, int deltaAdd = 3)
        {
            mNumActive = 0;
            mNumReserved = 0;
            mDeltaAdd = deltaAdd;
            pReserveHead = null;
            pActiveHead = null;
            generateReserves(numStart);
        }

        protected DLink BaseAddSort(DLink toAdd)
        {
            //DLink.addToFront(ref pActiveHead, toAdd);
            DLink.baseSortAdd(ref pActiveHead, toAdd);
            return toAdd;
        }

       //private copy constructor
        private baseManager(baseManager x){}

        ~baseManager()
        {
            Debug.Print("A manager has been garbage collected!");
        }
        
        private void generateReserves(int numToAdd)
        {
            Debug.Assert(numToAdd > 0);
            for(int i =0;i<numToAdd;i++)
            {
                generateReserve();
            }
        }

        private void generateReserve()
        {
            mNumReserved++;
            DLink newLink = this.dCreateNode();
            Debug.Assert(newLink != null);

            DLink.addToFront(ref pReserveHead, newLink);
        }

        protected DLink getFromReserve()
        {
            mNumReserved--;
            if (null == pReserveHead)
                generateReserves(3);
            return DLink.PullFromFront(ref pReserveHead);
        }
      
        private DLink addToActive(DLink newLink)
        {
            mNumActive++;
            DLink.addToFront(ref pActiveHead, newLink);
            return  newLink;
        }

        public DLink BaseAdd() => addToActive(getFromReserve());

        public DLink BaseFind(DLink pTarget)
        {
            DLink cur = pActiveHead;
            while (cur != null)
            {
                if (dCompareNodes(cur,pTarget))
                    break;
                cur = cur.pNext;
            }
            return cur;
        }

        //Remove moves a given node from the active list and puts it at the head of reserve list
        //I put bool in now because I wanna rework all of these to do error/status Enums.
        //I also wanna refactor these eventually to use Error Enums and add more error handling or correction. But I don't quite understand what would be useful just yet.
        public bool baseRemove(DLink toRemove)
        {
            Debug.Assert(toRemove != null);

            removeFromActive(ref toRemove);
            toRemove.dClean();
            recycleToReserve(ref toRemove);
            
            return true;
        }

        protected void removeFromActive(ref DLink toRemove)
        {
            DLink.RemoveNode(ref pActiveHead, toRemove);
            mNumActive--;
        }

        protected void recycleToReserve(ref DLink toRemove)
        {
            DLink.addToFront(ref pReserveHead, toRemove);
            mNumReserved++;
        }

        abstract protected DLink dCreateNode();

        abstract protected bool dCompareNodes(DLink pLinkA, DLink pLinkB);

        abstract protected void dClearNode(DLink pLink);

        protected DLink baseInSort(DLink toAdd)
        {
            DLink.baseSortAdd(ref pActiveHead, toAdd);
            return toAdd; 
        }

        // these are just here to allow you to ensure that the length of the list is what is reported by the number.
        //mainly because I wanna automate test cases so I don't want to have to look to make sure the list is properly linked.
        //THESE ARE NEVER FOR CALLING IN RELEASE 

#if DEBUG
        public int debugCountActive()
        {
            int numActive = 0;
            DLink cur = pActiveHead;
            while(null != cur)
            {
                numActive++;
                cur = cur.pNext;
            }

            return numActive;
        }

        public int debugCountReserve()
        {
            int numReserve = 0;
            DLink cur = pReserveHead;
            while (null != cur)
            {
                numReserve++;
                cur = cur.pNext;
            }
            return numReserve;
        }

        public int debugCountAll()
        {
            return debugCountActive() + debugCountReserve();
        }

#endif

    }
}
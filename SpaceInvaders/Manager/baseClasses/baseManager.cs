//This needs some optimization and cleaning, but I've been very busy with moving so I haven't been able to sit down and do a ton of work.
using System.Diagnostics;

namespace SpaceInvaders.Manager
{
    public abstract class baseManager //: iManager
    {
   
        DLink pReserveHead;
        DLink pActiveHead;

        DLink P1Head;
        DLink P2Head;

        int mDeltaAdd;
        int mNumActive;
        int mNumReserved;

        protected DLink baseGetPAHead()
        {
            return P1Head;
        }

        protected DLink baseGetPBHead()
        {
            return P2Head;
        }

        protected void nullHeads()
        {
            P1Head = null;
            P2Head = null;
        }

        protected void baseStorePlayerA()
        {
            P1Head = this.getActiveHead();
            this.setActiveHead(null);
        }

        protected bool AHeadEmpty()
        {
            if(null == P1Head)
            {
                return true;
            }
            return false;
        }

        protected void baseSetPlayerA()
        {
            this.setActiveHead(P1Head);
        }

        protected void baseStorePlayerB()
        {
            P2Head = this.getActiveHead();
            this.setActiveHead(null);
        }

        protected void baseSetPlayerB()
        {
            this.setActiveHead(P2Head);
        }

        protected DLink getActiveHead()
        {
            return pActiveHead;
        }

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

        protected DLink baseAddSort(DLink toAdd)
        {
            //DLink.addToFront(ref pActiveHead, toAdd);
            DLink.baseSortAdd(ref pActiveHead, toAdd);
            return toAdd;
        }

       //private copyconstructor
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
            {
                generateReserves(3);
            }
            return DLink.PullFromFront(ref pReserveHead);
        }
      
        private DLink addToActive(DLink newLink)
        {
            mNumActive++;
            DLink.addToFront(ref pActiveHead, newLink);
            return  newLink;
        }

        public DLink baseAdd()
        {
            return addToActive(getFromReserve());
        }

        public DLink baseFind(DLink pTarget)
        {
            DLink cur = pActiveHead;
            while (cur != null)
            {
                if (dCompareNodes(cur,pTarget))
                {
                    break;
                }
                cur = cur.pNext;
            }
            return cur;
        }


        //Remove moves a given node from the active list and puts it at the head of reserve list
        //I put bool in now because I wanna rework all of theese to do error/status enums.
        //I also wanna refactor theese eventually to use Error Enums and add more error handling or correction. But I don't quite understand what would be useful just yet.
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


        public int getActive()
        {
            return mNumActive;
        }

        public int getReserve()
        {
            return mNumReserved;
        }

        public int getTotal()
        {
            return mNumActive+mNumReserved;
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
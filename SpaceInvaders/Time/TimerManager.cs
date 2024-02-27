using System.Diagnostics;
using SpaceInvaders.Manager;
using SpaceInvaders.Commands;

namespace SpaceInvaders.Time
{

    class TimerManager : TMan
    {
        private static float currTime =0;
        private static TimerManager pInstance;
        private static TimeEvent poRefTE = new TimeEvent();
        private static Command poNullCMD = NullCMD.getInstance();

        private TimerManager(int numStart = 5, int deltaAdd = 3)
            : base(numStart,deltaAdd)
        {
        }

        //TODO: clean this up it's horrid. I don't think on remove a node is cleaned so I should be able to eleminate the if/else
        //but currently when I try to factor it out it breaks the list.
        public static void Update(float gameTime)
        {
            TimerManager t = TimerManager.getInstance();

            currTime += gameTime;
            TimeEvent e = (TimeEvent) t.getActiveHead();
            while(e != null)
            {

                if (currTime >= e.getTriggerTime())
                {
                    e.process();
                    if (e.pNext != null)
                    {
                        e = (TimeEvent)e.pNext;
                        t.baseRemove(e.pPrev);
                    } else
                    {
                        t.baseRemove(e);
                        e = null;
                    }

                }
                else
                {
                    break;
                }

            }
            
        }

        internal static void Clear()
        {
            TimerManager t = TimerManager.getInstance();
            TimeEvent e = (TimeEvent)t.getActiveHead();
            TimeEvent ePrev;
            while (e != null)
            {
                if (e.pNext != null)
                {
                    e = (TimeEvent)e.pNext;
                    ePrev = (TimeEvent)e.pPrev;
                    
                        t.baseRemove(e.pPrev);
                   
                }
                else
                {
                    
                        t.baseRemove(e);
                        e = null;
                }
            }
        }

        internal static void Clear(TimeEventID eID)
        {
            TimerManager t = TimerManager.getInstance();
            TimeEvent e = (TimeEvent) t.getActiveHead();
            TimeEvent ePrev;
            while (e != null)
            {
                if (e.pNext != null)
                {
                    e = (TimeEvent)e.pNext;
                    ePrev = (TimeEvent)e.pPrev;
                    if (ePrev.getName() == eID)
                    {
                        t.baseRemove(e.pPrev);
                    }
                }
                else
                {
                    if (e.getName() == eID)
                    {
                        t.baseRemove(e);
                    }
                    e = (TimeEvent)e.pNext;
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
                pInstance = new TimerManager(numStart, deltaAdd);
            }
        }

        public static TimeEvent Add(TimeEventID Name,Command cmd, float DTime)
        {
            Debug.Assert(DTime >= 0f);
        
            TimerManager pTMan = TimerManager.getInstance();
            Debug.Assert(pTMan != null);
            
            TimeEvent pNode = (TimeEvent)pTMan.getFromReserve();
            Debug.Assert(pNode != null);

            pNode.set(Name, DTime, currTime + DTime, cmd);    
            //InsertionSort
            pTMan.InSort(pNode);
            
            return pNode;
        }

        public TimeEvent InSort(TimeEvent te)
        {
            baseAddSort(te);
            return te;
        }

        public static float getCurrentTime()
        {
            return currTime;
        }

        public static TimerManager getInstance()
        {
            if (pInstance == null)
            {
                TimerManager.Create();
            }
            return pInstance;
        }

        protected override void dClearNode(DLink pLink)
        {
            TimeEvent p = (TimeEvent)pLink;
            p.dClean();
        }

        protected override bool dCompareNodes(DLink pLinkA, DLink pLinkB)
        {
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            TimeEvent left = (TimeEvent)pLinkA;
            TimeEvent right = (TimeEvent)pLinkB;

            if (left.getName() == right.getName())
            {
                return true;
            }
            return false;
        }

        protected override DLink dCreateNode()
        {
            DLink newNode = new TimeEvent();
            Debug.Assert(newNode != null);

            return newNode;
        }

        public TimeEvent Find(TimeEventID eventID)
        {
            TimerManager mrT = TimerManager.getInstance();
            Debug.Assert(mrT != null);
            TimeEvent target = mrT.toFind(eventID);
            return (TimeEvent)mrT.baseFind(target);
        }

        //for find
        private TimeEvent toFind(TimeEventID id)
        {
            poRefTE.setName(id);
            return poRefTE;
        }
    }
}

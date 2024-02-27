using SpaceInvaders.GameObjects;
using System.Diagnostics;

namespace SpaceInvaders.CollisionSystem
{
    class ColSubject
    {
        private ColObserver pHead;
        private GameObject pGObjA;
        private GameObject pGObjB;

        public ColSubject()
        {
            this.pGObjA = null;
            this.pGObjB = null;
            this.pHead = null;
        }

        public void Clean()
        {
            this.pGObjA = null;
            this.pGObjB = null;
            this.pHead = null;
        }

        public GameObject getA()
        {
            return pGObjA;
        }

        public GameObject getB()
        {
            return pGObjB;
        }

        public void setObjects(GameObject pA, GameObject pB)
        {
            Debug.Assert(null != pA);
            Debug.Assert(null != pB);

            pGObjA = pA;
            pGObjB = pB;
        }

        ~ColSubject()
        {
            this.pGObjA = null;
            this.pGObjB = null;
            this.pHead = null;

            //DLink pHead
            
            while( pHead != null )
            {
              //  DLink.RemoveNode(ref pHead,pHead);
            }
        }

        public void Attach(ColObserver observer)
        {
            // protection
            Debug.Assert(observer != null);

            observer.pSubject = this;

            // add to front
            if (pHead == null)
            {
                pHead = observer;
                observer.pNext = null;
                observer.pPrev = null;
            }
            else
            {
                observer.pNext = pHead;
                pHead.pPrev = observer;
                pHead = observer;
            }

        }

        public void Notify()
        {
            ColObserver pNode = this.pHead;

            while (pNode != null)
            {
                // Fire off listener
                pNode.Notify();

                pNode = (ColObserver)pNode.pNext;
            }
        }

        public void Detach()
        {
            //TODO Implement
        }

    }
}

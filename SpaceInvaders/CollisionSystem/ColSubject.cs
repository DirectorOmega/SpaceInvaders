using SpaceInvaders.GameObjects;
using System.Diagnostics;

namespace SpaceInvaders.CollisionSystem
{
    internal sealed class ColSubject
    {
        private ColObserver pHead;
        private GameObject pGObjA;
        private GameObject pGObjB;

        public ColSubject()
        {
            pGObjA = null;
            pGObjB = null;
            pHead = null;
        }

        public void Clean()
        {
            pGObjA = null;
            pGObjB = null;
            pHead = null;
        }

        public GameObject GetA() => pGObjA;
        public GameObject GetB() => pGObjB;

        public void setObjects(GameObject pA, GameObject pB)
        {
            Debug.Assert(null != pA);
            Debug.Assert(null != pB);

            pGObjA = pA;
            pGObjB = pB;
        }

        ~ColSubject()
        {
            pGObjA = null;
            pGObjB = null;
            pHead = null;

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
            ColObserver pNode = pHead;

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

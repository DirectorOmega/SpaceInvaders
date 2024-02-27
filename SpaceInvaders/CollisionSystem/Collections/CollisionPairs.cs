using SpaceInvaders.GameObjects;
using System.Diagnostics;

namespace SpaceInvaders.CollisionSystem
{
    enum CollisionPairName
    {
        reserveList,
        undef,
        GridvsMissile,
        UFOvsMissile,
        GridvsLeftWall,
        GridvsRightWall,
        MissilevsTopWall,
        PlayervsLBumper,
        PlayervsRBumper,
        BombsvsBottom,
        BombvsShip,
        MissilevsShield,
        BombvsShield,
        BombvsMissile,
        NoisevsShield,
        UFOvsLeftWall,
        GridvsShield
    }

    internal sealed class CollisionPair : ColLink
    {
        private CollisionPairName name;
        private GameObject A;
        private GameObject B;
        private ColSubject poSubject;

       public CollisionPair()
        {
            A = null;
            B = null;
            poSubject = new ColSubject();
        }

        public override void dClean()
        {
            name = CollisionPairName.reserveList;
            A = null;
            B = null;
            poSubject.Clean();
        }

        internal void Set(CollisionPairName name, GameObject treeRootA, GameObject treeRootB)
        {
            Debug.Assert(null != treeRootA);
            Debug.Assert(null != treeRootB);

            this.name = name;
            A = treeRootA;
            B = treeRootB;
        }

        public void SetName(CollisionPairName name) => this.name = name;

        public void Process() => Collide(A, B);

        static public void Collide(GameObject pSafeTreeA,GameObject pSafeTreeB)
        {
            GameObject pNodeA = pSafeTreeA;
            GameObject pNodeB;

            //  Debug.WriteLine("\nColPair: start {0}, {1}", pNodeA.getName(), pNodeB.getName());
            while (pNodeA != null)
            {
                // Restart compare
                pNodeB = pSafeTreeB;

                while (pNodeB != null)
                {
                    // who is being tested?
                    //Debug.WriteLine("ColPair: collide:  {0}, {1}", pNodeA.getName(), pNodeB.getName());

                    // Get rectangles
                    CollisionRect rectA = pNodeA.CollisionObject.GetColRect();
                    CollisionRect rectB = pNodeB.CollisionObject.GetColRect();

                    // test them
                    if (CollisionRect.Intersect(rectA, rectB))
                    {
                        // Boom - it works (Visitor in Action)
                       // Debug.WriteLine("Col Detected!");
                        pNodeA.Accept(pNodeB);
                        break;
                    }

                    pNodeB = (GameObject)pNodeB.getSibling();
                }
                pNodeA = (GameObject)pNodeA.getSibling();
            }
        }

        public void Attach(ColObserver ob)
        {
            Debug.Assert(null != ob);
            this.poSubject.Attach(ob);
        }

        public void SetCollision(GameObject pObjA, GameObject pObjB)
        {
            Debug.Assert(pObjA != null);
            Debug.Assert(pObjB != null);
            this.poSubject.setObjects(pObjA, pObjB);
        }

        public void NotifyListeners() => poSubject.Notify();

        public CollisionPairName GetName() => name;
    }
}

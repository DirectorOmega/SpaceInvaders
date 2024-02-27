using SpaceInvaders.GameObjects;
using System.Diagnostics;
using SpaceInvaders.GameObjects.Projectiles;

namespace SpaceInvaders.CollisionSystem
{
    static class Reactions
    {
        //TODO: remove the remove checks here and add them to observers so they can be delayed.
        public static void Reaction(Grid g, Sidewall s)
        {
            Debug.Assert(g != null);
            Debug.Assert(s != null);

            CollisionPair pColPair = CollisionPairManager.getActiveColPair();
            Debug.Assert(pColPair != null);
            pColPair.SetCollision(g, s);
            pColPair.NotifyListeners();
        }

        internal static void Reaction(Sidewall s, UFORoot ur) => CollisionPair.Collide(s, (GameObject)ur.getChild());

        internal static void Reaction(Sidewall s, UFO u)
        {
            Debug.Assert(s != null);
            Debug.Assert(u != null);
            CollisionPair pColPair = CollisionPairManager.getActiveColPair();
            Debug.Assert(null != pColPair);
            pColPair.SetCollision(s, u);
            pColPair.NotifyListeners();
        }

        internal static void Reaction(Missile m, UFO uFO)
        {
            Debug.Assert(m != null);
            Debug.Assert(uFO != null);
            CollisionPair pColPair = CollisionPairManager.getActiveColPair();
            Debug.Assert(null != pColPair);
            pColPair.SetCollision(m, uFO);
            pColPair.NotifyListeners();
        }

        internal static void Reaction(GNoiseRoot gNoiseRoot, ShieldRoot shieldRoot)
        {
            Debug.Assert(gNoiseRoot != null);
            Debug.Assert(shieldRoot != null);
            CollisionPair.Collide(gNoiseRoot,(GameObject) shieldRoot.getChild());
            //probably want to change this.
            if (null == gNoiseRoot.getChild())
            {
                gNoiseRoot.setCoords(-42, -42);
                gNoiseRoot.                CollisionObject.GetColRect().Set(0.0f, 0.0f, 0.0f, 0.0f);
            }
            if (null == shieldRoot.getChild())
            {
                shieldRoot.                CollisionObject.GetColRect().Set(0.0f, 0.0f, 0.0f, 0.0f);
                shieldRoot.setCoords(-41, -41);
            }
        }

        internal static void Reaction(Missile missile, UFORoot ur)
        {
            Debug.Assert(missile != null);
            Debug.Assert(ur != null);
            CollisionPair.Collide(missile,(GameObject) ur.getChild());
            //if(null == ur.getChild())
            //{
            //    ur.getCollisionObject().getColRect().Set(-280.0f, -80.0f, 0.0f, 0.0f);
            //    ur.setCoords(-280, -80);
            //}
        }

        internal static void Reaction(GNoiseRoot gNoiseRoot, Shield shield)
        {
            CollisionPair.Collide((GameObject)gNoiseRoot.getChild(), shield);
            //if (null == shield.getChild())
            //{
            //    shield.Remove();
            //}
        }

        internal static void Reaction(GNoisePoint gnp, Shield shield) => CollisionPair.Collide(gnp, (GameObject)shield.getChild());

        internal static void Reaction(GNoisePoint gnp, ShieldColumn shieldColumn)
        {
            CollisionPair.Collide(gnp, (GameObject)shieldColumn.getChild());
            //if(null == shieldColumn.getChild())
            //{
            //    shieldColumn.Remove();
            //}
        }

        internal static void Reaction(GNoisePoint gnp, ShieldBrick sb)
        {
            CollisionPair pColPair = CollisionPairManager.getActiveColPair();
            Debug.Assert(pColPair != null);
            pColPair.SetCollision(gnp, sb);
            pColPair.NotifyListeners();
        }

        internal static void Reaction(BombRoot br, BottomWall bw)
        {
            CollisionPair.Collide((GameObject)br.getChild(), bw);
            //probably want to change this.
            if (null == br.getChild())
            {
                br.setCoords(-30, -30);
                br.                CollisionObject.GetColRect().Set(0.0f, 0.0f, 0.0f, 0.0f);
             
            }
        }

        internal static void Reaction(BombRoot br, Missile missile)
        {
            CollisionPair.Collide((GameObject)br.getChild(), missile);
            if(null == br.getChild())
            {
                br.setCoords(-30, -30);
                br.                CollisionObject.GetColRect().Set(0.0f, 0.0f, 0.0f, 0.0f);  
            }
        }

        internal static void Reaction(Bomb b, Missile missile)
        {
            CollisionPair pColPair = CollisionPairManager.getActiveColPair();
            Debug.Assert(pColPair != null);
            pColPair.SetCollision(b, missile);
            pColPair.NotifyListeners();
        }

        internal static void Reaction(Bomb b, BottomWall bWall)
        {
            Debug.Assert(b != null);
            Debug.Assert(bWall != null);

            CollisionPair pColPair = CollisionPairManager.getActiveColPair();
            Debug.Assert(pColPair != null);
            pColPair.SetCollision(b, bWall);
            pColPair.NotifyListeners();
        }

        internal static void Reaction(BombRoot br, Ship ship)
        {
            CollisionPair.Collide((GameObject)br.getChild(), ship);
            //probably want to remove this.
            if (null == br.getChild())
            {
                //br.Remove();
                br.setCoords(-30, -30);
                br.                CollisionObject.GetColRect().Set(0.0f, 0.0f, 0.0f, 0.0f);
                // br.setCoords(-30, -30);
            }
        }

        internal static void Reaction(Bomb b, ShieldRoot shieldRoot) 
            => CollisionPair.Collide(b, (GameObject)shieldRoot.getChild());

        internal static void Reaction(Grid g, ShieldRoot sr) 
            => CollisionPair.Collide((GameObject)sr.getChild(), g);

        internal static void Reaction(Grid g, Shield s) 
            => CollisionPair.Collide((GameObject)g.getChild(), s);

        //colliding with columns because the bottom of hte column is what will erase a shield anyway
        internal static void Reaction(Column c, Shield s)
        {
            // CollisionPair.Collide((GameObject)c.getChild(), s);
            CollisionPair.Collide(c, (GameObject)s.getChild());
        }

        //internal static void Reaction(Alien a, Shield s)
        //{
        //    CollisionPair.Collide(a, (GameObject)s.getChild());
        //}

        internal static void Reaction(Column c, ShieldColumn sc) 
            => CollisionPair.Collide(c, (GameObject)sc.getChild());

        internal static void Reaction(Column c, ShieldBrick b)
        {
            CollisionPair pColPair = CollisionPairManager.getActiveColPair();
            Debug.Assert(pColPair != null);
            pColPair.SetCollision(c, b);
            pColPair.NotifyListeners();
        }

        internal static void Reaction(BombRoot br, ShieldRoot shieldRoot)
        {
            CollisionPair.Collide((GameObject)br.getChild(), shieldRoot);
            if (null == br.getChild())
            {
                //br.Remove();
                br.setCoords(-30, -30);
                br.                CollisionObject.GetColRect().Set(0.0f, 0.0f, 0.0f, 0.0f);
            }
            if(null == shieldRoot.getChild())
            {
                // shieldRoot.Remove();
                shieldRoot.setCoords(-41, -41);
                shieldRoot.                CollisionObject.GetColRect().Set(0.0f, 0.0f, 0.0f, 0.0f);
            }
        }

        internal static void Reaction(Bomb b, ShieldColumn shieldColumn)
        {
            CollisionPair.Collide(b, (GameObject)shieldColumn.getChild());
            //if (null == shieldColumn.getChild())
            //{
            //    shieldColumn.Remove();
            //}
        }

        internal static void Reaction(Bomb b, ShieldBrick shieldBrick)
        {
            CollisionPair pColPair = CollisionPairManager.getActiveColPair();
            Debug.Assert(pColPair != null);
            pColPair.SetCollision(b, shieldBrick);
            pColPair.NotifyListeners();
        }

        internal static void Reaction(Bomb b, Ship ship)
        {
            CollisionPair pColPair = CollisionPairManager.getActiveColPair();
            Debug.Assert(pColPair != null);
            pColPair.SetCollision(b, ship);
            pColPair.NotifyListeners();
        }

        internal static void Reaction(Missile m, ShieldRoot shieldRoot)
        {
            CollisionPair.Collide(m, (GameObject)shieldRoot.getChild());
            //if (null == shieldRoot.getChild())
            //{
            //      //hack hack cuz the root is needed for every additon to the tree.
            //    shieldRoot.setCoords(-60, -60);
            //    shieldRoot.getCollisionObject().getColRect().Set(0.0f, 0.0f, 0.0f, 0.0f);
            //}
        }

        internal static void Reaction(Bomb b, Shield shield)
        {
            CollisionPair.Collide(b, (GameObject)shield.getChild());
            //if (null == shield.getChild())
            //{
            //    shield.Remove();
            //}
        }

        internal static void Reaction(Missile m, Shield shield)
        {
            CollisionPair.Collide(m, (GameObject)shield.getChild());
            //if(null == shield.getChild())
            //{
            //    shield.Remove();
            //}
        }

        internal static void Reaction(Missile m, ShieldColumn shieldColumn)
        {
            CollisionPair.Collide(m, (GameObject)shieldColumn.getChild());
            //if(null == shieldColumn.getChild())
            //{
            //    shieldColumn.Remove();
            //}
        }

        internal static void Reaction(Missile m, ShieldBrick sb)
        {
            CollisionPair pCP = CollisionPairManager.getActiveColPair();
            pCP.SetCollision(m, sb);
            pCP.NotifyListeners();
        }

        internal static void Reaction(Bumper b, Ship s)
        {
            Debug.Assert(b != null);
            Debug.Assert(s != null);

            CollisionPair pColPair = CollisionPairManager.getActiveColPair();
            Debug.Assert(pColPair != null);
            pColPair.SetCollision(b, s);
            pColPair.NotifyListeners();
        }

        //TODO: setup a observer for when the grid disappears so I can end the level.
        public static void Reaction(Grid g, Missile m)
        {
            CollisionPair.Collide((GameObject) g.getChild(), m);
            //if(null == g.getChild())
            //{
            //    g.Remove();
            //}
        }

        internal static void Reaction(Column c, Missile missile)
        {
            CollisionPair.Collide((GameObject)c.getChild(), missile);
            //if(null == c.getChild())
            //{
            //    c.Remove();
            //}
        }
        //todo fix ordering on these I fucked it up.
        internal static void Reaction(Missile missile, Octo o)
        {
            CollisionPair pCP = CollisionPairManager.getActiveColPair();
            pCP.SetCollision(o, missile);
            pCP.NotifyListeners();
        }

        internal static void Reaction(Missile missile, Squid s)
        {
            CollisionPair pCP = CollisionPairManager.getActiveColPair();
            pCP.SetCollision(s, missile);
            pCP.NotifyListeners();
        }

        internal static void Reaction(Crab c, Missile missile)
        {
            CollisionPair pCP = CollisionPairManager.getActiveColPair();
            pCP.SetCollision(c, missile);
            pCP.NotifyListeners();
        }

        public static void Reaction(Missile m , Topwall t)
        {
            CollisionPair pColPair = CollisionPairManager.getActiveColPair();
            pColPair.SetCollision(m, t);
            pColPair.NotifyListeners();
           
        }
    }
}

using SpaceInvaders.GraphicalObjects;
using System.Diagnostics;

namespace SpaceInvaders.CollisionSystem
{
    internal sealed class CollisionObject
    {
        private CollisionRect poColRect;
        //hint use a box sprite; I could force it but I don't want too just yet.
        baseSprite pColSprite;

        public CollisionObject(baseSprite pProxySprite)
        {
            Debug.Assert(pProxySprite != null);
            // Origin is in the UPPER RIGHT 

            Azul.Rect t = new Azul.Rect(pProxySprite.getX(), pProxySprite.getY(), pProxySprite.getScreenRect().width, pProxySprite.getScreenRect().height);

            poColRect = new CollisionRect(t);

            //this.poColRect = new CollisionRect(pProxySprite.getScreenRect());
            //this.poColRect = new CollisionRect(pProxySprite.get)
            Debug.Assert(poColRect != null);

            // Create the sprite
            pColSprite = BoxSpriteManager.Add(SpriteID.Box, poColRect);
            Debug.Assert(pColSprite != null);
            pColSprite.setColor(255.0f, 0.0f, 0.0f);
        }

        public void setColRect(baseSprite t)
        {
            pColSprite.setScreenRect(t.getScreenRect());
            poColRect.Set(t.getScreenRect());
        }

        public baseSprite GetColSprite() => pColSprite;
        public CollisionRect GetColRect() => poColRect;

        //I need to change how this is called so it will only be called on add or remove of a object.
        public void Union(CollisionObject o)
        {
            poColRect.Union(o.poColRect);
            pColSprite.setScreenRect(poColRect);
        }

        public void UpdatePos(float x, float y)
        {
            poColRect.x = x;
            poColRect.y = y;

            pColSprite.setCoords(poColRect.x, poColRect.y);
            pColSprite.setScreenRect(poColRect);
            //will need to remove this when it's a proxy box sprite.
            pColSprite.Update();
        }
    }
}

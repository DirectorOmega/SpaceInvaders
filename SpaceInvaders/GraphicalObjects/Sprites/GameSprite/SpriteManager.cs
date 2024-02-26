using System.Diagnostics;
using SpaceInvaders.Manager;

namespace SpaceInvaders.GraphicalObjects
{
    class GameSpriteManager : GSMan
    {
        private static GameSpriteManager pInstance;
        private static GameSprite poRefSprite = new GameSprite();
        //private static GameSprite pCachedSprite;

        private GameSpriteManager(int numStart = 5, int deltaAdd = 3)
            : base(numStart, deltaAdd)
        {
        }

        public static GameSprite Add(SpriteID Name, Image image,Azul.Rect screenRect)
        {
            GameSpriteManager pTMan = GameSpriteManager.getInstance();
            Debug.Assert(pTMan != null);

            GameSprite pNode = (GameSprite)pTMan.baseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(Name,image,screenRect);

            return pNode;
        }

        public static GameSprite Add(SpriteID Name, Image image, Azul.Rect screenRect,Azul.Color color)
        {
            GameSpriteManager pTMan = GameSpriteManager.getInstance();
            Debug.Assert(pTMan != null);

            GameSprite pNode = (GameSprite)pTMan.baseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(Name, image, screenRect, color);

            return pNode;
        }


        public static void Create(int numStart = 5, int deltaAdd = 3)
        {
            Debug.Assert(numStart > 0);
            Debug.Assert(deltaAdd > 0);
            Debug.Assert(pInstance == null);

            if (pInstance == null)
            {
                pInstance = new GameSpriteManager(numStart, deltaAdd);
                Debug.Assert(null!= GameSpriteManager.Add(SpriteID.NullSprite, ImageManager.Find(ImageID.Default), new Azul.Rect(0f, 0f, 0f, 0f)));
                Debug.Assert(null != GameSpriteManager.Add(SpriteID.Undef, ImageManager.Find(ImageID.Default), new Azul.Rect(0.0f, 0.0f, 20.0f, 20.0f)));
            }
        }

        protected override void dClearNode(DLink pLink)
        {
            GameSprite p = (GameSprite)pLink;
            p.dClean();
        }

        protected override bool dCompareNodes(DLink pLinkA, DLink pLinkB)
        {
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            GameSprite left = (GameSprite)pLinkA;
            GameSprite right = (GameSprite)pLinkB;

            if (left.getName() == right.getName())
            {
                return true;
            }
            return false;
        }

        protected override DLink dCreateNode()
        {
            DLink newNode = new GameSprite();
            Debug.Assert(newNode != null);

            return newNode;
        }

        public static GameSprite Find(SpriteID sprite)
        {
            GameSpriteManager sMan = GameSpriteManager.getInstance(); 
               
            //I noticed I make objects in groups I want to create a cache object that would bypass the find logic for
            //repeated searches for the same object, would be very helpful for creating the shield objects, since many of those
            //are stamped out back to back.

            //if(pCachedSprite.getName() == sprite)
            //{
            //    return pCachedSprite;
            //}

            GameSprite target = GameSpriteManager.toFind(sprite);
            return (GameSprite)sMan.baseFind(target);

        }

        private static GameSprite toFind(SpriteID name)
        {
            poRefSprite.setName(name);
            return poRefSprite;
        }

       public static GameSpriteManager getInstance()
        {
            if (pInstance == null)
            {
                GameSpriteManager.Create();
            }
            return pInstance;
        } 
    }
}

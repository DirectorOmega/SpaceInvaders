namespace SpaceInvaders.GraphicalObjects
{

    //TODO: refiine rectangles down to be closer to the sprites
    public static class ISS
    {
        //this is just so I don't have to figure out all the god damn rectangles on the sprite sheet again
        private static Azul.Texture pInvadersTex = new Azul.Texture("Aliens14x14.tga");

            //The texture sheet.
        public static Azul.Texture getTexture() { return pInvadersTex; }

        //baddie textures
        public static Azul.Rect getShieldBrick() { return new Azul.Rect(266.0f, 154.0f, 14.0f, 14.0f); }

        public static Azul.Rect getShieldBrickAdjSize() { return new Azul.Rect(0.0f, 0.0f, 6.0f, 6.0f); }

        public static Azul.Rect getSquidF1() { return new Azul.Rect(56f, 28.0f, 168f, 114f); }

        public static Azul.Rect getSquidF2() { return new Azul.Rect(56f, 180.0f, 168f, 114f); }

        public static Azul.Rect getCrabF1() { return new Azul.Rect(322f, 28.0f, 168f, 114f); }

        public static Azul.Rect getCrabF2() { return new Azul.Rect(322f, 180f, 168f, 114f); }

        public static Azul.Rect getOctoF1() { return new Azul.Rect(590f, 28.0f, 168.0f, 114.0f); }

        public static Azul.Rect getOctoF2() { return new Azul.Rect(590f, 180f, 168f, 114f); }

        public static Azul.Rect getAlienTextureSize() { return new Azul.Rect(0f, 0f, 168f, 114f); }

        public static Azul.Rect getAlienAdjSize() { return new Azul.Rect(0f, 100f, 42f, 28.5f); }
       
        public static Azul.Rect getAlienExp() { return new Azul.Rect(573f, 489f, 184f, 114f);}

        public static Azul.Rect getAlienExpTexSize() { return new Azul.Rect(0.0f, 0.0f, 184.0f, 114.0f);}
         
        public static Azul.Rect getAlienExpAdjSize() { return new Azul.Rect(0.0f, 0.0f, 62.0f, 38.0f);}

        public static Azul.Rect getUFO() { return new Azul.Rect(84f, 505f, 224f, 100f);}

        public static Azul.Rect getUFOExp() { return new Azul.Rect(44f, 645f, 293f, 113f);}

        public static Azul.Rect getUFOSize() { return new Azul.Rect(0f, 0f, 224f, 100f);}

        public static Azul.Rect getUFOAdjSize() { return new Azul.Rect(0f, 0f, 75f, 34f); }
        //Ship related textures
        public static Azul.Rect getHero() { return new Azul.Rect(56.0f, 340f, 182f, 112f);}

        public static Azul.Rect getHeroExp1() { return new Azul.Rect(281f, 323f, 182f, 112f);}

        public static Azul.Rect getHeroExp2() { return new Azul.Rect(547f, 323f, 182f, 112f);}

        public static Azul.Rect getHeroTextureSize() { return new Azul.Rect(0f, 0f, 182f, 112f);}

        public static Azul.Rect getHeroAdjSize() { return new Azul.Rect(0f, 0f, 60f, 28f); }
 
        //ballistics
        public static Azul.Rect getHeroShot() { return new Azul.Rect(420f, 700f, 14f, 56f);}
       
        public static Azul.Rect getHeroShotExp() { return new Azul.Rect(406f, 490f, 113f, 116f); }

        public static Azul.Rect getHeroShotExpTextureSize() { return new Azul.Rect(0.0f, 0.0f, 113.0f, 116.0f); }

        public static Azul.Rect getMissileExpAdjSize() { return new Azul.Rect(0.0f, 0.0f, 28.0f, 28.0f); }

        public static Azul.Rect getShotSize() { return new Azul.Rect(0f, 100f, 15f, 56f);} 

        public static Azul.Rect getAlienShotAdjSize() { return new Azul.Rect(0f, 0f, 10f, 25f); }

        public static Azul.Rect getShotAdjSize() { return new Azul.Rect(0f, 0f, 5f, 19f);}

        public static Azul.Rect getZigZagShotF1() { return new Azul.Rect(490f, 644f, 42f, 98f);}

        public static Azul.Rect getZigZagShotF2() { return new Azul.Rect(574f, 644f, 42f, 98f);}

        public static Azul.Rect getZigZagShotF3() { return new Azul.Rect(658f, 644f, 42f, 98f);}

        public static Azul.Rect getZigZagShotF4() { return new Azul.Rect(742f, 644f, 42f, 98f);}

        public static Azul.Rect getRollerShotF1() { return new Azul.Rect(28f, 798f, 42f, 84f);}
        public static Azul.Rect getRollerShotF2() { return new Azul.Rect(112f, 798f, 42f, 84f);}
        public static Azul.Rect getRollerShotF3() { return new Azul.Rect(196f, 798f, 42f, 84f);}
        public static Azul.Rect getRollerShotF4() { return new Azul.Rect(280f, 798f, 42f, 84f);}

        public static Azul.Rect getThirdShotF1() { return new Azul.Rect(364f, 798f, 42f, 98f);}
        public static Azul.Rect getThirdShotF2() { return new Azul.Rect(454f, 798f, 42f, 98f);}
        public static Azul.Rect getThirdShotF3() { return new Azul.Rect(532f, 798f, 42f, 98f);}
        public static Azul.Rect getThirdShotF4() { return new Azul.Rect(616f, 798f, 42f, 98f);}

        public static Azul.Rect getAlienShotExp()  {return new Azul.Rect(700f,798f,84f,112f);}

        public static Azul.Rect getAlienShotExpSize() { return new Azul.Rect(0f, 100f, 112f, 134f);}

        public static Azul.Rect getAlienShotExpAdjSize() { return new Azul.Rect(0f, 100f, 56f, 67f); }

    }
}

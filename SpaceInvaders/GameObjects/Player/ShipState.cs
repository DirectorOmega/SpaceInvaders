namespace SpaceInvaders.GameObjects
{
    abstract class MvState
    {
       public abstract void Handle(Ship pShip);
       public abstract void moveRight(Ship pShip); 
       public abstract void moveLeft(Ship pShip); 
         
    }
}

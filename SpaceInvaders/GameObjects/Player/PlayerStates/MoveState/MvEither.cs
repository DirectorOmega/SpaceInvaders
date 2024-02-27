namespace SpaceInvaders.GameObjects
{
    class MvEither : MvState
    {
        public override void Handle(Ship pShip) { }

        public override void moveLeft(Ship pShip) 
            => pShip.incrementX(-pShip.getSpeed());

        public override void moveRight(Ship pShip) 
            => pShip.incrementX(pShip.getSpeed());
    }
}

using SpaceInvaders.GameObjects;
using System.Diagnostics;

namespace SpaceInvaders.InputSystem
{
    internal sealed class InputManager
    {
        private static Ship pShip;

        // Data: ----------------------------------------------
        private static InputManager pInstance = null;
        private bool privSpaceKeyPrev;
        private bool privOKeyPrev;
        private bool privEnterKeyPrev;
        private bool privNum1Prev;
        //private bool privNum2Prev;

        private InputSubject pSubjectArrowRight;
        private InputSubject pSubjectArrowLeft;
        private InputSubject pSubjectSpace;
        private InputSubject pSubjectEnter;
        private InputSubject pSubjectnum1;
        //private InputSubject pSubjectnum2;
        private InputSubject pSubjectO;

        private InputManager()
        {
            this.pSubjectArrowLeft = new InputSubject();
            this.pSubjectArrowRight = new InputSubject();
            this.pSubjectSpace = new InputSubject();

            this.pSubjectEnter = new InputSubject();
            this.pSubjectnum1 = new InputSubject();
          //  this.pSubjectnum2 = new InputSubject();

            this.pSubjectO = new InputSubject();

            this.privSpaceKeyPrev = false;
            this.privEnterKeyPrev = false;
            this.privNum1Prev = false;
           // this.privNum2Prev = false;
            this.privOKeyPrev = false;
        }

        private static void setActiveShip(Ship p)
        {
            pShip = p;
        }

        private static Ship getActiveShip()
        {
            return pShip;
        }

        //I do all the other ones lazily but this one I want to be setup before use.
        private static InputManager privGetInstance()
        {
            if (pInstance == null)
            {
                pInstance = new InputManager();
            }
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        public static InputSubject GetKey1Subject()
        {
            InputManager pMan = InputManager.privGetInstance();
            Debug.Assert(pMan != null);
            return pMan.pSubjectnum1;
        }

        //public static InputSubject GetKey2Subject()
        //{
        //    InputManager pMan = InputManager.privGetInstance();
        //    Debug.Assert(pMan != null);
        //    return pMan.pSubjectnum2;
        //}

        public static InputSubject GetEnterSubject()
        {
            InputManager pMan = InputManager.privGetInstance();
            Debug.Assert(pMan != null);


            return pMan.pSubjectEnter;
        }

        public static InputSubject GetArrowRightSubject()
        {
            InputManager pMan = InputManager.privGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectArrowRight;
        }

        public static InputSubject GetArrowLeftSubject()
        {
            InputManager pMan = InputManager.privGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectArrowLeft;
        }

        public static InputSubject GetSpaceSubject()
        {
            InputManager pMan = InputManager.privGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pSubjectSpace;
        }

        public static InputSubject GetOSubject()
        {
            InputManager pMan = InputManager.privGetInstance();
            Debug.Assert(pMan != null);


            return pMan.pSubjectO;
        }

        public static void Update()
        {
            InputManager pMan = InputManager.privGetInstance();
            Debug.Assert(pMan != null);

            bool num1Curr = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_1);

            if (num1Curr == true && pMan.privNum1Prev == false)
            {
                pMan.pSubjectnum1.Notify();
            }
            pMan.privNum1Prev = num1Curr;

            //cheater key B to blast missiles as fast as possibble
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_B) == true)
            {
                pMan.pSubjectSpace.Notify();
            }

            // SpaceKey: (with key history) -----------------------------------------------------------
            bool spaceKeyCurr = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_SPACE);
           // if (spaceKeyCurr) { Debug.Print("SpaceKeyPressed\n"); }
            if (spaceKeyCurr == true && pMan.privSpaceKeyPrev == false)
            {
                pMan.pSubjectSpace.Notify();
            }
            pMan.privSpaceKeyPrev = spaceKeyCurr;

            // EnterKey: (with key history)-----------------------------------------------------------
            bool EnterKeyCurr = Azul.Input.GetKeyState((Azul.AZUL_KEY) 294);
           // if (EnterKeyCurr) { Debug.Print("EnterKeyPressed\n"); }
            if (EnterKeyCurr == true && pMan.privEnterKeyPrev == false)
            {
                pMan.pSubjectEnter.Notify();
            }
            pMan.privEnterKeyPrev = EnterKeyCurr;
            //OKey: (with key history)----------------------------------------------------------------
            bool OKeyCurr = Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_O);
            // if (EnterKeyCurr) { Debug.Print("EnterKeyPressed\n"); }
            if (OKeyCurr == true && pMan.privOKeyPrev == false)
            {
                pMan.pSubjectO.Notify();
            }
            pMan.privOKeyPrev = OKeyCurr;

            // LeftKey: (no history) -----------------------------------------------------------
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_LEFT) == true)
            {
                pMan.pSubjectArrowLeft.Notify();
            }

            // RightKey: (no history) -----------------------------------------------------------
            if (Azul.Input.GetKeyState(Azul.AZUL_KEY.KEY_ARROW_RIGHT) == true)
            {
                pMan.pSubjectArrowRight.Notify();
            }
          
        }

    }
}

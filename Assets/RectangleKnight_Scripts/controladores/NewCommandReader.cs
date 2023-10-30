using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using static Rectangle_Knight_Input_Actions;

namespace Assets.RectangleKnight_Scripts.controladores
{
    public class NewCommandReader :  MonoBehaviour, IPlayerActions
    {
        Rectangle_Knight_Input_Actions actions;

        public Vector3 move;
        public bool attack;
        public bool magicAttack;
        public bool magicHold;
        public bool dash;
        public bool defense;
        public bool startJump;
        public bool holdJump;
        public bool mudaEspadaMais;
        public bool mudaEspadaMenos;

        private void Awake()
        {
            actions = new Rectangle_Knight_Input_Actions();
            actions.Player.SetCallbacks(this);
        }

        private void OnEnable()
        {
            actions.Enable();
        }

        private void OnDisable()
        {
            actions.Disable();
        }
        private void Start()
        {
            
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            move = context.ReadValue<Vector2>();
            Vector3 forward = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);//new Vector3(1, 0, 0);
            /*
            if (AplicadorDeCamera.cam != null)
                if (AplicadorDeCamera.cam.Cdir != null)
                    forward = AplicadorDeCamera.cam.Cdir.DirecaoInduzida(h, v);*/

            forward.y = 0;
            forward = forward.normalized;

            Vector3 right = new Vector3(forward.z, 0, -forward.x);

            //Debug.Log(forward + " : " + right + " :" + h + " : " + v);
            
            move = move.x * right + move.y * forward;
            //Debug.Log("Pressed: " + context.interaction);
            //Debug.Log("Eu estou ovando: " + context.ReadValue<Vector2>());
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            
            //throw new System.NotImplementedException();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                attack = true;
                new MyInvokeMethod().InvokeAoFimDoQuadro(() => { attack = false; });
            }
        }

        public void OnMagicAttack(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Canceled)
            {
                magicAttack = true;
                new MyInvokeMethod().InvokeAoFimDoQuadro(() => { magicAttack = false; });
            }
            if (context.phase == InputActionPhase.Started)
                magicHold = true;
            else if (context.phase == InputActionPhase.Canceled)
                magicHold = false;

        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                dash = true;
                new MyInvokeMethod().InvokeAoFimDoQuadro(() => { dash = false; });
            }
            
            
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.interaction is PressInteraction)
            {
                if (context.phase == InputActionPhase.Performed)
                {
                    startJump = true;
                    holdJump = true;
                }

                #region umTeste_b
                //if (context.phase == InputActionPhase.Started)
                //{
                //    Debug.Log("jump start press: " + startJump + " : " + holdJump);
                //}
                //if (context.phase == InputActionPhase.Performed)
                //{
                //    Debug.Log("jump perform press: " + startJump + " : " + holdJump);
                //}
                //if (context.phase == InputActionPhase.Canceled)
                //{
                //    Debug.Log("jump calcel press: " + startJump + " : " + holdJump);
                //} 
                #endregion
            }

            #region umTeste
            //if (context.interaction is HoldInteraction)
            //{
            //    if (context.phase == InputActionPhase.Started)
            //    {
            //        Debug.Log("jump start hold: " + startJump + " : " + holdJump);
            //    }
            //    if (context.phase == InputActionPhase.Performed)
            //    {
            //        Debug.Log("jump perform hold: " + startJump + " : " + holdJump);
            //    }
            //    if (context.phase == InputActionPhase.Canceled)
            //    {
            //        Debug.Log("jump calcel hold: " + startJump + " : " + holdJump);
            //    }

            //} 
            #endregion

            if (context.phase == InputActionPhase.Canceled)
                holdJump = false;

            new MyInvokeMethod().InvokeAoFimDoQuadro(() =>
            {
                startJump = false;
            });
        }

        public void OnParryDefense(InputAction.CallbackContext context)
        {
            defense = context.phase == InputActionPhase.Performed;
            //throw new System.NotImplementedException();
        }

        public void OnMudaEspadaMais(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                mudaEspadaMais = true;
                new MyInvokeMethod().InvokeAoFimDoQuadro(() => { mudaEspadaMais = false; });
            }
        }

        public void OnMudaEspadaMenos(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                mudaEspadaMenos = true;
                new MyInvokeMethod().InvokeAoFimDoQuadro(() => { mudaEspadaMenos = false; });
            }
        }
    }
}
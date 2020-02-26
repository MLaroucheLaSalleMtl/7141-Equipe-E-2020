using UnityEngine;
using UnityEngine.InputSystem;
namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
        float h = 0;    //CrossPlatformInputManager.GetAxis("Horizontal");
        float v = 0; //CrossPlatformInputManager.GetAxis("Vertical");
        bool crouch = false;//Input.GetKey(KeyCode.C);
        bool jumping = false;
        bool walk = false;



        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 value = context.ReadValue<Vector2>(); //stock dedans vector2
            h = value.x;
            v = value.y;
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            //jumping = context.performed; équivalent du GetButton
            // if (context.started)  équivalent GetButtonDown
            // {
            // jumping = true;
            //}
            if (context.canceled)  //équivalent GetButtonUp
            {
                jumping = true;
            }
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                crouch = !crouch; //methode friendly de faire un toggle
            }
        }

        public void OnWalk(InputAction.CallbackContext context)
        {
            walk = context.performed;
        }


        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = jumping;//Keyboard.current.spaceKey.isPressed; //CrossPlatformInputManager.GetButtonDown("Jump");
                jumping = false;
            }

        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            
            

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v*m_CamForward + h*m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v*Vector3.forward + h*Vector3.right;
            }
#if !MOBILE_INPUT
			// walk speed multiplier
	        if (walk) m_Move *= 0.5f;
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);
            m_Jump = false;
        }
    }
}

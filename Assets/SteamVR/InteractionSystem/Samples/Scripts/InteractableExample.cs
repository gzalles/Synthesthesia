//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Demonstrates how to create a simple interactable object
//
//=============================================================================

using UnityEngine;
using System.Collections;
using Valve.VR;

namespace Valve.VR.InteractionSystem.Sample
{
    //-------------------------------------------------------------------------
    [RequireComponent(typeof(Interactable))]
    public class InteractableExample : MonoBehaviour
    {
        private TextMesh textMesh;
        private Vector3 oldPosition;
        private Quaternion oldRotation;
        public HandEvent onButtonDown;

        /*
        [SteamVR_DefaultActionSet("myController")]
        public SteamVR_ActionSet actionSet;

        [SteamVR_DefaultAction("Touchpad", "myController")]
        public SteamVR_Action_Vector2 a_Touchpad;
        */

        //private SteamVR_Controller.Device device;

        private float attachTime;
        
		private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & ( ~Hand.AttachmentFlags.SnapOnAttach ) & (~Hand.AttachmentFlags.DetachOthers) & (~Hand.AttachmentFlags.VelocityMovement);

        private Interactable interactable;

        Renderer rend; // Yana
        private bool muted; //Yana
            

            //public AudioClip myAudioClip;// -gabriel
            AudioSource myAudioSource;// -shinu

        public Component[] myComponents; //yana
        public Material myMaterial;
        private Color onColor;
        private Color offColor;

        //-------------------------------------------------

        void Awake()
		{
            //interactable = GetComponent<Interactable>();
            //interactable.activateActionSetOnAttach = SteamVR_ActionSet;


            onColor = new Color(.8980f, .5098f, .8901f, .4627f);
            offColor = new Color(.999f, .999f, .999f, .4627f);

            myAudioSource = GetComponent<AudioSource>(); //added audio component -shinu
            myAudioSource.mute = true; // start as muted
            myAudioSource.Play();   // play on start
            myAudioSource.loop = true;  // loop in background

            textMesh = GetComponentInChildren<TextMesh>();
			//textMesh.text = "No Hand Hovering"; //gabriel zalles commented this

            interactable = this.GetComponent<Interactable>();

            rend = GetComponent<Renderer>(); //Yana
            muted = true; //yana 

            //myComponents = GetComponentInChildren<MeshRenderer>().material;
            myMaterial = GetComponentInChildren<MeshRenderer>().material; //yana
            //Debug.Log("here");
            //string toPrint = myMaterial.GetTexturePropertyNames;
            //Debug.Log(toPrint);


        }

        void Update()
        {

            


        }

        
		//-------------------------------------------------
		// Called when a Hand starts hovering over this object
		//-------------------------------------------------
		private void OnHandHoverBegin( Hand hand )
		{


            myAudioSource.mute = !myAudioSource.mute; //mute or unmute on hover
            muted = !muted; // change the bool to opposite, Yana


            if (!myAudioSource.mute) // yana
            {

                //myMaterial.shader = Shader.Find("Standard (Standard)");
                
                myMaterial.SetFloat("_Glossiness", .558f);
                myMaterial.SetColor("_Color", onColor);
                /*
                myMaterial.shader = Shader.Find("_Color");
                myMaterial.SetColor("_Color", Color.grey);
                myMaterial.shader = Shader.Find("Specular");
                myMaterial.SetColor("_SpecColor", Color.yellow);
                */
            }
            else //yana
            {
                //myMaterial.shader = Shader.Find("Standard");
                myMaterial.SetFloat("_Glossiness", 1);
                myMaterial.SetColor("_Color", offColor);
                /*
                myMaterial.shader = Shader.Find("_Color");
                myMaterial.SetColor("_Color", Color.grey);
                myMaterial.shader = Shader.Find("Specular");
                myMaterial.SetColor("_SpecColor", Color.grey);
                */
            }
            //textMesh.text = "Hovering hand: " + hand.name; //gabriel zalles commented this
        }

        

        //-------------------------------------------------
        // Called when a Hand stops hovering over this object
        //-------------------------------------------------
        private void OnHandHoverEnd( Hand hand )
		{
            //textMesh.text = "No Hand Hovering"; //gabriel zalles commented this
        }


        //-------------------------------------------------
        // Called every Update() while a Hand is hovering over this object
        //-------------------------------------------------
        private void HandHoverUpdate( Hand hand )
		{
            GrabTypes startingGrabType = hand.GetGrabStarting();
            bool isGrabEnding = hand.IsGrabEnding(this.gameObject);





            if (interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
            {
                // Save our position/rotation so that we can restore it when we detach
                //oldPosition = transform.position;
                //oldRotation = transform.rotation;

                // Call this to continue receiving HandHoverUpdate messages,
                // and prevent the hand from hovering over anything else
                hand.HoverLock(interactable);

                // Attach this object to the hand
                hand.AttachObject(gameObject, startingGrabType, attachmentFlags);

                //Use rotatary to change volume? gabe

            }
            else if (isGrabEnding)
            {
                // Detach this object from the hand
                hand.DetachObject(gameObject);

                // Call this to undo HoverLock
                hand.HoverUnlock(interactable);

                // Restore position/rotation
                // transform.position = oldPosition;
                // transform.rotation = oldRotation;
            }
		}


		//-------------------------------------------------
		// Called when this GameObject becomes attached to the hand
		//-------------------------------------------------
		private void OnAttachedToHand( Hand hand )
		{

			//textMesh.text = "Attached to hand: " + hand.name;
			//attachTime = Time.time;
		}


		//-------------------------------------------------
		// Called when this GameObject is detached from the hand
		//-------------------------------------------------
		private void OnDetachedFromHand( Hand hand )
		{
			//textMesh.text = "Detached from hand: " + hand.name;
		}
       
        
        //-------------------------------------------------
        // Called every Update() while this GameObject is attached to the hand
        //-------------------------------------------------
        private void HandAttachedUpdate(Hand hand)
        {

            Debug.Log(Input.GetKeyDown(KeyCode.T));
            //Debug.Log("hep");
            
            //SteamVR_Input_Sources thumb = interactable.attachedToHand.handType;
            //Vector2 touchpad = a_Touchpad.GetAxis(thumb);

            //Vector2 touchpad = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);

            //Debug.Log(touchpad.x));
          
            //textMesh.text = "Attached to hand: " + hand.name + "\nAttached time: " + ( Time.time - attachTime ).ToString( "F2" );

        }

		//-------------------------------------------------
		// Called when this attached GameObject becomes the primary attached object
		//-------------------------------------------------
		private void OnHandFocusAcquired( Hand hand )
		{
		}


		//-------------------------------------------------
		// Called when another attached GameObject becomes the primary attached object
		//-------------------------------------------------
		private void OnHandFocusLost( Hand hand )
		{
		}
	}
}

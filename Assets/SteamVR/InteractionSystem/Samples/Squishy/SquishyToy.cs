﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem
{
    public class SquishyToy : MonoBehaviour
    {
        public AudioSource tickSource;// our sound source

        public Interactable interactable;
        public new SkinnedMeshRenderer renderer;

        public bool affectMaterial = true;

        [SteamVR_DefaultAction("Squeeze")]
        public SteamVR_Action_Single gripSqueeze;

        [SteamVR_DefaultAction("Squeeze")]
        public SteamVR_Action_Single pinchSqueeze;

        public bool startPlay = false; //gabe 

        private new Rigidbody rigidbody;

        private void Start()
        {

            if (tickSource == null)
                tickSource = GetComponent <AudioSource>();
            // line above added

            if (rigidbody == null)
                rigidbody = GetComponent<Rigidbody>();

            if (interactable == null)
                interactable = GetComponent<Interactable>();

            if (renderer == null)
                renderer = GetComponent<SkinnedMeshRenderer>();
        }

        private void Update()
        {
            float grip = 0;
            float pinch = 0;

            if (interactable.attachedToHand)
            {
                grip = gripSqueeze.GetAxis(interactable.attachedToHand.handType);
                pinch = pinchSqueeze.GetAxis(interactable.attachedToHand.handType);
                startPlay = true; //gabe
            }

            renderer.SetBlendShapeWeight(0, Mathf.Lerp(renderer.GetBlendShapeWeight(0), grip * 150, Time.deltaTime * 10));

            if (renderer.sharedMesh.blendShapeCount > 1) // make sure there's a pinch blend shape
                renderer.SetBlendShapeWeight(1, Mathf.Lerp(renderer.GetBlendShapeWeight(1), pinch * 200, Time.deltaTime * 10));

            if (affectMaterial)
            {
                renderer.material.SetFloat("_Deform", Mathf.Pow(grip * 1.5f, 0.5f));
                if (renderer.material.HasProperty("_PinchDeform"))
                {
                    renderer.material.SetFloat("_PinchDeform", Mathf.Pow(pinch * 2.0f, 0.5f));
                }
            }
        }

        void OnCollisionEnter (Collision collision)
        {

            if (startPlay == true)
                {
                tickSource.Play();
                tickSource.volume = Mathf.Clamp01(collision.relativeVelocity.magnitude / 20);
            }
            

        }
    }
}
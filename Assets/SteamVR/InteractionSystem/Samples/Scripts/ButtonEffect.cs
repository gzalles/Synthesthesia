//======= Copyright (c) Valve Corporation, All rights reserved. ===============

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


namespace Valve.VR.InteractionSystem.Sample
{
   

    public class ButtonEffect : MonoBehaviour
    {
        //AudioSource m_MyAudioSource;

        public void OnButtonDown(Hand fromHand)
        {
            //m_MyAudioSource = GetComponent<AudioSource>();
            ColorSelf(Color.cyan);
            fromHand.TriggerHapticPulse(1000);
            //m_MyAudioSource.time = 0;
            //m_MyAudioSource.Play();
        }

        public void OnButtonUp(Hand fromHand)
        {
            ColorSelf(Color.white);
            //if (m_MyAudioSource.time > 20f)
            //{
                //m_MyAudioSource.Stop();
            //}

        }

        private void ColorSelf(Color newColor)
        {
            Renderer[] renderers = this.GetComponentsInChildren<Renderer>();
            for (int rendererIndex = 0; rendererIndex < renderers.Length; rendererIndex++)
            {
                renderers[rendererIndex].material.color = newColor;
            }
        }
    }
}
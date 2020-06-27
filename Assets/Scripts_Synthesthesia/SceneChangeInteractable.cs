using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Valve.VR.InteractionSystem
{
    public class SceneChangeInteractable : MonoBehaviour
    {

        Text[] newText;
        string scene;
        // Start is called before the first frame update
        void Start()
        {
            newText = GetComponentsInChildren<Text>();
            scene = newText[0].text;
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log(scene);



        }

        private void HandHoverUpdate(Hand hand)
        {
            Debug.Log(scene);

            SceneManager.LoadScene(scene);

        }


    }
}
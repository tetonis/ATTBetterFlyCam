using MelonLoader;
using HarmonyLib;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

[assembly: MelonInfo(typeof(BetterFlyCam.Core), "BetterFlyCam", "0.9.2", "Tetonis")]
[assembly: MelonGame("Alta", "A Township Tale")]

//TS NOT AI TWIN!
namespace BetterFlyCam
{

    public class Core : MelonMod
    {
        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("BetterFlyCam initialized.");
            
            HarmonyInstance.PatchAll();
        }

        public static float sensitivity = 45f;
        public static float camSpeed = 10f;
        public static bool cinematicCamera = false;
        float scrollMultiplier = 0.1f;
        float sensitivityChangeSpeedMultiplier = 50f;

        //My child will put all code in the main function
        public override void OnUpdate()
        {
            //Toggle mouse lock so you can actually use menus
            if (InputHelper.GetKeyUp(Key.LeftAlt))
            {
                if (InputHelper.GetMouseLockState() == true)
                {
                    InputHelper.SetMouseLockState(false);
                }
                else
                {
                    InputHelper.SetMouseLockState(true);
                }
            }

            if (InputHelper.GetKeyUp(Key.C))
            {
                cinematicCamera = !cinematicCamera;
            }

            //Change mouse sens over time while key is held
            if (InputHelper.GetKey(Key.Equals))
            {
                sensitivity += Time.deltaTime * sensitivityChangeSpeedMultiplier;
            }
            if (InputHelper.GetKey(Key.Minus)){
                sensitivity -= Time.deltaTime * sensitivityChangeSpeedMultiplier;
            }

            //Let's not get too crazy
            sensitivity = Mathf.Clamp(sensitivity, 10f, 1024f);

            float mWheelDelta = Mouse.current.scroll.ReadValue().y;
            camSpeed += mWheelDelta * scrollMultiplier;
            camSpeed = Mathf.Clamp(camSpeed, 2f, 100f);
        }
    }
}

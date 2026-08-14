using MelonLoader;
using HarmonyLib;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

[assembly: MelonInfo(typeof(BetterFlyCam.Core), "BetterFlyCam", "0.9.3", "Tetonis")]
[assembly: MelonGame("Alta", "A Township Tale")]

//TS NOT AI TWIN!
namespace BetterFlyCam
{

    public class Core : MelonMod
    {
        static MelonPreferences_Category camCfg;
        static MelonPreferences_Entry<float> cfgCameraSensitivity;
        static MelonPreferences_Entry<float> cfgDefaultSpeed;
        static MelonPreferences_Entry<float> cfgScrollMultiplier;
        static MelonPreferences_Entry<float> cfgSensitivityAdjustmentSpeed;

        public static float sensitivity = 45f;
        public static float camSpeed = 10f;
        public static bool cinematicCamera = false;
        float scrollMultiplier = 0.1f;
        float sensitivityChangeSpeedMultiplier = 50f;

        public override void OnInitializeMelon()
        {
            camCfg = MelonPreferences.CreateCategory("BetterFlyCam", "Preferences");
            cfgCameraSensitivity = camCfg.CreateEntry("Camera Sensitivity", 45f, description: "The current camera sensitivity (default 45)");
            cfgDefaultSpeed = camCfg.CreateEntry("Default Speed", 10f, description: "The default camera move speed (default 10)");
            cfgScrollMultiplier = camCfg.CreateEntry("Scroll Wheel Sensitivity", 0.1f, description: "How fast the camera speed changes when you scroll (default 0.1)");
            cfgSensitivityAdjustmentSpeed = camCfg.CreateEntry("Camera Sensitivity Adjustment Speed", 50f, description: "How fast the camera sensitivity changes when you press +/- (default 50)");

            sensitivity = cfgCameraSensitivity.Value;
            camSpeed = cfgDefaultSpeed.Value;
            scrollMultiplier = cfgScrollMultiplier.Value;
            sensitivityChangeSpeedMultiplier = cfgSensitivityAdjustmentSpeed.Value;

            MelonPreferences.Save();

            LoggerInstance.Msg("BetterFlyCam initialized.");
            
            HarmonyInstance.PatchAll();
        }

        public override void OnDeinitializeMelon()
        {
            MelonPreferences.Save();
        }

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
            sensitivity = Mathf.Clamp(sensitivity, 5f, 1024f);

            float mWheelDelta = Mouse.current.scroll.ReadValue().y;
            camSpeed += mWheelDelta * scrollMultiplier;
            camSpeed = Mathf.Clamp(camSpeed, 2f, 100f);

            cfgCameraSensitivity.Value = sensitivity;
        }
    }
}

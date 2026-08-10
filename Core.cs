using MelonLoader;
using HarmonyLib;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

[assembly: MelonInfo(typeof(BetterFlyCam.Core), "BetterFlyCam", "0.9.1", "Tetonis")]

[assembly: MelonGame("Alta", "A Township Tale")]

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
        float sensitivityChangeSpeedMultiplier = 40f;

        public override void OnUpdate()
        {
            if (InputHelper.GetKeyUp(UnityEngine.InputSystem.Key.LeftAlt))
            {
                if (Cursor.lockState == CursorLockMode.Locked)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else if (Cursor.lockState == CursorLockMode.None || Cursor.lockState == CursorLockMode.Confined)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
            if (InputHelper.GetKeyUp(Key.C))
            {
                cinematicCamera = !cinematicCamera;
            }
            if (InputHelper.GetKey(Key.Equals))
            {
                sensitivity += Time.deltaTime * sensitivityChangeSpeedMultiplier;
            }
            if (InputHelper.GetKey(Key.Minus)){
                sensitivity -= Time.deltaTime * sensitivityChangeSpeedMultiplier;
            }
            sensitivity = Mathf.Clamp(sensitivity, 10f, 1024f);
            float mWheelDelta = Mouse.current.scroll.ReadValue().y;
            camSpeed += mWheelDelta * scrollMultiplier;
            camSpeed = Mathf.Clamp(camSpeed, 2f, 100f);
        }
    }
}

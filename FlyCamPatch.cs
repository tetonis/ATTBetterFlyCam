using HarmonyLib;
using MelonLoader;
using System.Runtime.CompilerServices;
using BetterFlyCam;
using UnityEngine;

namespace BetterFlyCam
{
    [HarmonyPatch(typeof(FlyCamPlayer), "CameraSensitivity")]
    [HarmonyPatch(MethodType.Getter)]
    public static class CursorLockPatch
    {
        static bool Prefix(ref float __result)
        {
            //Makes camera stop moving when cursor is free
            //Even works with Unity Explorer!
            if (InputHelper.GetMouseLockState() == false)
            {
                __result = 0f;
            }
            else
            {
                __result = Core.sensitivity;
            }
            return false;
        }
    }
    [HarmonyPatch(typeof(FlyCamPlayer), "NormalMoveSpeed")]
    [HarmonyPatch(MethodType.Getter)]
    public static class CameraSpeedPatch
    {
        static bool Prefix(ref float __result)
        {
            __result = Core.camSpeed;
            return false;
        }
        
    }

    [HarmonyPatch(typeof(FlyCamPlayer), "IsCinematicMovement")]
    [HarmonyPatch(MethodType.Getter)]
    public static class CinematicCameraPath
    {
        static bool Prefix(ref bool __result)
        {
            __result = Core.cinematicCamera;
            return false;
        }
    }
}

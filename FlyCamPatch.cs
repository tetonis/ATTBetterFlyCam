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
            if (Cursor.lockState != CursorLockMode.Locked)
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
}

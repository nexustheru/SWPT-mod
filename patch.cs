using UnityEngine;
using HarmonyLib;
using HarmonyLib.Tools;
using System;
using UnityEngine.UI;
//this is the patch file special stuff if u need something extra u do it like so

namespace tesplugin
{
    public class patch
    {
        #region Variables
        static Player play;
        static Inventory Inv;
        static Scene sce;
        static Storage sto;
        static UIInventory uiiinv;
        static InventorySlot invslot;
        static Global glob;
        static Mainframe mf;
        static UIMenu umen;
        #endregion
    
        #region Patches   
        [HarmonyPatch(typeof(Player), "Update")]
        static class Player_Update_Patch
        {
            static void Postfix(Player __instance)
            {
                play = __instance;
                Debug.Log("player update " + play.name);
              
            }
        }

        [HarmonyPatch(typeof(Inventory), "Refresh")]
        static class Invetory_Refresh_Patch
        {
            static void Postfix(Inventory __instance)
            {
                Inv = __instance;
                sto = Inv.storage;
                Debug.Log("invetory refresh " + Inv.storage.items.items.Count.ToString());
            }
        }

        [HarmonyPatch(typeof(Scene), "Start")]
        static class Scene_Start_Patch
        {
            static void Postfix(Scene __instance)
            {
                sce = __instance;
                Debug.Log("Scene started ");
                //GameObject unityyplay =  GameObject.Find("Player");
                // print(unityyplay.name);

            }
        }

        [HarmonyPatch(typeof(InventorySlot), "Click")]
        static class Invetory_Click_Patch
        {
            static void Postfix(InventorySlot __instance)
            {
                invslot = __instance;
                Debug.Log("Inv clicked " + Global.code.crystals.ToString());
                
            }
        }

        [HarmonyPatch(typeof(Global), "Awake")]
        static class Global_acces_Patch
        {
            static void Postfix(Global __instance)
            {
                glob = __instance;
                Debug.Log("Global setup ");
            }
        }

        [HarmonyPatch(typeof(UIMenu), "Open")]
        static class Uimenu_Patch
        {
            static void Postfix(UIMenu __instance)
            {
                umen = __instance;
                Debug.Log("Menu setup");
            }
        }

        [HarmonyPatch(typeof(UIInventory), "Open")]
        static class uiinv_Patch
        {
            static void Postfix(UIInventory __instance)
            {
                uiiinv = __instance;
                Debug.Log("uiinvetory setup");
            }
        }

        [HarmonyPatch(typeof(Mainframe), "Awake")]
        static class gmain_Patch
        {
            static void Postfix(Mainframe __instance)
            {
                mf = __instance;

                Debug.Log("Mainfram setup");
            }
        }
        #endregion
        }
    #region passtomenu
    
    #endregion
}

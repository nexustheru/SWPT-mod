using UnityEngine;
using BepInEx;
using HarmonyLib;
using System.Diagnostics;
using System.Reflection;
using HarmonyLib.Tools;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
//simplicity at its finest works with newest she will punish em all game,v 800,900, but i suppose it same for most unity games.its not very complicated
namespace MyFirstPlugin
{
    [BepInPlugin("org.bepinex.plugins.exampleplugin", "MyFirstPlugin", "1.0.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        #region Variables
        private Harmony _harmonyInstance;
        tesplugin.MainUi tm = new tesplugin.MainUi();
        #endregion
       
        #region Musthave
        private void debuglog(string str = "")
        {
            Logger.LogInfo(str);
        }
        private void Awake()
        {
            debuglog("MyFirstPlugin is loaded!");

            HarmonyFileLog.Enabled = true;         
            this.transform.parent = null;
            DontDestroyOnLoad(this);
             tm.FetchPatches();
            _harmonyInstance = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
       
            //var mOriginal = AccessTools.Method(typeof(SomeGameClass), "DoSomething");
        }
        protected void OnGUI()
        {
            tm.OnGUI();
             
        }
        protected void Update()
        {
            tm.Update();
        }
        private void WindowFunction(int windowID)
        { 
            tm.WindowFunction(windowID);
        }
        #endregion
    }
}

//            playerInvetory = GameObject.FindObjectOfType<Inventory>();
//            playerModel = GameObject.FindObjectOfType<Player>();
//            playerScene = GameObject.FindObjectOfType<Scene>();

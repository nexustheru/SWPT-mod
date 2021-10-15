using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
//mainframe.code
//global
//scene
//uimenu
//is ure friend, it give u acces to almost entire game,special stuff u need to use harmony, example can be found on patch file, this is however how u add a pop up menu to the game
namespace tesplugin
{
    class MainUi : MonoBehaviour
    {
        private Rect UI = new Rect(20, 20, 400, 400);
        private static Vector2 scrollPosition = Vector2.zero;
        private GUIStyle logTextStyle = new GUIStyle();
        private tesplugin.patch paatch; 
        public void FetchPatches()
        {
            paatch = new patch();
            print("patches completed");
        }

        public void OnGUI()
        {

            UI = GUILayout.Window(589, UI, WindowFunction, "Some Tools");
        }

        public void Update()
        {

        }

        public void WindowFunction(int windowID)
        {
            if (GUILayout.Button("Stack Inventory"))
            {
                print("stack click");
            }
            if (GUILayout.Button("Add Crystals"))
            {
                Global.code.AddCrystals(3000);
            }

            Event e = Event.current;

            GUI.DragWindow();
        }

    }
}

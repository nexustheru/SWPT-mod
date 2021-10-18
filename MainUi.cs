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
        GameObject myglob = new GameObject();
        GameObject unityyplay = new GameObject();
        GameObject stoo = new GameObject();
        public void FetchPatches()
        {
            paatch = new patch();
            print("patches completed");
            setupvariables();
        }
       
        public void setupvariables()
        {
          
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
               
                myglob = GameObject.Find("Global J (1)");
                unityyplay = GameObject.Find("Player");
                stoo = GameObject.Find("Player Storage");
             
                for (int i = 0; i < stoo.transform.childCount; i++)
                {
                    Transform chiild = stoo.transform.GetChild(i);
                    
                    if (chiild.name== "Minor Health Potion")
                    {
                        Global.code.AddItemToHomeStorage(chiild);
                        Global.code.uiInventory.curCustomization.RemoveItem(stoo.transform.GetChild(i));                    
                        print("Deleting " + chiild.name);
                        chiild.GetComponent<Item>().occupliedSlots.Clear();
                    }
                    else if(chiild.name == "Mana Potion")
                    {
                        Global.code.AddItemToHomeStorage(chiild);
                        Global.code.uiInventory.curCustomization.RemoveItem(stoo.transform.GetChild(i));
                        print("Deleting " + chiild.name);
                        chiild.GetComponent<Item>().occupliedSlots.Clear();
                    }
                    else
                    {
                        print("Found " + chiild.name);
                    }
                   
                }
                for (int ip = 0; ip < myglob.transform.childCount; ip++)
                {
                    print("children glob 1   " + myglob.transform.GetChild(ip).name);
                }


                print("Player has");
            }
            if (GUILayout.Button("Add Crystals"))
            {
                Global.code.AddCrystals(9000);
            }
            if (GUILayout.Button("Add Exp"))
            {
                Global.code.AddExpToAllUnits(9000);
            }
            if (GUILayout.Button("Add Gold"))
            {
                Global.code.AddGold(9000);
            }
          
            Event e = Event.current;

            GUI.DragWindow();
        }

    }
}

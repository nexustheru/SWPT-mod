using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
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
        private string assetPath = "E://SteamLibrary//steamapps//common//She Will Punish Them//BepInEx//plugins//Textures//test.png";
        private string assetPath1 = "E://SteamLibrary//steamapps//common//She Will Punish Them//She Will Punish Them_Data//test.png";
        private string assetPath2 = "E://SteamLibrary//steamapps//common//She Will Punish Them//She Will Punish Them_Data//Resources//test.png";
        //private tesplugin.patch paatch;
        GameObject myglob = new GameObject();
        GameObject unityyplay = new GameObject();
        GameObject stoo = new GameObject();
        private string pdir = Directory.GetCurrentDirectory().Replace("\\", "//");
        public void FetchPatches()
        {
            //paatch = new patch();
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
                Player play = null;
                CharacterCustomization playcust = null;
                ID playid = null;
                CharacterCustomization custom = null;
                Companion comp = null;
                ID ids = null;

            
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
            if (GUILayout.Button("Resurect All"))
            {
                
                for (int i = 0; i < Scene.code.transform.childCount; i++)
                {
                    Transform it = Scene.code.transform.GetChild(i);
                    if (it.name == "Companion Base")
                    {
                        custom = it.GetComponent<CharacterCustomization>();
                        comp = it.GetComponent<Companion>();
                        ids = it.GetComponent<ID>();
                        if(ids.health ==0)
                        {
                        ids.health = custom._ID.maxHealth;
                        ids.mana = custom._ID.maxMana;
                        custom.anim.enabled = true;
                        custom.GetComponent<Rigidbody>().isKinematic = false;
                        if (custom.GetComponent<Collider>())
                        {
                            custom.GetComponent<Collider>().enabled = true;
                        }
                        foreach (Transform transform in custom.bones)
                        {
                            if (transform)
                            {
                                transform.GetComponent<Rigidbody>().isKinematic = true;
                                transform.GetComponent<Rigidbody>().useGravity = false;
                                transform.GetComponent<Collider>().enabled = false;
                            }
                        }

                        if (custom.GetComponent<MapIcon>())
                        {
                            DestroyImmediate(custom.gameObject.GetComponent<MapIcon>());
                        }
                        custom.gameObject.AddComponent<MapIcon>();
                        custom.GetComponent<MapIcon>().healthBarColor = Color.green;
                        custom.GetComponent<MapIcon>().id = custom.GetComponent<ID>();
                        custom.GetComponent<MapIcon>().posBias = new Vector3(0f, 2.3f, 0f);
                        custom.GetComponent<MapIcon>().visibleRange = 300f;
                        }
                       
                        //

                    }
                    if (it.name == "Player")
                    {
                        play = it.GetComponent<Player>();
                        playcust = it.GetComponent<CharacterCustomization>();
                        playid = it.GetComponent<ID>();
                        if (playid.health <= 5)
                        {
                            playid.health = playcust._ID.maxHealth;
                            playid.mana = playcust._ID.maxMana;
                            //
                        }
                    }
                }
            }
            if (GUILayout.Button("Heal Party"))
            {
                Global.code.AddGold(9000);
            }
            if (GUILayout.Button("Test floating text"))
            {
              
                DoFloatingText();
            }
            if (GUILayout.Button("Test Load asset 3d"))
            {
                GameObject gol11 = new GameObject();
                loadasset script = gol11.AddComponent<loadasset>();
                print("asset inported test");

            }
            if (GUILayout.Button("custom 3d test"))
            {
                GameObject gol = new GameObject();
                Meshimport mi = gol.AddComponent<Meshimport>();
              
                print("custom Import?");
            }
            if (GUILayout.Button("triangle test"))
            {
                GameObject gol1=new GameObject();
                CreateTriangleScript scrip1t = gol1.AddComponent<CreateTriangleScript>();
            }
            if (GUILayout.Button("custom tex test"))
            {
                GameObject gol2 = new GameObject();
                customTex script2 = gol2.AddComponent<customTex>();
            }
            if (GUILayout.Button("custom tex test1"))
            {
                test0();
            }

            Event e = Event.current;
            
            GUI.DragWindow();
        }


        //[HarmonyPatch(typeof(RM), "LoadResources")]
        //static class LoadResources_Patch
        //{           
        //    static void Postfix(RM __instance)
        //    {
        //        GameObject gol = new GameObject();
        //        Meshimport mi = gol.AddComponent<Meshimport>();
        //        __instance.allItems.AddItem(mi.transform);
        //        DontDestroyOnLoad(gol);
        //    }
        //}
      public static void DoFloatingText()
        {
            floatingtext fl = Scene.code.gameObject.AddComponent(typeof(floatingtext)) as floatingtext;
            fl.enabled = true;
            fl.setup();
        }

        public void test0()
        {
            Debug.Log("custom scene management");
            Texture2D tex = (Texture2D)Resources.Load(assetPath2, typeof(Texture2D));
            tex.name = "linlooo";
            tex.Apply();

            var allGOs = FindObjectsOfType<GameObject>();
            foreach (GameObject go in allGOs)
            {
                var mr = go.GetComponent<MeshRenderer>();
                if (mr)
                {
                   Material[] materialss = mr.materials;
                   materialss[0].mainTexture = tex;
                    // mr.material.mainTexture = tex;
                }
            }
            Debug.Log("custom scene management done");

        }
    }
}

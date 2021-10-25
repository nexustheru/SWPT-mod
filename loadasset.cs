using System;
using System.IO;
using UnityEngine;

namespace tesplugin
{
    class loadasset : MonoBehaviour
    {
        private string pdir = Directory.GetCurrentDirectory().Replace("\\", "//");
        GameObject assetModel;
        public void loadfile()
        {
            //var myLoadedAssetBundle = AssetBundle.LoadFromFile(Application.dataPath + "/BepInEx/plugins/StreamingAssets/AssetBundles/femalebasemesh");//works
            //var myLoadedAssetBundle2 = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath + "/AssetBundles/", "femalebasemesh"));//works
            var myLoadedAssetBundle = AssetBundle.LoadFromFile(pdir + "/BepInEx/plugins/StreamingAssets/AssetBundles/ball");//works

            if (myLoadedAssetBundle == null)
            {
                Debug.Log("Failed to load custom assets");
                return;
            }
            else
            {
                assetModel = Instantiate(myLoadedAssetBundle.LoadAsset<GameObject>("Ball"));
                //Instantiate(prefab, GameObject.Find("Player").transform,true);
            
                print("succesfully loaded the custom assets");
            }

        }
        private void Start()
        {
           loadfile();
           init();

        }
        private void Update()
        {

        }
        public void init()
        {
            assetModel.transform.localPosition = Vector3.zero;
            assetModel.SetActive(true);
            assetModel.AddComponent<MapIcon>();
            assetModel.AddComponent<Rigidbody>();
            assetModel.AddComponent<SphereCollider>();
        }
    }
}

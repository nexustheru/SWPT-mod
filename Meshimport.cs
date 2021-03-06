using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModelImporter;
using System.IO;

public class Meshimport : MonoBehaviour
{
    private string pdir = Directory.GetCurrentDirectory().Replace("\\", "//");
    private string pdirr = "E://SteamLibrary//steamapps//common//She Will Punish Them//BepInEx//plugins//Meshes";
    private List<Vector3> vertexes;
    private List<List<int>> triangles;
    private List<Vector3> normals;
    private List<List<Vector2>> uv;
    private List<Color> color;
    private List<Material> material;
    private List<string> models;
    private DirectoryInfo dinf;
    private string fnameshort = "";
    // Start is called before the first frame update
    //export fbx with unwrap,uv,material,texture(baked texture) fbx options
    private void fillvertexes(FBXModelPtr m)
    {
        for (int i = 0; i < m.GetVertexCount(); ++i)
        {
            var v = m.GetVertex(i);
            vertexes.Add(new Vector3(v.x, v.y, v.z));
        }
        Debug.Log("fbx vertex");
    }
    private void fillnormal(FBXModelPtr m)
    {
        for (int i = 0; i < m.GetNormalCount(); ++i)
        {
            var v = m.GetNormal(i);
            normals.Add(new Vector3(v.x, v.y, v.z));
        }
        Debug.Log("fbx normal");
    }
    private void filluv(FBXModelPtr m)
    {
        for (int iUVLayer = 0; iUVLayer < m.GetUVLayerCount(); ++iUVLayer)
        {
            List<Vector2> uvss = new List<Vector2>();
            for (int iUV = 0; iUV < m.GetUVCount(iUVLayer); ++iUV)
            {
                UnityEngine.Vector2 vec = new Vector2(m.GetUV(iUVLayer, iUV).x, m.GetUV(iUVLayer, iUV).y);
                uvss.Add(vec);
            }
            uv.Add(uvss);
        }
        Debug.Log("fbx uv");
    }
    private void fillcolor(FBXModelPtr m)
    {
        for (int i = 0; i < m.GetColorCount(); ++i)
        {
            Color col = new Color(m.GetColor(i).r, m.GetColor(i).g, m.GetColor(i).b, m.GetColor(i).a);
            color.Add(col);
        }
        Debug.Log("fbx color");
    }
    private void filltriangles(FBXModelPtr m)
    {

        for (int i = 0; i < m.GetMaterialCount(); ++i)
        {
            int materialPolygonCount = m.GetMaterialPolygonCount(i);
        }

        for (int iMaterial = 0; iMaterial < m.GetMaterialCount(); ++iMaterial)
        {
            List<int> triangle = new List<int>();
            for (int i = 0; i < m.GetIndiceCount(iMaterial); i += 3)
            {
                triangle.Add(m.GetIndex(iMaterial, i));
                triangle.Add(m.GetIndex(iMaterial, i + 2));
                triangle.Add(m.GetIndex(iMaterial, i + 1));
            }
            triangles.Add(triangle);
        }
        Debug.Log("fbx triangles");
    }
    private void fillmaterial(FBXModelPtr m)
    {
        for (int i = 0; i < m.GetMaterialCount(); ++i)
        {
            var temp = m.GetMaterial(i);
            Material newmat = new Material(Shader.Find("Standard"));
            newmat.name = temp.GetName();
            if (temp.Exist("Diffuse"))
            {
                Color newcol = new Color(temp.GetVector3("Diffuse").x, temp.GetVector3("Diffuse").y, temp.GetVector3("Diffuse").z);
                newmat.SetColor("_Color", newcol);
            }
            if (temp.Exist("DiffuseColor"))
            {
                var texname = Path.GetFileNameWithoutExtension(temp.GetString("DiffuseColor"));
                var re = scannTexture(newmat.name, texname);
                newmat.mainTexture = scannTexture(newmat.name, texname);
            }
            if (temp.Exist("NormalMap"))
            {
                Color newcol = new Color(temp.GetVector3("Diffuse").x, temp.GetVector3("Diffuse").y, temp.GetVector3("Diffuse").z);
                newmat.SetColor("_Color", newcol);
            }
            material.Add(newmat);
        }
        Debug.Log("fbx material");
    }
    private void scannFolder()
    {
        //string path = pdir + "/BepInEx/plugins/Meshes";
        dinf = new DirectoryInfo(pdirr);
        foreach (var f in dinf.GetFiles())
        {
            if (f.FullName.Contains(".fbx"))
            {
                models.Add(f.FullName);
            }
        }
    }
    private Texture scannTexture(string MatName, string TexName)
    {
        var subDirectories = dinf.GetDirectories("*.*", SearchOption.AllDirectories);
        Texture2D textt = new Texture2D(0, 0);
        textt.name = TexName;
        if (subDirectories.Length > 0)
        {
            for (int te = 0; te < subDirectories.Length; ++te)
            {
                if (subDirectories.GetValue(te).ToString().Contains(fnameshort))
                {
                    var texpath = subDirectories.GetValue(te).ToString() + "\\";
                    foreach (var f in Directory.GetFiles(texpath))
                    {
                        var bytes = File.ReadAllBytes(f);
                        textt.LoadImage(bytes);
                    }
                }

            }
        }
        textt.Apply();
        return textt;
    }

    void Awake()
    {
        vertexes = new List<Vector3>();
        triangles = new List<List<int>>();
        normals = new List<Vector3>();
        uv = new List<List<Vector2>>();
        models = new List<string>();
        color = new List<Color>();
        material = new List<Material>();
        scannFolder();
    }
    void Start()
    {
        foreach (var s in models)
        {
            fnameshort = Path.GetFileName(s).Replace(".fbx", "");
            FBXScenePtr xscene = ModelImporter.Importer.ImportFbxScene(s);
            FBXModelPtr model = xscene.GetModel();
            for (int i = 0; i < model.GetChildCount(); ++i)
            {
                FBXModelPtr xmesh = model.GetChild(i);
                fillvertexes(xmesh);
                filltriangles(xmesh);
                fillnormal(xmesh);
                filluv(xmesh);
                fillcolor(xmesh);
                fillmaterial(xmesh);
                buildObject();
            }
        }
     
        
    }

    void buildObject()
    {
        Mesh newMesh = new Mesh();
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        var mer = GetComponent<MeshRenderer>();
        mer.material = material[0];
        newMesh.vertices = vertexes.ToArray();
        newMesh.subMeshCount = triangles.Count;
        for (int i = 0; i < triangles.Count; ++i)
        {
            newMesh.SetIndices(triangles[i].ToArray(), MeshTopology.Triangles, i);
        }
        newMesh.SetColors(color);
        if (uv.Count > 0)
        {
            for (int i = 0; i < uv.Count; ++i)
            {
                newMesh.SetUVs(i, uv[i]);

            }
        }
        newMesh.RecalculateBounds();
        newMesh.RecalculateNormals();
        newMesh.Optimize();
        newMesh.name = "MyMesh";
        GetComponent<MeshFilter>().mesh = newMesh;
        gameObject.AddComponent<MapIcon>();
        gameObject.AddComponent<Interaction>();
        transform.position = new Vector3(6.3417f, 4.82f, 73.3733f);
    }
    // Update is called once per frame
    void Update()
    {

    }
}

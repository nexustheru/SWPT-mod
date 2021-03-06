using UnityEngine;
using System.Collections;

public class CreateTriangleScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        //Create new mesh - this will hold our triangle
        Mesh newMesh = new Mesh();

        //This makes the mesh available to other components
        gameObject.AddComponent<MeshFilter>();

        //This makes it visible
        gameObject.AddComponent<MeshRenderer>();

        //Define vertices
        Vector3 topLeftFront = new Vector3(1, 1, 0.5f);
        Vector3 topLeftBack = new Vector3(1, 1, -0.5f);
        Vector3 bottomLeftFront = new Vector3(1, 0, 0.5f);
        Vector3 bottomLeftBack = new Vector3(1, 0, -0.5f);
        Vector3 bottomRightFront = new Vector3(0, 0, 0.5f);
        Vector3 bottomRightBack = new Vector3(0, 0, -0.5f);

        //Assign vertices
        Vector3[] verts = new Vector3[6];
        verts[0] = topLeftFront;
        verts[1] = topLeftBack;
        verts[2] = bottomLeftFront;
        verts[3] = bottomLeftBack;
        verts[4] = bottomRightFront;
        verts[5] = bottomRightBack;

        //Create UVs
        Vector2[] uvs = new Vector2[newMesh.vertices.Length];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(newMesh.vertices[i].x, newMesh.vertices[i].z);
        }

        //Create triangles

        // x+ face 1, 0, 3, 3, 0, 2
        // y- face 3, 2, 5, 5, 2, 4
        // x- face 0, 1, 4, 4, 1, 5
        // z+ face 0, 4, 2
        // z- face 1, 3, 5

        int[] tris = new int[18];

        // z+ face
        tris[0] = 4;
        tris[1] = 2;
        tris[2] = 0;

        // x- face
        tris[3] = 0;
        tris[4] = 1;
        tris[5] = 4;
        tris[6] = 4;
        tris[7] = 1;
        tris[8] = 5;

        // z- face
        tris[9] = 1;
        tris[10] = 3;
        tris[11] = 5;

        // x+ face
        tris[12] = 1;
        tris[13] = 0;
        tris[14] = 3;
        tris[15] = 3;
        tris[16] = 0;
        tris[17] = 2;

        // --

        //Assign vertices
        newMesh.vertices = verts;

        //Assign uvs
        newMesh.uv = uvs;

        //Assign triangles
        newMesh.triangles = tris;


        //Recalculate bounds before normals
        newMesh.RecalculateBounds();

        //Recalculate normals
        newMesh.RecalculateNormals();

        //Optimize
        newMesh.Optimize();

        //Name
        newMesh.name = "MyMesh";

        //Assign mesh to filter
        GetComponent<MeshFilter>().mesh = newMesh;
        transform.position = new Vector3(6.3417f, 4.82f, 73.3733f);
    }
}

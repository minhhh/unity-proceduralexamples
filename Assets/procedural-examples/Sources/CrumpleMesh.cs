// This script is placed in public domain. The author takes no responsibility for any possible harm.
using UnityEngine;

public class CrumpleMesh : MonoBehaviour
{
    float scale = 1.0f;
    float speed = 1.0f;
    bool recalculateNormals = false;

    private Vector3[] baseVertices;
    Perlin noise;

    void Start ()
    {
        noise = new Perlin ();
    }

    void Update ()
    {
        var mesh = GetComponent <MeshFilter> ().mesh;

        if (baseVertices == null)
            baseVertices = mesh.vertices;

        var vertices = new Vector3[baseVertices.Length];

        var timex = Time.time * speed + 0.1365143;
        var timey = Time.time * speed + 1.21688;
        var timez = Time.time * speed + 2.5564;
        for (var i = 0; i < vertices.Length; i++) {
            var vertex = baseVertices [i];

            vertex.x += noise.Noise ((float)timex + vertex.x, (float)timex + vertex.y, (float)timex + vertex.z) * scale;
            vertex.y += noise.Noise ((float)timey + vertex.x, (float)timey + vertex.y, (float)timey + vertex.z) * scale;
            vertex.z += noise.Noise ((float)timez + vertex.x, (float)timez + vertex.y, (float)timez + vertex.z) * scale;

            vertices [i] = vertex;
        }

        mesh.vertices = vertices;

        if (recalculateNormals)
            mesh.RecalculateNormals ();
        mesh.RecalculateBounds ();
    }
}

//using UnityEngine;

//public static class MeshVolumeCalculator
//{
//    /// <summary>
//    /// needs meshCollider.sharedMesh
//    /// needs transform.localScale
//    /// </summary>
//    /// <param name="mesh"></param>
//    /// <param name="scale"></param>
//    /// <returns></returns>
//    public static float CalculateMeshVolume(Mesh mesh, Vector3 scale)
//    {
//        float volume = 0;
//        Vector3[] vertices = mesh.vertices;
//        int[] triangles = mesh.triangles;

//        for (int i = 0; i < triangles.Length; i += 3)
//        {
//            Vector3 v1 = Vector3.Scale(vertices[triangles[i]], scale);
//            Vector3 v2 = Vector3.Scale(vertices[triangles[i + 1]], scale);
//            Vector3 v3 = Vector3.Scale(vertices[triangles[i + 2]], scale);

//            volume += SignedVolumeOfTriangle(v1, v2, v3);
//        }

//        return Mathf.Abs(volume);
//    }

//    static float SignedVolumeOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
//    {
//        return Vector3.Dot(p1, Vector3.Cross(p2, p3)) / 6.0f;
//    }
//}

using UnityEngine;

public static class MeshVolumeCalculator
{
    public static float CalculateMeshVolume(MeshCollider meshCollider)
    {
        Mesh mesh = meshCollider.sharedMesh;
        if (mesh == null) return 0f;

        float volume = 0;
        Matrix4x4 localToWorld = meshCollider.transform.localToWorldMatrix;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        for (int i = 0; i < triangles.Length; i += 3)
        {
            Vector3 v1 = localToWorld.MultiplyPoint3x4(vertices[triangles[i]]);
            Vector3 v2 = localToWorld.MultiplyPoint3x4(vertices[triangles[i + 1]]);
            Vector3 v3 = localToWorld.MultiplyPoint3x4(vertices[triangles[i + 2]]);

            volume += SignedVolumeOfTriangle(v1, v2, v3);
        }

        return Mathf.Abs(volume);
    }

    static float SignedVolumeOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return Vector3.Dot(p1, Vector3.Cross(p2, p3)) / 6.0f;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		GameObject[] gobs = UnityEngine.Object.FindObjectsOfType<GameObject>();
		foreach (GameObject go in gobs)
        {
			if (go.activeInHierarchy)
            {
				MeshFilter mf = go.GetComponent<MeshFilter>();
				if(mf)
                {
					Mesh mesh = mf.sharedMesh;
					getMesh(mesh, go.transform);
				}
				
			}
				
		}
			
		
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void getMesh(Mesh m, Transform t)
	{
		Vector3 s = t.localScale;
		Vector3 p = t.localPosition;
		Quaternion r = t.localRotation;


		int numVertices = 0;
	
		if (!m)
		{
			return;
		}


		foreach (Vector3 vv in m.vertices)
		{
			Vector3 v = t.TransformPoint(vv);
			numVertices++;
		}
		/*
		foreach (Vector3 nn in m.normals)
		{
			Vector3 v = r * nn;
			sb.Append(string.Format("vn {0} {1} {2}\n", -v.x, -v.y, v.z));
		}
		foreach (Vector3 v in m.uv)
		{
			sb.Append(string.Format("vt {0} {1}\n", v.x, v.y));
		}*/

		string path = "Assets/test.txt";
		//Write some text to the test.txt file
		StreamWriter writer = new StreamWriter(path, true);
		
		string str = "";
		for (int material = 0; material < m.subMeshCount; material++)
		{
			int[] triangles = m.GetTriangles(material);
			for (int i = 0; i < triangles.Length; i += 3)
            {
				Vector3 v1 = t.TransformPoint(m.vertices[triangles[i]]);
				v1.x *= 100;
				v1.y *= -100;
				v1.z *= -100;
				Vector3 uv1 = m.uv[triangles[i]];

				Vector3 v2 = t.TransformPoint(m.vertices[triangles[i+1]]);
				v2.x *= 100;
				v2.y *= -100;
				v2.z *= -100;
				Vector3 uv2 = m.uv[triangles[i + 1]];

				Vector3 v3 = t.TransformPoint(m.vertices[triangles[i+2]]);
				v3.x *= 100;
				v3.y *= -100;
				v3.z *= -100;
				Vector3 uv3 = m.uv[triangles[i + 2]];

				str += "new Polygon(new Point3d("+ v1.x + " * scale.x, " + v1.y + " * scale.y, " + v1.z + " * scale.z, "+ uv1.x+ ", " + uv1.y + "), new Point3d(" + v2.x + " * scale.x, " + v2.y + " * scale.y, " + v2.z + " * scale.z, " + uv2.x + ", " + uv2.y + "), new Point3d(" + v3.x + " * scale.x, " + v3.y + " * scale.y, " + v3.z + " * scale.z, " + uv3.x + ", " + uv3.y + "), bd),";

			}

			print(str);


		}

		writer.WriteLine(str);
		writer.Close();


	}
}


using UnityEngine;
//using UnityEditor;
using System;
using System.Collections;
using System.IO;

public class BlendingPlaneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Load by default
		LoadData ();
	}
	
	// Update is called once per frame
	void Update () {
		GameObject blend0 = GameObject.FindGameObjectWithTag ("BlendPlane0");
		GameObject blend1 = GameObject.FindGameObjectWithTag ("BlendPlane1");
		if (Input.GetKey (KeyCode.B)) {
			if (Input.GetKeyDown (KeyCode.E)) {
				blend0.transform.Translate (0.0f, 0.0f, -0.00002f);
			}
			if (Input.GetKeyDown (KeyCode.D)) {
				blend0.transform.Translate (0.0f, 0.0f, 0.00002f);
			}
			if (Input.GetKeyDown (KeyCode.R)) {
				blend0.transform.localScale = blend0.transform.localScale + new Vector3 (0.0f, 0.0f, 0.00001f);
			}
			if (Input.GetKeyDown (KeyCode.F)) {
				blend0.transform.localScale = blend0.transform.localScale - new Vector3 (0.0f, 0.0f, 0.00001f);
			}
			if (Input.GetKeyDown (KeyCode.T)) {
				blend0.GetComponent<Renderer> ().enabled = !blend0.GetComponent<Renderer> ().enabled;
			}
			
			if (Input.GetKeyDown (KeyCode.I)) {
				blend1.transform.Translate (0.0f, 0.0f, -0.00002f);
			}
			if (Input.GetKeyDown (KeyCode.K)) {
				blend1.transform.Translate (0.0f, 0.0f, 0.00002f);
			}
			if (Input.GetKeyDown (KeyCode.U)) {
				blend1.transform.localScale = blend1.transform.localScale + new Vector3 (0.0f, 0.0f, 0.00001f);
			}
			if (Input.GetKeyDown (KeyCode.J)) {
				blend1.transform.localScale = blend1.transform.localScale - new Vector3 (0.0f, 0.0f, 0.00001f);
			}
			if (Input.GetKeyDown (KeyCode.Y)) {
				blend1.GetComponent<Renderer> ().enabled = !blend1.GetComponent<Renderer> ().enabled;
			}
		}

		if (Input.GetKeyDown (KeyCode.S)) {
			if(File.Exists ("blendingConfig.txt"))
			{
				//FileUtil.DeleteFileOrDirectory("blendingConfig.txt");
			}

			StreamWriter sw = File.CreateText ("blendingConfig.txt");
			sw.WriteLine("BlendPlane0P;{0}", blend0.transform.position.z);
			sw.WriteLine("BlendPlane0W;{0}", blend0.transform.localScale.z);
			sw.WriteLine("BlendPlane1P;{0}", blend1.transform.position.z);
			sw.WriteLine("BlendPlane1W;{0}", blend1.transform.localScale.z);
			sw.Close ();
		}

		if (Input.GetKeyDown (KeyCode.L)) {
			LoadData ();
		}

	}

	void LoadData()
	{
		GameObject blend0 = GameObject.FindGameObjectWithTag ("BlendPlane0");
		GameObject blend1 = GameObject.FindGameObjectWithTag ("BlendPlane1");

		if(File.Exists("blendingConfig.txt")){
			StreamReader sr = File.OpenText("blendingConfig.txt");

			string line = sr.ReadLine();
			blend0.transform.position = new Vector3(0.0f, 7.680f, GetLineValue("BlendPlane0P", line));
			line = sr.ReadLine();
			blend0.transform.localScale = new Vector3(0.0022f, 1.0f, GetLineValue("BlendPlane0W", line));
			line = sr.ReadLine();
			blend1.transform.position = new Vector3(0.0f, 7.680f, GetLineValue("BlendPlane1P", line));
			line = sr.ReadLine();
			blend1.transform.localScale = new Vector3(0.0022f, 1.0f, GetLineValue("BlendPlane1W", line));

		} else {
			Debug.Log("Could not Open the file: blendingConfig.txt");
		}
	}

	float GetLineValue(string variableName, string line)
	{
		if (line.StartsWith (variableName)) {
			char[] delimiterChars = {';'};
			string[] words = line.Split (delimiterChars);
			return Convert.ToSingle (words [1].ToString ());
		} else {
			return 0.0f;
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class PropManager : MessageBehaviour {


	private float coord0;
	private float coord1;
	private float coord2;

	private GameObject new_prop;
	
	private string prev_prop;

	private bool prev_prop1 = false;
	private bool prev_prop2 = false;
	private bool prev_prop3 = false;
	private bool prev_prop4 = false;
	

	private SkinnedMeshRenderer[] childRenderer;
	
	private Vector3 source1;
	private Vector3 source2;
	private Vector3 source3;
	private Vector3 source4;

	private List<string> propNames1 = new List<string>();
	private List<string> propNames2 = new List<string>();
	private List<string> propNames3 = new List<string>();
	private List<string> propNames4 = new List<string>();
	
	public bool randomPositions;

	private int i;
	private int j;
	private int k;
	private int p;
	

	private float timer1 = 0;
	private float timer2 = 0;
	private float timer3 = 0;
	private float timer4 = 0;
	
	private int timeNewProp = 10;
	
	private bool newProp = false;

	private Vector3 randomPos1;
	private Vector3 randomPos2;
	private Vector3 randomPos3;
	private Vector3 randomPos4;

	// Door's key logic
	public bool hasToCreateKeys;

	private bool hasToCreateKeyHead;
	private bool hasToCreateKeyBody;

	private bool createKeyHead;
	private bool createKeyBody;

	// Use this for initialization
	protected override void OnStart () 
	{
	
		source1 = new Vector3(-0.7f, 0.0f, -1.8f);
		source2 = new Vector3(1.9f, 0.0f, -0.7f);
		source3 = new Vector3(-1.85f, 0.0f, 0.5f);
		source4 = new Vector3(0.8f, 0.0f, 1.8f);

		randomPos1 = new Vector3(1.8f, 0.0f, -1.5f);
		randomPos2 = new Vector3(1.5f, 0.0f, 1.7f);
		randomPos3 = new Vector3(1.3f, 0.0f, 1.9f);
		randomPos4 = new Vector3(2.2f, 0.0f, 1.2f);

		LoadNamesLists();

		CreatePropSource1();
		CreatePropSource2();
		CreatePropSource3();
		CreatePropSource4();

		createKeyHead = false;
		createKeyBody = false;

		// If has to create keys is true, means this game will have to create the keys for the door
		if (hasToCreateKeys) {
			hasToCreateKeyHead = true;
			hasToCreateKeyBody = true;
		} else {
			hasToCreateKeyHead = false;
			hasToCreateKeyBody = false;
		}
		

		Messenger.RegisterListener(new Listener("prop_eliminated", gameObject, "NewProp"));	

	}

	void LoadNamesLists()
	{	
		
		propNames1.Add("Larva");
		propNames1.Add("Pez");
		propNames1.Add("Rana");
		propNames1.Add("Cabezones");
		
		propNames2.Add("Animalito_hielo");
		propNames2.Add("Cristal_hielo");
		propNames2.Add("Frailecillo");
		propNames2.Add("Runas03");
		
		propNames3.Add("Grillo");
		propNames3.Add("Hojarasca");
		propNames3.Add("Setas02");
		propNames3.Add("Runas02");
	
		propNames4.Add("Nido_hormigas");
		propNames4.Add("Runas01");
		propNames4.Add("Sanguijuela");
		propNames4.Add("Setas03");

	}
	
	void NewProp(Message m)
	{
//		prev_prop = m.MessageValue;
//		newProp = true;

		if (m.MessageSource.tag == "prop1")
		{
			prev_prop1 = true;
		}
		if (m.MessageSource.tag == "prop2")
		{
			prev_prop2 = true;
		}
		if (m.MessageSource.tag == "prop3")
		{
			prev_prop3 = true;
		}
		if (m.MessageSource.tag == "prop4")
		{
			prev_prop4 = true;
		}

		Debug.Log ("Manipulated: "+m.MessageSource.gameObject.name);
		// Logic for keys
		if(m.MessageSource.gameObject.name == "Runas01(Clone)"
			|| m.MessageSource.gameObject.name == "Runas02(Clone)"
			|| m.MessageSource.gameObject.name == "Runas03(Clone)"){
			if (hasToCreateKeyHead) {
				Debug.Log ("Apareix clau cap");
				hasToCreateKeyHead = false;
				createKeyHead = true;
			} else {
				if (hasToCreateKeyBody) {
					Debug.Log ("Apareix clau cos");
					hasToCreateKeyBody = false;
					createKeyBody = true;
				}
			}
		}

	}
	
	void CreatePropSource1()
	{
		string propName;
	
		propName = propNames1 [i];

		i += 1;

		if (i == propNames1.Count) {
			i = 0;
		}

		if (randomPositions) {
			Vector3 newPos = new Vector3 (source1.x + Random.Range (-randomPos1.x / 2, randomPos1.x / 2), source1.y, source1.z + Random.Range (-randomPos1.z / 2, randomPos1.z / 2));
			LoadProp (propName, newPos, "prop1");
		} else {
			LoadProp (propName, source1, "prop1");
		}
	
	}
	
	void CreatePropSource2()
	{
		string propName;

		if(!createKeyBody && !createKeyHead) {
			propName = propNames2[j];
			
			j+=1;
			
			if( j == propNames2.Count)
			{
				j=0;
			}
			
			if (randomPositions) {
				Vector3 newPos = new Vector3(source2.x+Random.Range(-randomPos2.x/2, randomPos2.x/2), source2.y, source2.z+Random.Range(-randomPos2.z/2, randomPos2.z/2));
				LoadProp (propName, newPos, "prop2");
			} else {
				LoadProp (propName, source2, "prop2");
			}
		} else {
			if (createKeyBody) {
				propName = "Llave_cuerpo";
				LoadKey (propName, source2, "prop2");
				createKeyBody = false;
			} else {
				if (createKeyHead) {
					propName = "Llave_mango";
					LoadKey (propName, source2, "prop2");
					createKeyHead = false;
				}
			}
		}
		
	}

	void CreatePropSource3()
	{
		string propName;
				
		if (!createKeyBody && !createKeyHead) {
			propName = propNames3[k];
			
			k+=1;
			
			if( k == propNames3.Count)
			{
				k=0;
			}
			
			if (randomPositions) {
				Vector3 newPos = new Vector3(source3.x+Random.Range(-randomPos3.x/2, randomPos3.x/2), source3.y, source3.z+Random.Range(-randomPos3.z/2, randomPos3.z/2));
				LoadProp (propName, newPos, "prop3");
			} else {
				LoadProp (propName, source3, "prop3");
			}
		} else {
			if (createKeyBody) {
				propName = "Llave_cuerpo";
				LoadKey (propName, source3, "prop3");
				createKeyBody = false;
			} else {
				if (createKeyHead) {
					propName = "Llave_mango";
					LoadKey (propName, source3, "prop3");
					createKeyHead = false;
				}
			}
		}
		
	}

	void CreatePropSource4()
	{
		string propName;

		if (!createKeyBody && !createKeyHead) {
			propName = propNames4 [p];
		
			p += 1;
		
			if (p == propNames4.Count) {
				p = 0;
			}
		
			if (randomPositions) {
				Vector3 newPos = new Vector3 (source4.x + Random.Range (-randomPos4.x / 2, randomPos4.x / 2), source4.y, source4.z + Random.Range (-randomPos4.z / 2, randomPos4.z / 2));
				LoadProp (propName, newPos, "prop4");
			} else {
				LoadProp (propName, source4, "prop4");
			}
		} else {
			if (createKeyBody) {
				propName = "Llave_cuerpo";
				LoadKey (propName, source4, "prop4");
				createKeyBody = false;
			} else {
				if (createKeyHead) {
					propName = "Llave_mango";
					LoadKey (propName, source4, "prop4");
					createKeyHead = false;
				}
			}
		}
		
	}
	
		
	void LoadProp ( string prop, Vector3 initPos, string Tag)
	{
		
		string path = "Props/" + prop + "/" + prop; //+ ".FBX";

		GameObject inputProp = (GameObject)Resources.Load(path, typeof(GameObject));		
				
		string[] filelines = File.ReadAllLines("assets/Resources/Props/" + prop + "/Scale.txt");
		
		for(int i = 0; i < filelines.Length; i++)
		{
			string[] coord = filelines[i].Split(";"[0]);
			coord0 = float.Parse(coord[0]) * 0.01f; //changed by /100 already
			coord1 = float.Parse(coord[1]) * 0.01f; //changed by /100 already
			coord2 = float.Parse(coord[2]) * 0.01f; //changed by /100 already
		}
		
		inputProp.transform.localScale = new Vector3(coord0,coord1,coord2);	
		
		new_prop = (GameObject) GameObject.Instantiate(inputProp);
		
		new_prop.AddComponent<PropController>();	
		
		new_prop.transform.position = initPos;
		
		if (new_prop.GetComponentsInChildren<MeshRenderer>() == null)
		{
			childRenderer = new_prop.GetComponentsInChildren<SkinnedMeshRenderer>();
			
			childRenderer[0].material.color = new Color (1,1,1);
			
			string path_texture = "Props/" + prop + "/" + prop ;// + ".png";
			
			childRenderer[0].material.mainTexture = Resources.Load<Texture>(path_texture);
		}

		new_prop.AddComponent<AudioSource>();	

		string path_controller = "Props/" + prop + "/" + prop;// + ".controller";
		
		Animator anim1 = new_prop.GetComponent<Animator>();
		
		anim1.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(path_controller);
		
		new_prop.tag = Tag;
	}

	void LoadKey ( string prop, Vector3 initPos, string Tag)
	{

		string path = "Props/" + prop + "/" + prop; //+ ".FBX";

		GameObject inputProp = (GameObject)Resources.Load(path, typeof(GameObject));		

		inputProp.transform.localScale = new Vector3(4f,4f,4f);	

		new_prop = (GameObject) GameObject.Instantiate(inputProp);

		new_prop.AddComponent<KeyController>();	

		if (prop == "Llave_cuerpo") {
			new_prop.transform.position = initPos + new Vector3(0.2f,-2f,-0.7f);
		} else {
			new_prop.transform.position = initPos + new Vector3(0.65f,-2f,-0.65f);
		}

		if (new_prop.GetComponentsInChildren<MeshRenderer>() == null)
		{
			childRenderer = new_prop.GetComponentsInChildren<SkinnedMeshRenderer>();

			childRenderer[0].material.color = new Color (1,1,1);

			string path_texture = "Props/" + prop + "/" + prop ;// + ".png";

			childRenderer[0].material.mainTexture = Resources.Load<Texture>(path_texture);
		}

		new_prop.AddComponent<AudioSource>();	

		string path_controller = "Props/" + prop + "/" + prop;// + ".controller";

		Animator anim1 = new_prop.GetComponent<Animator>();

		anim1.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(path_controller);

		new_prop.tag = Tag;
	}



	// Update is called once per frame
	void Update () 
	{

		if (prev_prop1 == true)
		{
			timer1 += Time.deltaTime;
			if (timer1>timeNewProp)
			{
				CreatePropSource1();
				timer1 = 0;
				prev_prop1 = false;
			}
		}
		if (prev_prop2 == true)
		{
			timer2 +=Time.deltaTime;
			if (timer2>timeNewProp)
			{
				CreatePropSource2();
				timer2 = 0;
				prev_prop2 = false;
			}
		}
		if (prev_prop3 == true)
		{
			timer3 +=Time.deltaTime;
			if (timer3>timeNewProp)
			{
				CreatePropSource3();
				timer3 = 0;
				prev_prop3 = false;
			}
		}
		if (prev_prop4 == true)
		{
			timer4 +=Time.deltaTime;
			if (timer4>timeNewProp)
			{
				CreatePropSource4();
				timer4 = 0;
				prev_prop4 = false;
			}
		}


	}

	protected virtual void OnDrawGizmos()
	{
		if (randomPositions) {
			Gizmos.color = Color.white;
			Gizmos.DrawWireCube (source1, randomPos1);
			Gizmos.DrawWireCube (source2, randomPos2);
			Gizmos.DrawWireCube (source3, randomPos3);
			Gizmos.DrawWireCube (source4, randomPos4);
		}
	}
}

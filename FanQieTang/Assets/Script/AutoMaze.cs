using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections.Generic;


public class AutoMaze : MonoBehaviour
{
	List<Wall> SearchList;
	List<Texture2D> textureList = new List<Texture2D> ();
	int x = 5;
	int s = 3;
	public GameObject Wall;
	public GameObject Ground;
	public GameObject Door;
	WallNode[,] NodeList;
	Wall[,] WallListi;
	Wall[,] WallListj;

	float space;
	float wallLength;
	float wallHight;

	private ReadJson readJson = new ReadJson ();

	void Start ()
	{
		x = this.readJson.curDialogInfo.getWidth ();
		s = this.readJson.curDialogInfo.getHeight ();
		List<string> imgList = this.readJson.curDialogInfo.getImageList ();
		foreach (string img in imgList) {
			textureList.Add ((Texture2D)Resources.Load (img));
		}
//		Wall.GetComponent<Renderer> ().material.mainTexture = texture;
		space = Wall.transform.localScale.z / 2f;
		wallLength = Wall.transform.localScale.z;
		wallHight = Wall.transform.localScale.y;
		Ground.transform.localScale = new Vector3 (s * wallLength, 1, x * wallLength);
		Ground.transform.position = new Vector3 (s * wallLength / 2f - wallLength / 2f, 0f, x * wallLength / 2f - wallLength / 2f);
		float doorX = s * wallLength - Door.transform.localScale.x - wallLength / 2f + Door.transform.localScale.x / 2f;
		float doorZ = x * wallLength - Door.transform.localScale.z - wallLength / 2f + Door.transform.localScale.z / 2f;
		Door.transform.position = new Vector3 (doorX, Door.transform.position.y, doorZ);
//		Ground.transform.localScale.z = s * wallLength;
		SearchList = new List<Wall> ();
		NodeList = new WallNode[s, x];
		WallListi = new Wall[s, x];
		WallListj = new Wall[x, s];
		for (int i = 0; i < s; i++)
			for (int j = 0; j < x; j++) {
				Vector3 position = new Vector3 (i * wallLength, 0, j * wallLength);
				WallNode node = new WallNode ();
				node.WallLsit = new List<Wall> ();
				node.Position = position;
				NodeList [i, j] = node;
			}
		for (int i = 0; i < s - 1; i++)
			for (int j = 0; j < x; j++) {
				GameObject Walli = Instantiate (Wall) as GameObject;
				this.setTexture (Walli);
//				Walli.GetComponent<Renderer> ().material.mainTexture = textureList [Random.Range (0, textureList.Count)];
//				Walli.transform.eulerAngles = new Vector3 (0, 90, 0);
				Walli.transform.position = NodeList [i, j].Position + new Vector3 (space, wallHight / 2, 0);
				Walli.name = ("i" + i + " " + j);
				WallListi [i, j] = new Wall ();
				WallListi [i, j].wall = Walli;
				WallListi [i, j].NodeList = new List<WallNode> ();
				WallListi [i, j].NodeList.Add (NodeList [i, j]);
				WallListi [i, j].NodeList.Add (NodeList [i + 1, j]);
			}
		for (int i = 0; i < x - 1; i++)
			for (int j = 0; j < s; j++) {
				GameObject Wallj = Instantiate (Wall) as GameObject;
				this.setTexture (Wallj);
//				Wallj.GetComponent<Renderer> ().material.mainTexture = textureList [Random.Range (0, textureList.Count)];
				Wallj.transform.eulerAngles = new Vector3 (0, 90, 0);
				Wallj.transform.position = NodeList [j, i].Position + new Vector3 (0, wallHight / 2, space);
				Wallj.name = ("j" + i + " " + j);
				WallListj [i, j] = new Wall ();
				WallListj [i, j].wall = Wallj;
				WallListj [i, j].NodeList = new List<WallNode> ();
				WallListj [i, j].NodeList.Add (NodeList [j, i]);
				WallListj [i, j].NodeList.Add (NodeList [j, i + 1]);
			}
		for (int i = 0; i < s; i++) {
			for (int j = 0; j < x; j++) {
				if (i != 0) {
					NodeList [i, j].WallLsit.Add (WallListi [i - 1, j]);
				}
				if (j != 0) {
					NodeList [i, j].WallLsit.Add (WallListj [j - 1, i]);
				}
				if (i != s - 1) {
					NodeList [i, j].WallLsit.Add (WallListi [i, j]);
				}
				if (j != x - 1) {
					NodeList [i, j].WallLsit.Add (WallListj [j, i]);
				}
			}
		}
		NodeList [1, 1].isConnect = true;
		for (int i = 0; i < NodeList [1, 1].WallLsit.Count; i++) {
			SearchList.Add (NodeList [1, 1].WallLsit [i]);
		}
		MigongCreate ();
		for (int i = 0; i < s; i++) {
			GameObject WallStartic1 = Instantiate (Wall) as GameObject;
			GameObject WallStartic2 = Instantiate (Wall) as GameObject;
			this.setTexture (WallStartic1);
			this.setTexture (WallStartic2);
//			WallStartic1.GetComponent<Renderer> ().material.mainTexture = textureList [Random.Range (0, textureList.Count)];
//			WallStartic2.GetComponent<Renderer> ().material.mainTexture = textureList [Random.Range (0, textureList.Count)];
			WallStartic1.transform.eulerAngles = new Vector3 (0, 90, 0);
			WallStartic2.transform.eulerAngles = new Vector3 (0, 90, 0);
			WallStartic1.name = "WallStartic1";
			WallStartic2.name = "WallStartic1";
			WallStartic1.transform.position = NodeList [i, 0].Position + new Vector3 (0, wallHight / 2, -space);
			WallStartic2.transform.position = NodeList [i, x - 1].Position + new Vector3 (0, wallHight / 2, space);
		}
		for (int i = 0; i < x; i++) {
			GameObject WallStartic1 = Instantiate (Wall) as GameObject;
			GameObject WallStartic2 = Instantiate (Wall) as GameObject;
			this.setTexture (WallStartic1);
			this.setTexture (WallStartic2);
//			WallStartic1.GetComponent<Renderer> ().material.mainTexture = textureList [Random.Range (0, textureList.Count)];
//			WallStartic2.GetComponent<Renderer> ().material.mainTexture = textureList [Random.Range (0, textureList.Count)];
//			WallStartic1.transform.eulerAngles = new Vector3 (0, 90, 0);
//			WallStartic2.transform.eulerAngles = new Vector3 (0, 90, 0);
			WallStartic1.name = "WallStartic2";
			WallStartic2.name = "WallStartic2";
			WallStartic1.transform.position = NodeList [0, i].Position + new Vector3 (-space, wallHight / 2, 0);
			WallStartic2.transform.position = NodeList [s - 1, i].Position + new Vector3 (space, wallHight / 2, 0);
		}
	}

	void setTexture (GameObject gameObj)
	{
		if (this.textureList.Count > 0) {
			gameObj.GetComponent<Renderer> ().material.mainTexture = textureList [Random.Range (0, textureList.Count)];
		}
	}

	void MigongCreate ()
	{
		if (SearchList.Count <= 0)
			return;
		int current = Random.Range (0, SearchList.Count);
		if (SearchList [current].NodeList [0].isConnect && SearchList [current].NodeList [1].isConnect) {
			SearchList [current].isCanSearchAgain = false;
			SearchList.RemoveAt (current);
			//Destroy(SearchList[current].wall);
		} else {
			for (int i = 0; i < 2; i++) {
				if (!SearchList [current].NodeList [i].isConnect) {
					SearchList [current].NodeList [i].isConnect = true;
					SearchList [current].isCanSearchAgain = false;
					for (int t = 0; t < SearchList [current].NodeList [i].WallLsit.Count; t++) {
						//						Debug.Log (current + "," + i + "," + t);
						//						Debug.Log (SearchList [current].NodeList [i].WallLsit [t]);
						if (SearchList [current].NodeList [i].WallLsit [t].isCanSearchAgain) {
							SearchList.Add (SearchList [current].NodeList [i].WallLsit [t]);
						}
					}
					Destroy (SearchList [current].wall);
					SearchList.RemoveAt (current);
				}
			}
		}
		if (SearchList.Count > 0) {
			MigongCreate ();
		}
	}

	// Update is called once per frame
	void Update ()
	{

	}
}

class WallNode
{
	public Vector3 Position;
	public bool isConnect;
	public List<Wall> WallLsit;

	public WallNode ()
	{
		isConnect = false;
	}
}

class Wall
{
	public GameObject wall;
	public List<WallNode> NodeList;
	public bool isCanSearchAgain;

	public Wall ()
	{
		isCanSearchAgain = true;
	}
}
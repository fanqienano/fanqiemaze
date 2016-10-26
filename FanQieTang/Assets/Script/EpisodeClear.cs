using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;

public class EpisodeClear : MonoBehaviour {

	private Text textBoard;
	private Image cover;
	private GameObject player;
	public Light light;
	private GameObject[] buttonList;
	private Animation animation;
	private bool isInDoor = false;

	private float TextAlpha = 0.0f;
	private float CoverAlpha = 0.0f;
	private float LightIntensity = 2.0f;

	// Use this for initialization
	void Start () {
		textBoard = GameObject.Find ("GameUI/Text").GetComponent<Text> ();
		cover = GameObject.Find ("GameUI/Cover").GetComponent<Image> ();
		player = GameObject.Find ("ninja_prefab");
		buttonList = GameObject.FindGameObjectsWithTag("ControlButton");
		foreach (GameObject g in buttonList){
			g.SetActive (true);
		}
		animation = player.GetComponent<Animation>();
		InvokeRepeating("ShowText", 0, 0.05f);
	}

	// Update is called once per frame
	void Update () {
	}

	void ShowText(){
		if(isInDoor & TextAlpha < 1.0f) {
			TextAlpha = TextAlpha + 0.02f;
			textBoard.material.color = new Color(textBoard.material.color.r, textBoard.material.color.g, textBoard.material.color.b, TextAlpha);
			if (textBoard.text == "") {
				textBoard.text = "游戏开始了";
			}
		}
		if (TextAlpha >= 1.0f & LightIntensity > 0.0f) {
			LightIntensity = LightIntensity - 0.1f;
			light.intensity = LightIntensity;
		}
		if (LightIntensity <= 0.0f & CoverAlpha < 1.0f) {
			CoverAlpha = CoverAlpha + 0.02f;
			cover.color = new Color(cover.color.r, cover.color.g, cover.color.b, CoverAlpha);
		}
		if (CoverAlpha >= 1.0f) {
			CancelInvoke ();
		}
	}

	void OnTriggerEnter(Collider e) {
		isInDoor = true;
		animation.Play ("idle", AnimationPlayMode.Stop);
		foreach (GameObject g in buttonList){
			g.SetActive (false);
		}
	}

	void OnTriggerExit(Collider e) {
		textBoard.text = "";
		isInDoor = false;
		foreach (GameObject g in buttonList){
			g.SetActive (true);
		}
	}
}

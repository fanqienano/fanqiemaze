using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;

public class EpisodeClear : MonoBehaviour
{

	private Text textBoard;
	private Image cover;
//	private GameObject player;
	public Light light;
	private GameObject[] buttonList;
//	private Animation animation;
	private bool isInDoor = false;

	private float TextAlpha = 1.0f;
	private float CoverAlpha = 1.0f;
	private float LightIntensity = 0.0f;

	private bool isStart = true;

	private ReadJson readJson = new ReadJson ();

	// Use this for initialization
	void Start ()
	{
		textBoard = GameObject.FindGameObjectWithTag("TextBoard").GetComponent<Text> ();
		cover = GameObject.FindGameObjectWithTag ("CoverImage").GetComponent<Image> ();
//		player = GameObject.Find ("ninja_prefab");
		buttonList = GameObject.FindGameObjectsWithTag ("ControlButton");
		foreach (GameObject g in buttonList) {
			g.SetActive (true);
		}
		light.intensity = 0.0f;
		cover.color = new Color (cover.color.r, cover.color.g, cover.color.b, 1.0f);
		textBoard.material.color = new Color (textBoard.material.color.r, textBoard.material.color.g, textBoard.material.color.b, 1.0f);
		textBoard.text = this.readJson.curDialogInfo.getStartText ();
//		animation = player.GetComponent<Animation> ();
		InvokeRepeating ("ShowText", 0, 0.05f);
	}

	// Update is called once per frame
	void Update ()
	{
	}

	void ShowText ()
	{
		if (isInDoor) {
			if (TextAlpha < 1.0f) {
				TextAlpha = TextAlpha + 0.02f;
				textBoard.material.color = new Color (textBoard.material.color.r, textBoard.material.color.g, textBoard.material.color.b, TextAlpha);
				if (textBoard.text == "") {
					textBoard.text = this.readJson.curDialogInfo.getClearText ();
				}
			}
			if (TextAlpha >= 1.0f & LightIntensity > 0.0f) {
				LightIntensity = LightIntensity - 0.1f;
				light.intensity = LightIntensity;
			}
			if (LightIntensity <= 0.0f & CoverAlpha < 1.0f) {
				CoverAlpha = CoverAlpha + 0.02f;
				cover.color = new Color (cover.color.r, cover.color.g, cover.color.b, CoverAlpha);
			}
			if (CoverAlpha >= 1.0f) {
				CancelInvoke ();
			}
		}
		if (isStart) {
//			if (TextAlpha < 1.0f) {
//				TextAlpha = TextAlpha + 0.02f;
//				textBoard.material.color = new Color (textBoard.material.color.r, textBoard.material.color.g, textBoard.material.color.b, TextAlpha);
//				if (textBoard.text == "") {
//					textBoard.text = this.readJson.curDialogInfo.getStartText ();
//				}
//			}
			if (CoverAlpha > 0.0f) {
				CoverAlpha = CoverAlpha - 0.02f;
				cover.color = new Color (cover.color.r, cover.color.g, cover.color.b, CoverAlpha);
				if (CoverAlpha <= 0.001f){
					CoverAlpha = 0.0f;
					cover.color = new Color (cover.color.r, cover.color.g, cover.color.b, CoverAlpha);
				}
			}
			if (CoverAlpha <= 0.0f & LightIntensity < 2.0f) {
				LightIntensity = LightIntensity + 0.1f;
				light.intensity = LightIntensity;
			}
			if (LightIntensity >= 2.0f && TextAlpha > 0.0f) {
				TextAlpha = TextAlpha - 0.02f;
				textBoard.color = new Color (textBoard.color.r, textBoard.color.g, textBoard.color.b, TextAlpha);
//				if (textBoard.text == "") {
//					textBoard.text = this.readJson.curDialogInfo.getStartText ();
//				}
			}
			if (TextAlpha <= 0.0f) {
				textBoard.text = "";
				isStart = false;
			}
//			if (LightIntensity <= 0.0f & CoverAlpha < 1.0f) {
//				CoverAlpha = CoverAlpha + 0.02f;
//				cover.color = new Color (cover.color.r, cover.color.g, cover.color.b, CoverAlpha);
//			}
		}
	}

	void OnTriggerEnter (Collider e)
	{
		isInDoor = true;
//		animation.Play ("idle", AnimationPlayMode.Stop);
		foreach (GameObject g in buttonList) {
			g.SetActive (false);
		}
	}

	void OnTriggerExit (Collider e)
	{
		textBoard.text = "";
		isInDoor = false;
		foreach (GameObject g in buttonList) {
			g.SetActive (true);
		}
	}
}

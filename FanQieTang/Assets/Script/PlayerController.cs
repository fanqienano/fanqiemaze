using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public Animation animation;
	public GameObject currentMap;
	public bool isFalling = false;
	public float movespeed =10f;

	void Start () {
//		if (SceneArgs.sceneName.StartsWith ("map1")) {
//			currentMap = (GameObject)Instantiate (Resources.Load ("map1"), Vector3.zero, Quaternion.identity);
//		} else if (SceneArgs.sceneName.StartsWith ("map2")) {
//			currentMap = (GameObject)Instantiate (Resources.Load ("map2"), Vector3.zero, Quaternion.identity);
//		} else {
//			Debug.Log(SceneArgs.sceneName);
//			Debug.Log(SceneArgs.sceneName.Equals ("map1"));
//		}
		animation = gameObject.GetComponent<Animation>();
	}

	void FixedUpdate () {
		//		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		bool isAction = false;
		if (Input.GetKey (KeyCode.UpArrow)) {
			isAction = true;
			transform.Translate (Vector3.forward * movespeed * Time.deltaTime * moveVertical);
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			isAction = true;
			transform.Translate (Vector3.back * movespeed * Time.deltaTime * -moveVertical);
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			isAction = true;
			transform.Rotate (0, -90 * Time.deltaTime, 0);
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			isAction = true;
			transform.Rotate (0, 90 * Time.deltaTime, 0);
		}
		if (isAction) {
			animation.Play ("run", AnimationPlayMode.Stop);
		} else {
			animation.Play ("idle", AnimationPlayMode.Stop);
		}
	}

//	void jumpUp () {
//		if (!animation.IsPlaying ("jump")) {
//			animation.Play ("jump", PlayMode.StopAll);
//			GetComponent<Rigidbody> ().AddForce (Vector3.up * 500);
//		}
//	}

	void Update () {
		if (gameObject.transform.position.y < -3) {
			isFalling = true;
		}
	}
}

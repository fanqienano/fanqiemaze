using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {
	private Animation animation;
	// Use this for initialization
	void Start () {
		animation = gameObject.GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!animation.isPlaying) {
			animation.Play ("idle", AnimationPlayMode.Stop);
		}
	}
}

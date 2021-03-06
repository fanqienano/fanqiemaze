﻿using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// 脚本位置：UGUI按钮组件身上
/// 脚本功能：实现按钮长按状态的判断
/// 创建时间：2015年11月17日
/// </summary>

// 继承：按下，抬起和离开的三个接口
public class ButtonLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
	public GameObject gameObj;
//	public Animation animation;
	// 延迟时间
	private float delay = 0.01f;

	// 按钮是否是按下状态
	private bool isDown = false;

	// 按钮最后一次是被按住状态时候的时间
	private float lastIsDownTime;

	void Start () {
//		animation = gameObj.GetComponent<Animation> ();
	}

	void FixedUpdate ()
	{
		bool isAction = false;
		// 如果按钮是被按下状态
		if (isDown) {
			// 当前时间 - 按钮最后一次被按下的时间 > 延迟时间0.2秒
			if (Time.time - lastIsDownTime > delay) {
				// 触发长按方法
				gameObj.transform.Rotate (0, -90 * Time.deltaTime, 0);
				// 记录按钮最后一次被按下的时间
				lastIsDownTime = Time.time;
//				animation.Play ("run", AnimationPlayMode.Stop);
			}
		}
//		if (isAction) {
//			animation.Play ("run", AnimationPlayMode.Stop);
//		} else {
//			animation.Play ("idle", AnimationPlayMode.Stop);
//		}
	}

	// 当按钮被按下后系统自动调用此方法
	public void OnPointerDown (PointerEventData eventData)
	{
		isDown = true;
		lastIsDownTime = Time.time;
	}

	// 当按钮抬起的时候自动调用此方法
	public void OnPointerUp (PointerEventData eventData)
	{
//		animation.Play ("idle", AnimationPlayMode.Stop);
		isDown = false;
	}

	// 当鼠标从按钮上离开的时候自动调用此方法
	public void OnPointerExit (PointerEventData eventData)
	{
		isDown = false;
	}
}
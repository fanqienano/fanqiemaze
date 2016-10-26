using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EpisodeInfo
{
	private int index = 1;
	private string startText = string.Empty;
	private string clearText = string.Empty;
	private int height = 1;
	private int width = 1;
	private List<string> imageList = new List<string> ();

	public void setIndex (int index)
	{
		this.index = index;
	}

	public void setStartText (string startText)
	{
		this.startText = startText;
	}

	public void setClearText (string clearText)
	{
		this.clearText = clearText;
	}

	public void setHeight (int height)
	{
		this.height = height;
	}

	public void setWidth (int width)
	{
		this.width = width;
	}

	public int getIndex ()
	{
		return this.index;
	}

	public string getStartText ()
	{
		return this.startText;
	}

	public string getClearText ()
	{
		return this.clearText;
	}

	public int getHeight ()
	{
		return this.height;
	}

	public int getWidth ()
	{
		return this.width;
	}

	public void setImageList (List<string> imageList)
	{
		this.imageList = imageList;
	}

	public List<string> getImageList ()
	{
		return this.imageList;
	}

	public void clearImageList ()
	{
		this.imageList.Clear ();
	}

	public void addImageList (string imageName)
	{
		this.imageList.Add (imageName);
	}
}

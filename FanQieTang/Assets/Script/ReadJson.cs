using System.Collections;
using LitJson;
using System;

public class ReadJson {
//	private JsonData curJson;
	private int curId = 1;
	private EpisodeInfo curDialogInfo = new EpisodeInfo();
	private JsonData allJson;

	public ReadJson(){
		this.readRecord ();
		this.readAllJson ();
		this.reloadJsonFile ();
	}

	private void readAllJson(){
		this.allJson = JsonMapper.ToObject(Utils.readJsonFile(Utils.mainJsonPath));
	}

	private void reloadJsonFile(){
		JsonData curJson = this.allJson [this.curId.ToString ()];
		this.curDialogInfo.setIndex (this.curId);
		this.curDialogInfo.setStartText (curJson["startText"].ToString());
		this.curDialogInfo.setClearText (curJson["clearText"].ToString());
		this.curDialogInfo.setHeight (int.Parse(curJson["height"].ToString()));
		this.curDialogInfo.setWidth (int.Parse(curJson["width"].ToString()));
		this.curDialogInfo.clearImageList ();
		if (curJson ["imageList"].IsArray) {
			foreach (string i in curJson ["imageList"]) {
				this.curDialogInfo.addImageList (i);;
			}
		}
	}

	public void nextEpisode(){
		this.curId = this.curId + 1;
		this.saveRecord ();
		this.reloadJsonFile ();
	}

	public void saveRecord(){
		Utils.saveRecord (Utils.recordPath, this.curId);
	}

	public void readRecord(){
		this.curId = Utils.readRecord ();
	}
}

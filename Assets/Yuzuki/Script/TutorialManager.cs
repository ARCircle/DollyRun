using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

	public bool endActionSupport = false;

	private int ProcessNum = -1;
	private string endMessage = "バッチリだね！！";

	string[] GuideText;

	GameObject GuideObj;
	Text Guide;


	// Use this for initialization
	void Start () {
		GuideObj = GameObject.Find ("GuideText");
		Guide = GuideObj.transform.Find ("Text").gameObject.GetComponent <Text> ();
		GuideObj.SetActive (false);

		NextTutorial ();
	}
	
	public void NextTutorial () {
		ProcessNum++;
		GuideObj.SetActive (true);
		//プレイヤーの操作をテキスト送り以外無効にする

		switch (ProcessNum) {
		case 0:
			GuideText = new string[3];
			GuideText [0] = "a";
			GuideText [1] = "aa";
			GuideText [2] = "aaa";
			break;
		case 1:
			GuideText = new string[7];
			break;

		}

		StartCoroutine (ShowGuideText ());
	}

	private IEnumerator ShowGuideText () {
		int TextNum = 0;
		Guide.text = GuideText [0];
		Debug.Log ("Start Guide Text");
		yield return null;

		while (true) {

			if (Input.GetMouseButtonUp (0)) {
				//次のテキストへ
				if (++TextNum == GuideText.Length) {
					GuideObj.SetActive (false);
					Debug.Log ("End Guide Text");
					yield return SupportAction ();

					yield break;
				}

				Guide.text = GuideText [TextNum];


				//連続入力禁止用に0.5s待つ
				yield return new WaitForSeconds (0.5f);
			}

			yield return null;
		}
	}


	private IEnumerator SupportAction () {
		Debug.Log ("Start Support Action");
		switch (ProcessNum) {
		case 0:
			break;
		case 1:
			endActionSupport = true;
			break;

		}
		yield return null;

		while (true) {
			if (endActionSupport) {
				endActionSupport = false;

				ProcessNum++;
				Debug.Log ("End Support Action");

				//終了メッセージを表示
				GuideObj.SetActive (true);
				Guide.text = endMessage;
				//タッチしたら終了
				while (true) {
					if (Input.GetMouseButtonUp (0)) {
						GuideObj.SetActive (false);
						yield break;
					}
					yield return null;
				}
			}

			yield return new WaitForSeconds (0.2f);
		}
	}

}

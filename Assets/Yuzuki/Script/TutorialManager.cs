using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

	public bool endActionSupport = false;

	private int ProcessNum = -1;
	private string endMessage = "バッチリだね！！";

	float time = 0;
	public float waittime = 2;
	string[] GuideText;
	bool isWaiting = true;

	public GameObject GuideObj;
	Text Guide;


	// Use this for initialization
	void Start () {
		GuideObj = GameObject.Find ("GuideText");
		Guide = GuideObj.transform.Find ("Text").gameObject.GetComponent <Text> ();
		GuideObj.SetActive (false);

		//NextTutorial ();
	}

	void Update () {
		//次のチュートリアルへ進むための制御
		if (isWaiting) {
			
			time += Time.deltaTime;
			if (time > waittime) {
				time = 0;
				isWaiting = false;
				NextTutorial ();
			}
		}
	}
	
	public void NextTutorial () {

		ProcessNum++;
		GuideObj.SetActive (true);
		//プレイヤーの操作をテキスト送り以外無効にする

		switch (ProcessNum) {
		case 0:		//最初の基本説明と線路の書き方
			GuideText = new string[3];
			GuideText [0] = "a";
			GuideText [1] = "aa";
			GuideText [2] = "aaa";

			waittime = 0;
			break;
		case 1:		//線路を引く、の追加説明
			GuideText = new string[7];
			waittime = 2;
			break;
		case 2:		//アイテムの説明
			GuideText = new string[8];
			GuideText [0] = "次は、アイテムについて\n説明するよ！！";
			GuideText [1] = "aa";
			GuideText [2] = "aaa";

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
			this.gameObject.AddComponent <DrowLineSupport> ();
			break;
		case 2:
			this.gameObject.AddComponent <ItemSupport> ();
			break;


		case 1:
			//テキスト表示だけで終わるときはこの二つを実行する
			isWaiting = true;
			yield break;
			break;

		}
		yield return null;

		while (true) {
			if (endActionSupport) {
				endActionSupport = false;

				Debug.Log ("End Support Action");

				//終了メッセージを表示
				GuideObj.SetActive (true);
				Guide.text = endMessage;
				//タッチしたら終了
				while (true) {
					if (Input.GetMouseButtonUp (0)) {
						GuideObj.SetActive (false);
						isWaiting = true;
						yield break;
					}
					yield return null;
				}
			}

			yield return new WaitForSeconds (0.2f);
		}
	}

}

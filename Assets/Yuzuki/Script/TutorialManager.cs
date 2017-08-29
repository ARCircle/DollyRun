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
		//GameObject.Find ("CoinManager").GetComponent <ItemManager> ().EnableCoinGenerate = false;
		GuideObj = GameObject.Find ("GuideText");
		Guide = GuideObj.transform.Find ("Text").gameObject.GetComponent <Text> ();
		GuideObj.SetActive (false);

		GameObject.Find ("PlayerBase").GetComponent <PlayerMoveControl_tutorial> ().DoDrowRail = false;
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
			GuideText [0] = "基本操作";
			GuideText [1] = "aa";
			GuideText [2] = "aaa";

			waittime = 0;
			break;
		case 1:		//線路を引く、の追加説明
			GuideText = new string[2];
			GuideText [0] = "こうやってひくんだよ";
			GuideText [1] = "aa";

			waittime = 2;
			break;
		case 2:		//アイテムの説明（青）
			GuideText = new string[3];
			GuideText [0] = "次は、アイテムについて\n説明するよ！！";
			GuideText [1] = "aa";
			GuideText [2] = "aaa";

			waittime = 0;
			break;

		case 3:		//アイテムの説明（青）の追加説明
			GuideText = new string[2];
			GuideText [0] = "青は無敵";
			GuideText [1] = "aa";

			waittime = 2;
			break;

		case 4:		//アイテムの説明（赤）
			GuideText = new string[3];
			GuideText [0] = "次は、赤について\n説明するよ！！";
			GuideText [1] = "aa";
			GuideText [2] = "aaa";

			waittime = 0;
			break;

		case 5:		//アイテムの説明（赤）、の追加説明
			GuideText = new string[2];
			GuideText [0] = "赤は回収";
			GuideText [1] = "aa";

			waittime = 2;
			break;

		case 6:		//アイテムの説明（どっちも）
			GuideText = new string[3];
			GuideText [0] = "次は、どうじについて\n説明するよ！！";
			GuideText [1] = "aa";
			GuideText [2] = "aaa";

			waittime = 0;
			break;

		case 7:		//アイテムの説明（どっちも）、の追加説明
			GuideText = new string[2];
			GuideText [0] = "どっちも使える";
			GuideText [1] = "aa";

			waittime = 2;
			break;


		case 8:		//最後に
			GuideText = new string[3];
			GuideText [0] = "おわた";
			GuideText [1] = "aa";
			GuideText [2] = "aaa";

			break;


		}

		StartCoroutine (ShowGuideText ());
	}

	private IEnumerator ShowGuideText () {
		int TextNum = 0;
		float ShowTime = 0;
		Guide.text = GuideText [0];
		Debug.Log ("Start Guide Text");
		yield return null;

		while (true) {

			if (Input.GetMouseButtonUp (0) || ShowTime > 3.0f) {
				//次のテキストへ
				ShowTime = 0;
				if (++TextNum == GuideText.Length) {
					GuideObj.SetActive (false);
					Debug.Log ("End Guide Text");
					yield return SupportAction ();

					yield break;
				}

				Guide.text = GuideText [TextNum];


				//連続入力禁止用に0.3s待つ
				yield return new WaitForSeconds (0.3f);
			}
			ShowTime += Time.deltaTime;
			yield return null;
		}
	}


	private IEnumerator SupportAction () {
		Debug.Log ("Start Support Action");
		switch (ProcessNum) {
		case 0:		//レールを引くチュートリアル
			this.gameObject.AddComponent <DrowLineSupport> ();
			break;
		case 2:		//Aアイテムのチュートリアル
			this.gameObject.AddComponent <ItemSupport> ();
			break;
		case 4:		//Rアイテムのチュートリアル
			this.GetComponent<ItemSupport> ().Start2 ();
			break;
		case 6:		//AとRの同時使用のチュートリアル
			this.GetComponent<ItemSupport> ().Start3 ();
			break;
		case 8:		//終了
			Fader fade = this.gameObject.AddComponent <Fader> ();
			yield return fade.blackin (1.5f, Totitle);
			yield break;
			//break;


		case 1:
		case 3:
		case 5:
		case 7:
			//テキスト表示だけで終わるときはこの二つを実行する
			isWaiting = true;
			yield break;
			//break;

		}
		yield return null;

		while (true) {
			if (endActionSupport) {
				endActionSupport = false;

				Debug.Log ("End Support Action");

				//終了メッセージを表示
				GuideObj.SetActive (true);
				Guide.text = endMessage;
				float time = 0;

				yield return new WaitForSeconds (1);
				//タッチしたら終了
				while (true) {
					if (Input.GetMouseButtonUp (0) || time > 3.0f) {
						GuideObj.SetActive (false);
						isWaiting = true;
						yield break;
					}
					time += Time.deltaTime;
					yield return null;
				}
			}

			yield return new WaitForSeconds (0.2f);
		}
	}


	void Totitle () {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Tittle");
	}

}

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
	PlayerMoveControl_tutorial pmc;

	// Use this for initialization
	void Start () {
		//GameObject.Find ("CoinManager").GetComponent <ItemManager> ().EnableCoinGenerate = false;
		GrobalClass.Reset ();
		GuideObj = GameObject.Find ("GuideText");
		Guide = GuideObj.transform.Find ("Text").gameObject.GetComponent <Text> ();
		GuideObj.SetActive (false);
		GrobalClass.StartInterval = 0f;
		pmc = GameObject.Find ("PlayerBase").GetComponent <PlayerMoveControl_tutorial> ();
		pmc.DoDrowRail = false;
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
			GuideText [0] = "これからチュートリアルを\n始めるよ！";
			GuideText [1] = "障害物をよけながら\nコインを集めよう！";
			GuideText [2] = "まずは線路を移動しよう！";

			waittime = 0;
			break;
		case 1:		//線路を引く、の追加説明
			GuideText = new string[2];
			GuideText [0] = "新しい線路を引いて\n移動できたね！";
			GuideText [1] = "これがDollyRunの\n基本操作だよ！";

			waittime = 2;
			break;
		case 2:		//アイテムの説明（青）
			GuideText = new string[3];
			GuideText [0] = "次は、アイテムについて\n説明するよ！！";
			GuideText [1] = "アイテムは2種類！";
			GuideText [2] = "まずは<color=#AAAAFF>アクアマリン</color>クリスタルだよ！";

			pmc.DoDrowRail = false;
			waittime = 0;
			break;

		case 3:		//アイテムの説明（青）の追加説明
			GuideText = new string[2];
			GuideText [0] = "<color=#AAAAFF>アクアマリン</color>を使うと、\n障害物を壊せるよ！";
			GuideText [1] = "ピンチになったら使おう！";

			pmc.DoDrowRail = true;
			waittime = 2;
			break;

		case 4:		//アイテムの説明（赤）
			GuideText = new string[1];
			GuideText [0] = "次は<color=#FF8888>ルビー</color>クリスタル";
			//GuideText [1] = "aa";
			//GuideText [2] = "aaa";

			pmc.DoDrowRail = false;
			waittime = 0;
			break;

		case 5:		//アイテムの説明（赤）、の追加説明
			GuideText = new string[2];
			GuideText [0] = "<color=#FF8888>ルビー</color>はコインを引き寄せるよ！";
			GuideText [1] = "取れない場所にあるコインを\n一気にゲットしよう！";

			pmc.DoDrowRail = true;
			waittime = 2;
			break;

		case 6:		//アイテムの説明（どっちも）
			GuideText = new string[3];
			GuideText [0] = "アイテムの効果時間は\n下に表示されるよ";
			GuideText [1] = "取った分だけ\n効果時間が伸びるんだ";
			GuideText [2] = "でもそれぞれ3つまで\nだから気をつけてね！";

			pmc.DoDrowRail = false;
			waittime = 0;
			break;

		case 7:		//アイテムの説明（どっちも）、の追加説明
			GuideText = new string[2];
			GuideText [0] = "一度にどっちも\n使っちゃうよ！";
			GuideText [1] = "ためた分は全部\nなくなっちゃうから\n注意しよう！";

			pmc.DoDrowRail = true;
			waittime = 2;
			break;


		case 8:		//最後に
			GuideText = new string[5];
			GuideText [0] = "これでチュートリアルは終わり！";
			GuideText [1] = "高いスコアを取るには……";
			GuideText [2] = "長い距離を走って、";
			GuideText [3] = "コインをたくさん取ろう！";
			GuideText [4] = "ハイスコアの記録\n目指してがんばってね！！";

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

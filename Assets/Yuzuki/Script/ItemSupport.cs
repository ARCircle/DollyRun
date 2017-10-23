using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSupport : MonoBehaviour {

	string FlickSupportText = "下のピッケルを\n上にフリックして\nアイテムを使おう！！";	//フリックしてアイテムを使うことを促すテキスト
	string[] GuideText;
	int Process = 0;


	GameObject crystal;
	GameObject maruta;
	GameObject coin;
	public GameObject GuideTextObj;

	ItemEffectControler_iyoka _iec;
	ItemManager im;

	// Use this for initialization
	void Start () {
		
		GuideTextObj = this.GetComponent <TutorialManager> ().GuideObj;
		_iec = GameObject.Find ("GameManager").GetComponent <ItemEffectControler_iyoka> ();

		int railcheck = GrobalClass.RideRailNum - 2;
		Debug.Log ("railcheck : " + railcheck);

		//アイテムの生成
		crystal = Instantiate (Resources.Load("Crystal_obj_A_iyoka") as GameObject);
		crystal.transform.position = new Vector3 (4.2f * railcheck, 0, 35);
		crystal.GetComponent<CrystalScript_iyoka> ().type = "A";
		maruta = Instantiate (Resources.Load("ObjBlock01") as GameObject);
		maruta.transform.position = new Vector3 (4.2f * railcheck, 0, 40);

		//テキストの設定
		GuideText = new string[1];
		GuideText[0] = "これが<color=#AAAAFF>アクアマリン</color>";
		//GuideText[1] = "aa";
		//GuideText[2] = "aaa";

		StartCoroutine (CheckItemPos ());
	}

	public void Start2 () {
		Process = 1;
		GuideTextObj = this.GetComponent <TutorialManager> ().GuideObj;
		_iec = GameObject.Find ("GameManager").GetComponent <ItemEffectControler_iyoka> ();
		im = GameObject.Find ("CoinManager").GetComponent <ItemManager> ();
		int railcheck = GrobalClass.RideRailNum - 2;
		Debug.Log ("railcheck : " + railcheck);

		//アイテムの生成
		crystal = Instantiate (Resources.Load("Crystal_obj_R_iyoka") as GameObject);
		crystal.transform.position = new Vector3 (4.2f * railcheck, 0, 35);
		crystal.GetComponent<CrystalScript_iyoka> ().type = "R";
		coin = Instantiate (Resources.Load("ButterflyCoinPrefab") as GameObject);
		coin.transform.position = new Vector3 (0, 0, 45);

		//リストに追加することで、アイテム吸収をできるようにする
		coin.transform.parent = im.transform;
		im.instanceList.Add (coin);


		//テキストの設定
		GuideText = new string[1];
		GuideText[0] = "これが<color=#FF8888>ルビー</color>";
		//GuideText[1] = "aa";
		//GuideText[2] = "aaa";

		StartCoroutine (CheckItemPos ());
	}
	public void Start3 () {
		Process = 2;
		GuideTextObj = this.GetComponent <TutorialManager> ().GuideObj;
		_iec = GameObject.Find ("GameManager").GetComponent <ItemEffectControler_iyoka> ();
		im = GameObject.Find ("CoinManager").GetComponent <ItemManager> ();
		int railcheck = GrobalClass.RideRailNum - 2;
		Debug.Log ("railcheck : " + railcheck);

		//アイテムの生成
		crystal = Instantiate (Resources.Load("Crystal_obj_R_iyoka") as GameObject);
		crystal.transform.position = new Vector3 (4.2f * railcheck, 0, 35);
		crystal.GetComponent<CrystalScript_iyoka> ().type = "R";
		coin = Instantiate (Resources.Load("ButterflyCoinPrefab") as GameObject);
		coin.transform.position = new Vector3 (0, 0, 50);
		//リストに追加することで、アイテム吸収をできるようにする
		coin.transform.parent = im.transform;
		im.instanceList.Add (coin);


		crystal = Instantiate (Resources.Load("Crystal_obj_A_iyoka") as GameObject);
		crystal.transform.position = new Vector3 (4.2f * railcheck, 0, 36.5f);
		crystal.GetComponent<CrystalScript_iyoka> ().type = "A";

		maruta = Instantiate (Resources.Load("ObjBlock01") as GameObject);
		maruta.transform.position = new Vector3 (4.2f * railcheck, 0, 40);


		//テキストの設定
		GuideText = new string[2];
		GuideText[0] = "二つのアイテムを取れそうだね";
		GuideText[1] = "同時に使ったらどうなるんだろう";
		//GuideText[2] = "aaa";

		StartCoroutine (CheckItemPos ());
	}
	

	IEnumerator CheckItemPos () {
		while (true) {
			if (crystal.transform.position.z < 13) {
				//2.テキスト表示
				GrobalClass.pause = true;
				yield return ShowText ();

				//3.ポーズ解除
				GrobalClass.pause = false;

				//4.アイテムを取ったらポーズ(取るまで処理を止める)
				if (Process == 0 || Process == 2) {
					while (ItemStatus.status_A == 0) {
						yield return null;
					}
				} else if (Process == 1) {
					while (ItemStatus.status_R == 0) {
						yield return null;
					}
				}
				GrobalClass.pause = true;
				yield return new WaitForSeconds (0.5f);
				GuideTextObj.SetActive (true);
				GuideTextObj.transform.Find("Text").gameObject.GetComponent <Text> ().text = FlickSupportText;

				//5. アイテムを使うように誘導
				while (true) {
					if (_iec.Flick ()) {
						//フリックを感知したら、先にポーズを解除し、アイテムを使う命令を送る
						//6.ポーズを解除して終了
						GrobalClass.pause = false;
						GuideTextObj.SetActive (false);

						_iec.UseItemProcess ();

						//このプロセスを終了
						this.GetComponent<TutorialManager> ().endActionSupport = true;
						if (Process == 2) {
							Destroy (this.gameObject.GetComponent<ItemSupport> ());
						}

						yield break;
					}
					yield return null;
				}


				//yield break;
			}
			yield return new WaitForSeconds(0.3f);
		}
	}

	IEnumerator ShowText () {
		int TextNum = 0;
		float ShowTime = 0;
		GuideTextObj.SetActive (true);
		GuideTextObj.transform.Find("Text").gameObject.GetComponent <Text> ().text = GuideText [0];
		while (true) {
			if (Input.GetMouseButtonUp (0) || ShowTime > 3.0f) {
				//次のテキストへ
				ShowTime = 0;
				if (++TextNum == GuideText.Length) {
					GuideTextObj.SetActive (false);

					yield break;
				}
				//テキスト更新
				GuideTextObj.transform.Find("Text").gameObject.GetComponent <Text> ().text = GuideText [TextNum];


				//連続入力禁止用に0.3s待つ
				yield return new WaitForSeconds (0.3f);
			}
			ShowTime += Time.deltaTime;
			yield return null;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	Fader fade;
	public int testnum;
	// Use this for initialization
	void Awake () {
		fade = this.gameObject.AddComponent <Fader> ();
		//ランキングをロード
		ScoreCalculator.LoadTopScore ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void StartScoreMenu () {
		Image[] imgs = GetComponentsInChildren <Image> ();
		Text[] texts = GetComponentsInChildren <Text> ();
		GetComponent <Canvas> ().enabled = true;

		StartCoroutine (fade.fadein2 (0.2f, imgs, texts));
	}

	public void EndScoreMenu () {
		Image[] imgs = GetComponentsInChildren <Image> ();
		Text[] texts = GetComponentsInChildren <Text> ();

		StartCoroutine (fade.fadeout2 (0.2f, imgs, texts, DisenableScoreCanvas));
	}

	void DisenableScoreCanvas () {
		GetComponent <Canvas> ().enabled = false;
	}
}


public static class ScoreCalculator {

	public static int[] TopScore = { 0, 0, 0, 0, 0, 0, 0 };	//トップ７のスコア
	public static int TmpScore = 0;
	public static int LatestScoreNum = -1;		//直前のスコアの番地、ランク外の場合は-1、ランクインしたら0以上の値になる


	public static void LoadTopScore () {
		TmpScore = PlayerPrefs.GetInt ("TmpScore");
		TopScore = PlayerPrefsX.GetIntArray ("TopScore");
		Debug.Log ("tmp : " + TmpScore);
		if (TopScore.Length <= 0) {
			ResetTopScore ();
		} else if(TmpScore > 0){
			UpdateTopScore (TmpScore);
			UpdateTmpScore (0);
		}
	}

	public static void ResetTopScore () {
		TopScore = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
		PlayerPrefsX.SetIntArray ("TopScore", TopScore);
		UpdateTmpScore (0);
	}

	public static void UpdateTmpScore(int NowScore){
		TmpScore = NowScore;
		PlayerPrefs.SetInt ("TmpScore", TmpScore);
	}

	//ステージをクリアした後は、この関数を呼び出して、引数として取得したスコアを入れればよい
	public static void UpdateTopScore (int NowScore) {

		UpdateTmpScore (0);
		for (int i = 0; i < 7; i++) {
			if (TopScore [i] < NowScore) {
				for (int j = 5; j - i >= 0; j--) {
					TopScore [j + 1] = TopScore [j];
				}
				TopScore [i] = NowScore;
				LatestScoreNum = i;

				//データ保存
				PlayerPrefsX.SetIntArray ("TopScore", TopScore);
				Debug.Log (TopScore [0] + ", " + TopScore [1] + ", " + TopScore [2] + ", " + TopScore [3] + ", " + TopScore [4] + ", " + TopScore [5] + ", " + TopScore [6]);
				//計算結果がでたら即終了
				return;
			}
		}
		//記録が更新されない場合は-1に戻す
		LatestScoreNum = -1;
	}
}

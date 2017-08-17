using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GrobalClass{

	public static int   RideRailNum = 2;  //プレイヤーが乗っている線路の番号 左から順に1,2,3 描いた線路は-1
	public static float StartInterval = 3f;  // ステージ開始時のカメラワーク時間
	public static float usingAtime = 0f;  //アイテムAを使用中は>0
	public static float usingRtime = 0f;  //アイテムBを使用中は>0
	public static float speed = 5f;       // 1秒にz軸マイナス方向へ進む速さ
	public static int   speedlevel = 1;   // スピード上昇段階
	public static float playtime = 0f;    // ゲーム進行時間
	public static float distance = 0f;    // 移動距離
	public static bool pause = false;     // ポーズ中はtrue
	public static bool gameover = false;  // ゲームオーバーになるとtrue

    public static int coins = 0; //今まで手に入れたコインの枚数

	public static int[] TopScore = new int[7];  //トップ7のスコア
	public static int LatestScoreNum = 0;  //直前のスコアの配列番号

	public static void Reset(){   //ゲームスタート時に実行してね！
		RideRailNum = 2;  //プレイヤーが乗っている線路の番号 左から順に1,2,3 描いた線路は-1
		usingAtime = 0f;  //アイテムAを使用中は>0
		usingRtime = 0f;  //アイテムBを使用中は>0
		speed = 5f;       // 1秒にz軸マイナス方向へ進む速さ
		speedlevel = 1;   // スピード上昇段階
		playtime = 0f;    // ゲーム進行時間
		distance = 0f;    // 移動距離
		pause = false;    // ポーズ中はtrue
		gameover = false; // ゲームオーバーになるとtrue
		coins = 0;        //今まで手に入れたコインの枚数
	}
}

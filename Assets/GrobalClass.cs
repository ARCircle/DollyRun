using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GrobalClass{

	public static int RideRailNum;  //プレイヤーが乗っている線路の番号 左から順に1,2,3 描いた線路は-1
	public static float usingAtime = 0f;  //アイテムAを使用中は>0
	public static float usingRtime;  //アイテムBを使用中は>0

    public static bool useItemR;    //アイテムＲ（コインを集める）を使用する変数
                                    //trueにすれば使用するが、次フレーム以降はfalseになる
                                    //使用している間に再びtrueにするとアイテム使用時間が延長される
    public static int coins; //今まで手に入れたコインの枚数
	public static bool gameover = false;
}

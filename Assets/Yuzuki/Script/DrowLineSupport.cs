using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrowLineSupport : MonoBehaviour {

	PlayerMoveControl pmc;

	Vector2 inputPos = new Vector2 ();
	Animation anim;
	Image SlideGuide;
	GameObject GuideText;

	bool isDrow = false;

	// Use this for initialization
	void Start () {
		pmc = GameObject.Find ("PlayerBase").GetComponent <PlayerMoveControl> ();
		anim = transform.Find ("SlideGuide").gameObject.GetComponent <Animation> ();
		SlideGuide = anim.gameObject.GetComponent <Image> ();
		GuideText = this.GetComponent <TutorialManager> ().GuideObj;

		//サポートアニメーション再生
		anim.Play ();
		SlideGuide.enabled = true;

		//ガイドテキスト表示
		GuideText.SetActive (true);
		GuideText.transform.Find ("Text").gameObject.GetComponent <Text> ().text = "スライドして線路を引こう！！";

		GrobalClass.pause = true;
	}
	
	// Update is called once per frame
	void Update () {
		//想定通りに描いてくれているかのチェック
		if (Input.GetMouseButtonDown(0)) {
			if (Input.mousePosition.x < 140 && Input.mousePosition.y < 370 && Input.mousePosition.y > 160) {
				inputPos.x = Input.mousePosition.x;
				inputPos.y = Input.mousePosition.y;
				isDrow = true;
				//animation停止
				anim.Stop ();
				SlideGuide.enabled = false;

			}
		}

		if (isDrow) {
			pmc.MainProcess ();
		}

		if (Input.GetMouseButtonUp (0)) {
			isDrow = false;

			if (Input.mousePosition.x > 200 && Input.mousePosition.y > inputPos.y) {
				//描いてほしい通りに描いてくれたので、その後の処理

				//成功音の再生処理を追加...

				//ポーズを解いて次の処理へ命令を出し、このスクリプトを消す
				GrobalClass.pause = false;
				this.GetComponent<TutorialManager> ().endActionSupport = true;
				GuideText.SetActive (false);
				Destroy (this.gameObject.GetComponent<DrowLineSupport> ());
			} else {
				anim.Play ();
				SlideGuide.enabled = true;
			}
		}

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrowLineSupport : MonoBehaviour {

	PlayerMoveControl_tutorial pmc;

	float inputPosZ;
	Animation anim;
	Image SlideGuide;
	GameObject GuideText;


	bool isDrow = false;

	// Use this for initialization
	void Start () {
		pmc = GameObject.Find ("PlayerBase").GetComponent <PlayerMoveControl_tutorial> ();
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
			Vector3 pos;
			pos = MouseposToWorldpos (Input.mousePosition);
			if (pos.x < 0 && pos.z > 3 && pos.z < 12) {
				inputPosZ = pos.z;
				isDrow = true;
				pmc.DoDrowRail = true;
				//animation停止
				anim.Stop ();
				SlideGuide.enabled = false;

			}
		}

		if (isDrow) {
			pmc.MainProcess ();

			if (Input.GetMouseButtonUp (0) || (isDrow && pmc.touchcnt == 0)) {
				isDrow = false;
				Vector3 pos;
				pos = MouseposToWorldpos (Input.mousePosition);
				if (pos.x > 4.2 && pos.z > inputPosZ) {
					//描いてほしい通りに描いてくれたので、その後の処理

					//成功音の再生処理を追加...

					//ポーズを解いてレールを引けなくする
					GrobalClass.pause = false;
					pmc.DoDrowRail = false;

					//次の処理へ命令を出し、このスクリプトを消す
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

	Vector3 MouseposToWorldpos (Vector3 mousePos) {
		Vector3 mp = Input.mousePosition;
		mp.z = 20;
		Vector3 wptmp = Camera.main.ScreenToWorldPoint (mp);
		Vector3 cp = Camera.main.transform.position;
		return (wptmp - cp) * cp.y / (cp.y - wptmp.y) + cp;
	}
}

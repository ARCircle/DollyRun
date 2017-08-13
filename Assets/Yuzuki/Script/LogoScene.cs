using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoScene : MonoBehaviour {

	private Image LogoMark;
	public Sprite[] logo;
	private Color c;
	int i = 0;
	float count = 0;
	public float fadeTime;
	public float showTime;

	// Use this for initialization
	void Start () {
		LogoMark = this.GetComponent<Image> ();
		c = new Color (255, 255, 255, 0);
		LogoMark.sprite = logo [i];

	}

	// Update is called once per frame
	void Update () {
		if (count <= fadeTime) {
			//フェードイン
			c.a = count / fadeTime;
			LogoMark.color = c;
			//Debug.Log ("fadein");
		} else {
			if (count > fadeTime * 2 + showTime) {

				i++;

				//シーン遷移
				if (i > logo.Length - 1) {
					if (count > fadeTime * 2 + showTime + 1) {
						UnityEngine.SceneManagement.SceneManager.LoadScene ("OpeningScene");
						return;
					} else {
						count += Time.deltaTime;
						return;
					}
				}
				//ロゴマーク変更
				count = 0;
				LogoMark.sprite = logo [i];
			} else if (count > fadeTime + showTime) {
				//フェードアウト
				c.a = 1 - ((count - (fadeTime + showTime)) / fadeTime);
				LogoMark.color = c;
				//Debug.Log ("fadeout");
			}
			//else Debug.Log ("wait");

		}
		count += Time.deltaTime;

		if (Input.GetMouseButtonDown (0)) {
			skipLogo ();
		}
	}


	void skipLogo() {
		i++;
		if (i > logo.Length - 1) {
			UnityEngine.SceneManagement.SceneManager.LoadScene ("OpeningScene");
			return;
		}
		count = fadeTime;
		LogoMark.sprite = logo [i];
	}
}

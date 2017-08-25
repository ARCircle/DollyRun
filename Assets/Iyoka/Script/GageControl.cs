using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GageControl : MonoBehaviour {
	public Text meter;
	public Text coin;
	public Image gageA;
	public Image gageR;

	// Update is called once per frame
	void Update () {
		meter.text = Mathf.FloorToInt(GrobalClass.distance).ToString ();
		coin.text = Mathf.FloorToInt(GrobalClass.coins).ToString ();
		/*if (GrobalClass.usingAtime <= 0f) {
			gageA.fillAmount = ItemStatus.status_A / 3f;
		} else {
			gageA.fillAmount = GrobalClass.usingAtime / 9f;
		}
		if (GrobalClass.usingRtime <= 0f) {
			gageR.fillAmount = ItemStatus.status_R / 3f;
		} else {
			gageR.fillAmount = GrobalClass.usingRtime / 9f;
		}*/
		gageA.fillAmount = GrobalClass.usingAtime / 9f;
		gageR.fillAmount = GrobalClass.usingRtime / 9f;
	}
}

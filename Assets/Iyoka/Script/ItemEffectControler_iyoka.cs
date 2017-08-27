using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffectControler_iyoka : MonoBehaviour {

	private Animator[] anims_A;
	private Animator[] anims_R;
	private Animator tonkachiAnim;
	private Animator yajirusiAnim;
	private Animator flickTextAnim;

	private ParticleSystem[] ps_GetA;
	private ParticleSystem[] ps_GetR;
	private ParticleSystem[] ps_UseA;
	private ParticleSystem[] ps_UseR;
	private ParticleSystem[] ps_IdleA;
	private ParticleSystem[] ps_IdleR;

	private bool touchOK;
	private float touchPos_y;

	private AudioSource sound1;
	private AudioSource sound2;


	void Start () {
		anims_A = new Animator[3];
		for (int i = 0; i < 3; i++) {
			anims_A [i] = GameObject.Find ("crystal_image_A" + i).GetComponent<Animator> ();
		}
		anims_R = new Animator[3];
		for (int i = 0; i < 3; i++) {
			anims_R [i] = GameObject.Find ("crystal_image_R" + i).GetComponent<Animator> ();
		}
		tonkachiAnim = GameObject.Find ("tonkachi").GetComponent<Animator> ();
		yajirusiAnim = GameObject.Find ("yajirusi").GetComponent<Animator> ();
		flickTextAnim = GameObject.Find ("flickText").GetComponent<Animator> ();

		ps_UseA = new ParticleSystem[3];
		for (int i = 0; i < 3; i++) {
			ps_UseA [i] = GameObject.Find ("crystal_image_A" + i).transform.Find ("ps_Use").gameObject.GetComponent<ParticleSystem> ();
		}
		ps_UseR = new ParticleSystem[3];
		for (int i = 0; i < 3; i++) {
			ps_UseR [i] = GameObject.Find ("crystal_image_R" + i).transform.Find ("ps_Use").gameObject.GetComponent<ParticleSystem> ();
		}
		ps_GetA = new ParticleSystem[3];
		for (int i = 0; i < 3; i++) {
			ps_GetA [i] = GameObject.Find ("crystal_image_A" + i).transform.Find ("ps_Get").gameObject.GetComponent<ParticleSystem> ();
		}
		ps_GetR = new ParticleSystem[3];
		for (int i = 0; i < 3; i++) {
			ps_GetR [i] = GameObject.Find ("crystal_image_R" + i).transform.Find ("ps_Get").gameObject.GetComponent<ParticleSystem> ();
		}
		ps_IdleA = new ParticleSystem[3];
		for (int i = 0; i < 3; i++) {
			ps_IdleA [i] = GameObject.Find ("crystal_image_A" + i).transform.Find ("ps_Idle").gameObject.GetComponent<ParticleSystem> ();
		}
		ps_IdleR = new ParticleSystem[3];
		for (int i = 0; i < 3; i++) {
			ps_IdleR [i] = GameObject.Find ("crystal_image_R" + i).transform.Find ("ps_Idle").gameObject.GetComponent<ParticleSystem> ();
		}

		AudioSource[] audiosources = GetComponents<AudioSource> ();
		sound1 = audiosources [1];
		sound2 = audiosources [2];
	}


	void Update () {
		if (ItemStatus.status_A != 0 || ItemStatus.status_R != 0) {
			UseItem ();
		}
	}


	//アイテムを得る
	public void GetItem(string type) {
		if (type == "A") {
			if (ItemStatus.status_A == 0) {
				if (ItemStatus.status_R == 0) {
					tonkachiAnim.SetTrigger ("SetActive");
					yajirusiAnim.SetTrigger ("SetActive");
					flickTextAnim.SetTrigger ("SetActive");
				}
			}
			if (ItemStatus.status_A < 3) {
				anims_A [ItemStatus.status_A].SetTrigger ("SetActive");
				ps_GetA [ItemStatus.status_A].Play ();
				ps_IdleA [ItemStatus.status_A].Play ();
				sound1.Play ();
				ItemStatus.status_A++;
			}
		}

		if (type == "R") {
			if (ItemStatus.status_R == 0) {
				if (ItemStatus.status_A == 0) {
					tonkachiAnim.SetTrigger ("SetActive");
					yajirusiAnim.SetTrigger ("SetActive");
					flickTextAnim.SetTrigger ("SetActive");
				}
			}
			if (ItemStatus.status_R < 3) {
				anims_R [ItemStatus.status_R].SetTrigger ("SetActive");
				ps_GetR [ItemStatus.status_R].Play ();
				ps_IdleR [ItemStatus.status_R].Play ();
				sound1.Play ();
				ItemStatus.status_R++;
			}
		}
	}


	//アイテムを使う
	void UseItem() {
		if (!GrobalClass.gameover && !GrobalClass.pause) {
			if (Flick () == true) {
				UseItemProcess ();
			}
		}
	}
	public void UseItemProcess () {
		tonkachiAnim.SetTrigger ("Use");
		yajirusiAnim.SetTrigger ("SetFalse");
		flickTextAnim.SetTrigger ("SetFalse");

		for (int i = 0; i < ItemStatus.status_A; i++) {
			anims_A [i].SetTrigger ("UseA");
			ps_IdleA [i].Stop ();
			ps_UseA [i].Play ();
			sound2.Play ();
			GrobalClass.usingAtime += 3f;
		}
		for (int i = 0; i < ItemStatus.status_R; i++) {
			anims_R [i].SetTrigger ("UseR");
			ps_IdleR [i].Stop ();
			ps_UseR [i].Play ();
			sound2.Play ();
			GrobalClass.usingRtime += 3f;
		}

		ItemStatus.status_A = 0;
		ItemStatus.status_R = 0;
	}


	//フリック感知
	public bool Flick() {
		if (Input.GetMouseButtonDown (0) && touchOK == false) {
			if (Input.mousePosition.y < Screen.height / 5) {
				touchOK = true;
				touchPos_y = Input.mousePosition.y;
			}
		}
		if (touchOK == true) {
			if (Input.mousePosition.y > touchPos_y) {
				touchOK = false;
				return true;
			}
		}
		if (Input.GetMouseButtonUp (0)) {
			touchOK = false;
		}
		return false;
	}
}

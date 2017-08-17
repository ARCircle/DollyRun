using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControl : MonoBehaviour {
	int touchcnt = 0, railcnt = 0;
	const int r_limit = 20, p_limit = 200;
	const float playerline = 1f;
	float touchtime = 0f, limit = 0.5f;
	float[,] StageRail = {{-5f, -4f}, {-0.5f, 0.5f}, {4f, 5f}};

	GameObject Body;
	GameObject[] rrrr = new GameObject[r_limit]; // Rail RendeReR
	RState[] RS = new RState[r_limit];
	Vector3 mp, wp, wptmp, cp;

	void Start () {
		GrobalClass.RideRailNum = 2;
		rrrr [0] = transform.parent.Find ("RailRenderer").gameObject;
		for (int i = 0; i < r_limit; i++) {
			Body = transform.Find ("Trokko").gameObject;
			if (i > 0) {
				rrrr [i] = Instantiate<GameObject> (rrrr [0]);
				rrrr [i].transform.SetParent (this.transform);
			}
			RS [i] = new RState ();
			RS [i].myrrrr = rrrr [i].transform; 
			RS [i].LR = rrrr [i].GetComponent<LineRenderer> ();
			RS [i].MD = rrrr [i].GetComponent<MoveDown> ();
		}
	}

	void Update () {
		if (!GrobalClass.gameover && !GrobalClass.pause) {
			
			// 速度上昇、距離計算
			GrobalClass.playtime += Time.deltaTime;
			GrobalClass.distance += GrobalClass.speed * Time.deltaTime;
			if (GrobalClass.playtime - GrobalClass.speedlevel * 0.4f > 0f) {
				GrobalClass.speed += 0.01f;
				GrobalClass.speedlevel += 1;
			}

			// トロッコ操作
			if (Input.GetMouseButtonDown (0)) {
				touchtime = 0f;
			}
			if (Input.GetMouseButton (0) && touchtime < limit) {
				// ワールド座標の取得
				mp = Input.mousePosition;
				if (mp.y < Screen.height / 5) {
					touchtime += limit;
				}
				mp.z = 20f;
				wptmp = Camera.main.ScreenToWorldPoint (mp);
				cp = Camera.main.transform.position;
				wp = (wptmp - cp) * cp.y / (cp.y - wptmp.y) + cp - rrrr [railcnt].transform.position;
				// 描画の制限、レール交点の取得
				if (touchcnt > 0) {
					if (wp.z < RS [railcnt].Points [touchcnt - 1].z) {
						wp.z = RS [railcnt].Points [touchcnt - 1].z;
					}
					if (wp.x < -5.5f) {
						wp.x = -5.5f;
					} else if (wp.x > 5.5f) {
						wp.x = 5.5f;
					}					
					float ccz = 0f;
					int ccnum = CrossCheck (RS [railcnt].Points [touchcnt - 1], wp, ref ccz);
					if (ccnum > 0) {
						RS [railcnt].SetCrossPoint (ccnum, ccz);//RS [railcnt].Points [touchcnt - 1].z);
						RS [railcnt].CFpoint = touchcnt;
						//if (RS [railcnt].CSpoint < 0)
						//	RS [railcnt].CSpoint = touchcnt - 1;
					}
				}
				// 描画
				RS [railcnt].Points [touchcnt] = wp;
				for (int i = touchcnt; i < p_limit; i++) {  // レール終端の追従
					RS [railcnt].LR.SetPosition (i, wp);
				}
				touchcnt++;
				RS [railcnt].Fin = touchcnt;
				touchtime += Time.deltaTime;
			} else if (touchcnt > 0) {
				//レール始端
				/*int strp = RS [railcnt].CSpoint;
				for (int i = 0; i < strp; i++) {
					RS [railcnt].LR.SetPosition (i, RS [railcnt].LR.GetPosition (strp));
				}*/
				//レール終端
				int finp = RS [railcnt].CFpoint;
				for (int i = finp; i < p_limit; i++) {
					RS [railcnt].LR.SetPosition (i, RS [railcnt].LR.GetPosition (finp));
				}
				touchcnt = 0;
				railcnt = (railcnt + 1) % r_limit;
				RS [railcnt].Reset ();
			}
			for (int i = 0; i < r_limit; i++) {
				RS [i].CheckKP ();
				if (RS [i].KPenable) {
					int index = RS [i].FindCrossPoint (GrobalClass.RideRailNum);
					if (index >= 0 && RS [i].CrossZ [index] + RS [i].myrrrr.position.z <= playerline) {
						if (RS [i].GetLastCrossZ () + RS [i].myrrrr.position.z <= playerline) { // 降りるとき
							GrobalClass.RideRailNum = RS [i].GetLastCrossR ();
							RS [i].Riding = false;
							int j = GrobalClass.RideRailNum - 1;
							Body.transform.rotation = Quaternion.Euler (0f, 0f, 0f);
							Body.transform.position = new Vector3 ((StageRail [j, 0] + StageRail [j, 1]) / 2f, 0f, playerline);
						} else { // 乗るとき、乗ってるとき
							RS [i].Riding = true;
							GrobalClass.RideRailNum = -1;
							Body.transform.Translate (RS [i].KeyPoint - Body.transform.position);
							Body.transform.LookAt (RS [i].NextPoint);
							Body.transform.position = new Vector3 (Body.transform.position.x, Body.transform.position.y, 1f);
						}
					}
					RS [i].DeleteCrossPoint ();
				}
			}
		}
	}

	int CrossCheck(Vector3 a, Vector3 b, ref float rtz) {
		float x, y, chg, zrate;
		if (a.x < b.x) {
			x = a.x; y = b.x; chg = 0f;
		} else {
			x = b.x; y = a.x; chg = 1f;
		}
		for (int i = 0; i < 3; i++) {
			if (StageRail [i, 0] <= x && x <= StageRail [i, 1]) {
				zrate = chg;
				rtz = (b.z - a.z) * zrate + a.z;
				return i + 1;
			} else if (StageRail [i, 0] <= y && y <= StageRail [i, 1]) {
				zrate = 1f - chg;
				rtz = (b.z - a.z) * zrate + a.z;
				return i + 1;
			} else if (x < StageRail [i, 0] && StageRail [i, 1] < y) {
				float center = (StageRail [i, 0] + StageRail [i, 1]) / 2;
				zrate = (center - a.x) / (b.x - a.x);
				rtz = (b.z - a.z) * zrate + a.z;
				return i + 1;
			}
		}
		return 0;
	}

	class RState {
		const int crosslimit = 30;
		private int CrossStart = 0, CrossFin = 0;
		public int Fin = 0, CFpoint = 0;//, CSpoint = -1;
		public int[] CrossR = new int[crosslimit];
		public float[] CrossZ = new float[crosslimit];
		public bool KPenable = false, StopPoint = false, Riding = false;
		public Vector3 KeyPoint = Vector3.zero;
		public Vector3 NextPoint = Vector3.zero;
		public Transform myrrrr;
		public LineRenderer LR;
		public MoveDown MD;
		public Vector3[] Points = new Vector3[p_limit];

		public void Reset () {
			this.LR.SetPositions(new Vector3[p_limit]);
			this.MD.Reset ();
			this.Fin = 0; this.CrossStart = 0;
			this.CrossFin = 0; this.CFpoint = 0; //this.CSpoint = -1;
			for (int i = 0; i < crosslimit; i++) {
				this.CrossR[i] = 0;
				this.CrossZ[i] = 0;
			}
			StopPoint = false;
			Riding = false;
		}

		public int GetLastCrossR () {
			return CrossR [CrossFin - 1];
		}

		public float GetLastCrossZ () {
			return CrossZ [CrossFin - 1];
		}

		public void SetCrossPoint (int r, float z){
			CrossR [CrossFin] = r;
			CrossZ [CrossFin] = z;
			CrossFin++;
		}

		public int FindCrossPoint (int plc) {
			if (plc < 0) {
				if (Riding) {
					return CrossStart;
				} else {
					return -1;
				}
			}
			for (int i = CrossStart; i < CrossFin; i++) {
				if (CrossR [i] == plc) {
					return i;
				}
			}
			return -1;
		}

		public void DeleteCrossPoint () {
			for (int i = CrossStart; i < CrossFin; i++) {
				if (CrossZ [i] + myrrrr.position.z < playerline && !Riding && CrossR [i] != GrobalClass.RideRailNum) {
					CrossStart += 1;
				}
			}
		}

		public void CheckKP() {
			KPenable = false;
			for (int i = Fin - 1; i >= 0; i--) {
				float pi = Points [i].z + myrrrr.position.z;
				if (pi < playerline) {
					int j = i + 1;
					if (j == Fin) {
						if (!StopPoint) {
							KPenable = true;
							StopPoint = true;		
							KeyPoint = Points [Fin - 1] + myrrrr.position;
							NextPoint = KeyPoint;
						}
					} else {
						float pj = Points [j].z + myrrrr.position.z;
						float rate = (playerline - pi) / (pj - pi);
						KPenable = true;
						KeyPoint = (Points [j] - Points [i]) * rate + Points [i] + myrrrr.position;
						NextPoint = Points [j] + myrrrr.position;
					}
					break;
				} else if (pi == playerline) {
					Debug.Log ("moving");
					KPenable = true;
					KeyPoint = Points [i] + myrrrr.position;
					NextPoint = KeyPoint;
					break;
				}
			}
		}
	}
}







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
		if (Input.GetMouseButtonDown(0)) {
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
			wp = (wptmp - cp) * cp.y / (cp.y - wptmp.y) + cp - rrrr[railcnt].transform.position;
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
				int ccnum = CrossCheck(RS [railcnt].Points [touchcnt - 1].x, wp.x);
				if (ccnum > 0) {
					RS [railcnt].SetCrossPoint (ccnum, RS [railcnt].Points [touchcnt - 1].z);
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
			//レール終端
			touchcnt = 0;
			railcnt = (railcnt + 1) % r_limit;
			RS [railcnt].Reset ();
		}
		for (int i = 0; i < r_limit; i++) {
			RS [i].CheckKP ();
			if (RS [i].KPenable) {
				int index = RS [i].FindCrossPoint (GrobalClass.RideRailNum);
				if (index >= 0 && RS [i].CrossZ [index] + RS [i].myrrrr.position.z <= playerline) {
					if (RS [i].GetLastCrossZ () + RS [i].myrrrr.position.z <= playerline) {
						GrobalClass.RideRailNum = RS [i].GetLastCrossR ();
						RS [i].Riding = false;
						int j = GrobalClass.RideRailNum - 1;
						Body.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
						Body.transform.position = new Vector3 ((StageRail [j, 0] + StageRail [j, 1]) / 2f, 0f, playerline);
					} else {
						GrobalClass.RideRailNum = -1;
						RS [i].Riding = true;
						//Body.transform.LookAt(RS [i].NextPoint);
						Body.transform.Translate (RS [i].KeyPoint - Body.transform.position);
					}
				}
			}
		}
	}

	int CrossCheck(float a, float b) {
		float x, y;
		if (a < b) {
			x = a;
			y = b;
		} else {
			x = b;
			y = a;
		}
		for (int i = 0; i < 3; i++) {
			if ((StageRail [i, 0] <= x && x <= StageRail [i, 1]) || 
				(StageRail [i, 0] <= y && y <= StageRail [i, 1]) ||
				(x < StageRail [i, 0] && StageRail [i, 1] < y)){
				return i + 1;
			}
		}
		return 0;
	}

	class RState {
		const int crosslimit = 30;
		public int Fin = 0, CrossFin = 0;
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
			this.Fin = 0; this.CrossFin = 0;
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
					return 0;
				} else {
					return -1;
				}
			}
			for (int i = 0; i < crosslimit; i++) {
				if (CrossR [i] == plc) {
					return i;
				}
			}
			return -1;
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
						//for (; j < Fin; j++) {
						float pj = Points [j].z + myrrrr.position.z;
						//	if (pj >= playerline) {
						float rate = (playerline - pi) / (pj - pi);
						KPenable = true;
						KeyPoint = (Points [j] - Points [i]) * rate + Points [i] + myrrrr.position;
						NextPoint = Points [j] + myrrrr.position;
						//		break;
						//	}
						//}
					}
					break;
				} else if (pi == playerline) {
					KPenable = true;
					KeyPoint = Points [i] + myrrrr.position;
					NextPoint = KeyPoint;
					break;
				}
			}
		}
	}
}







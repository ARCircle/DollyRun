using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour {

    public float thresholdTime = 1f;
    public GameObject[] childButton;
    public GameObject pauseButton;
    bool isButtonPushed = false;
    bool isRestarting = false;
    bool isToTitle = false;
    bool fading = false;
    Animator[] anims;
    Fader fade;// = new Fader ();

    // Use this for initialization
    void Start() {
        fade = gameObject.AddComponent<Fader>();
        anims = transform.root.GetComponentsInChildren<Animator>();
        StartCoroutine(fade.blackout(1f, DeletePanel));
    }

    // Update is called once per frame
    void Update() {
        if (isButtonPushed) {
            thresholdTime -= Time.deltaTime;
            if (thresholdTime < 0f) {
                if (isRestarting)
                    SceneManager.LoadScene("GameScene");
                else if (isToTitle)
                    SceneManager.LoadScene("OpeningScene");
                GrobalClass.Reset();
            } else if (thresholdTime < 1f && !fading) {
                fading = true;
                StartCoroutine(fade.blackin(1f, DeletePanel));
            }
        }
    }

    public void RestartButton() {
        isButtonPushed = true;
        isRestarting = true;
        for (int i = 0; i < childButton.Length; i++) {
            childButton[i].SetActive(false);
        }
        pauseButton.SetActive(false);
    }

    public void ToTitleButton() {
        isButtonPushed = true;
        isToTitle = true;
        for (int i = 0; i < childButton.Length; i++) {
            childButton[i].SetActive(false);
        }
        pauseButton.SetActive(false);
    }

    void DeletePanel() {
        Destroy(GameObject.Find("BlackPlate(Clone)"));
    }
}

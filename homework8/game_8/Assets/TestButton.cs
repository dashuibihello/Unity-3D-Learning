using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButton : MonoBehaviour {

    private Button myButton;
    public Text text;
    private int frame = 100;

    // Use this for initialization  
    void Start()
    {
        myButton = gameObject.GetComponent<Button>();
        myButton.onClick.AddListener(TaskOnClick);
        text.gameObject.SetActive(false);
    }

    IEnumerator Folding() {
        float x = 0;
        float y = 600;
        for (int i = 0; i < frame; i++) {
            x -= 90f / frame;
            y -= 600f / frame;
            text.transform.rotation = Quaternion.Euler(x, 0, 0);
            text.rectTransform.sizeDelta = new Vector2(text.rectTransform.sizeDelta.x, y);
            if (i == frame - 1) {
                text.gameObject.SetActive(false);
            }
            yield return null;
        }
    }

    IEnumerator Spread() {
        float x = -90;
        float y = 0;
        for (int i = 0; i < frame; i++) {
            x += 90f / frame;
            y += 600f / frame;
            text.transform.rotation = Quaternion.Euler(x, 0, 0);
            text.rectTransform.sizeDelta = new Vector2(text.rectTransform.sizeDelta.x, y);
            if (i == 0) {
                text.gameObject.SetActive(true);
            }
            yield return null;
        }
    }


    void TaskOnClick()
    {
        if (text.gameObject.activeSelf) {
            StartCoroutine(Folding());
        }
        else {
            StartCoroutine(Spread());
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserGUI : MonoBehaviour {
    public Text mytext;
    string c;
    float width = Screen.width / 6;
    float height = Screen.height / 16;
    float y = 4 * Screen.height / 5;
    private IUserAction actions;
    private FirstController myobj;
    // Use this for initialization
    void Start () {
        myobj = SSDirector.getInstance().currentSceneController as FirstController;
        actions = SSDirector.getInstance().currentSceneController as IUserAction;
        //mytext = this.transform.Find("Canvas").Find("mytext").GetComponent<Text>();
        c = "";
    }

    void OnGUI() {
        if (myobj.state != FirstController.State.MOVING)
        {
            if (GUI.Button(new Rect(Screen.width / 20, y, width, height), "Devil Gets On"))
            {
                actions.DevilGeton();
            }
            if (GUI.Button(new Rect(Screen.width / 20 + width, y, width, height), "Left Gets Off"))
            {
                actions.LeftGetoff();
            }
            if (GUI.Button(new Rect(Screen.width / 20 + width * 2, y, width * 3 / 2, height), "Go To The Other Side"))
            {
                actions.RowingBoat();
            }
            if (GUI.Button(new Rect(Screen.width / 20 + width / 2 + width * 3, y, width, height), "Right Gets Off"))
            {
                actions.RightGetoff();
            }
            if (GUI.Button(new Rect(Screen.width / 20 + width / 2 + width * 4, y, width, height), "Priest Gets On"))
            {
                actions.PriestGeton();
            }
            if (actions.isWin())
            {
                if (GUI.Button(new Rect(Screen.width / 5 * 2, Screen.height / 10, Screen.width / 5, Screen.height / 6), "Win!Click here and Restart"))
                {
                    actions.Restart();
                }
            }
            if (actions.isLose())
            {
                if (GUI.Button(new Rect(Screen.width / 5 * 2, Screen.height / 10, Screen.width / 5, Screen.height / 6), "Lose!Click here and Restart"))
                {
                    actions.Restart();
                }
            }
            if (!actions.isLose() && !actions.isWin())
            {
                if (GUI.Button(new Rect(Screen.width / 5 * 2, Screen.height / 10, Screen.width / 5, Screen.height / 6), "Playing!Click here and Restart"))
                {
                    actions.Restart();
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
       
    }
}

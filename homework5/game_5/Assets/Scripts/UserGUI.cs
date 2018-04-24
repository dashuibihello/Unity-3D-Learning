using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UserGUI : MonoBehaviour
{
    float width = Screen.width / 5;
    float height = Screen.height / 10;
    float y = 4 * Screen.height / 5;
    private IUserAction actions;
    private FirstController myobj;
    public Text MainText;
    public Text ScoreText;
    public Text RoundText;
    public Text RuleText;
    string temp;

    // Use this for initialization
    void Start()
    {
        myobj = SSDirector.getInstance().currentSceneController as FirstController;
        actions = SSDirector.getInstance().currentSceneController as IUserAction;
        temp = RuleText.text;
    }

    void OnGUI()
    {
        ScoreText.text = "Score: " + actions.getScore().ToString();
        RoundText.text = "Round: " + actions.getRound().ToString();
        if (myobj.state == FirstController.State.STOP || myobj.state == FirstController.State.REST)
        {
            if (myobj.state == FirstController.State.STOP)
                RuleText.text = temp;
            if (myobj.state == FirstController.State.STOP && actions.getRound() == 0)
            {
                if(myobj.isUsePsy == false)
                {
                    if (GUI.Button(new Rect(Screen.width / 5 + width * 2, y, width, height), "Use Physics"))
                    {
                        actions.usePsy();
                    }
                }    
            }
            if (GUI.Button(new Rect(Screen.width / 5 + width, y, width, height), "Start / Next Round"))
            {
                actions.Startgame();
            }
        }
        else if(myobj.state == FirstController.State.COUNTDOWN)
        {
            RuleText.text = "";
            MainText.text = ((int)myobj.getCountdown()).ToString();
        }
        else if(myobj.state == FirstController.State.PLAYING)
        {
            MainText.text = "";
            if (Input.GetButtonDown("Fire1"))
            {
                Vector3 pos = Input.mousePosition;
                actions.Click(pos);
            }   
            if (GUI.Button(new Rect(Screen.width / 5 + width, y, width, height), "Restart"))
            {
                actions.Restart();
            }
        }
        else if(myobj.state == FirstController.State.LOSE)
        {
            if (GUI.Button(new Rect(Screen.width / 5 + width, y, width, height), "You Lose!Click here and Restart"))
            {
                actions.Restart();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}

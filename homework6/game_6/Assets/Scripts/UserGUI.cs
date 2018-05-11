using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UserGUI : MonoBehaviour {

    private IUserAction actions;
    private IScore score;

    public Text ScoreText;


    // Use this for initialization
    void Start()
    {
        actions = SSDirector.getInstance().currentSceneController as IUserAction;
        score = SSDirector.getInstance().currentSceneController as IScore;
    }

    void OnGUI()
    {
        ScoreText.text = "Score: " + score.getScore().ToString();
        GUI.Label(new Rect(4 * Screen.width / 5, 2 * Screen.height / 5, Screen.width / 8, Screen.height / 2), "将右下角的白色方块移出左下角缺口就完成游戏.巡逻兵在设定范围内感知到玩家，会自动追击玩家计分：玩家每次甩掉一个巡逻兵计一分，与巡逻兵碰撞游戏结束；");
        if (!actions.GameStart())
        {
            if (GUI.Button(new Rect(4 * Screen.width / 5, Screen.height / 5, Screen.width / 9, Screen.width / 16), "Start"))
            {
                actions.StartGame();
            }
            
        }
        if(actions.GameOver() || actions.GameStart())
        {
            if (GUI.Button(new Rect(4 * Screen.width / 5, Screen.height / 5, Screen.width / 9, Screen.width / 16), "Restart"))
            {
                actions.Restart();
            }
        }
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        move(h, v);
        turn(h, v);
    }

    public void move(float h, float v)
    {
        actions.movePlayer(h, v);
    }

    public void turn(float h, float v)
    {
        if (h != 0 || v != 0)
        {
            actions.setDirection(h, v);
        }
    }

}

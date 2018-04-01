using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseBeh : MonoBehaviour {

    bool[] chessarea = new bool[9];
    string[] str = new string[9];
    int isPlayerone = 1;
    int[,] Game = new int[3, 3];
    int winner = 0;
    int time = 0;

    // Use this for initialization
    void Start ()
    {

	}

    void OnGUI() {
        //九个按钮的函数
        for (int row = 0; row < 3; row++)
            for (int col = 0; col < 3; col++)
            {
                if (GUI.Button(new Rect((col + 3) * 100, (row * 100) + 50, 100, 100), str[col + row * 3]) && !chessarea[col + row * 3] && winner == 0)
                {
                    if (isPlayerone == 1)
                        str[col + row * 3] = "⊙";
                    else
                        str[col + row * 3] = "√";
                    chessarea[col + row * 3] = true;
                    Game[row, col] = isPlayerone;
                    winner = isWin(col + row * 3, isPlayerone);
                    isPlayerone = -isPlayerone;
                }
            }
       
        //重新开始
        if (GUI.Button(new Rect(650, 200, 200, 100), "Click here and restart!"))
        {
            winner = 0;
            Array.Clear(chessarea, 0, 9);
            Array.Clear(str, 0, 9);
            for (int a = 0; a < 3; a++)
                for (int b = 0; b < 3; b++)
                    Game[a, b] = 0;
            isPlayerone = 1;
            time = 0;
        }

        //游戏进行
        if(winner == 0 && time < 9)
        {
            if (isPlayerone == -1)
                GUI.Box(new Rect(650, 100, 200, 50), "Player2");
            else
                GUI.Box(new Rect(50, 100, 200, 50), "Player1");
        }

        //游戏结束
        else
        {
            if (winner != 0)
            {
                if (winner == -1)
                    winner = 2;
                GUI.Box(new Rect(50, 300, 200, 100), "Player" + winner + " is winner!");
            }
            else
                GUI.Box(new Rect(50, 300, 200, 100), "Draw!");
        }
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    //判断是否胜利
    int isWin(int num, int player)
    {
        time++;
        int row = num / 3;
        int col = num % 3;
        for (int a =1; a <= 2; a++)
        {
            if (Game[(row + a) % 3, col] != player)
                break;
            if (a == 2)
                return player;
        }
        for (int a =1; a <= 2; a++)
        {
            if (Game[row, (col + a) % 3] != player)
                break;
            if (a == 2)
                return player;
        }
        if(col == row)
        {
            for (int a =1; a <= 2; a++)
            {
                if (Game[(row + a) % 3, (col + a) % 3] != player)
                    break;
                if (a == 2)
                    return player;
            }
        }
        if(col + row == 2)
        {
            for (int a =1; a <= 2; a++)
            {
                if (Game[(row + a) % 3, (col - a + 3) % 3] != player)
                    break;
                if (a == 2)
                    return player;
            }
        }
        return 0;
    }

}

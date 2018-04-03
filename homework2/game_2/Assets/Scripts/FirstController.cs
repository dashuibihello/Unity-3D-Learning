using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class FirstController : MonoBehaviour, IUserAction, ISceneController
{
    //游戏从左开始
    Stack<GameObject> priests_left = new Stack<GameObject>();
    Stack<GameObject> priests_right = new Stack<GameObject>();
    Stack<GameObject> devils_left = new Stack<GameObject>();
    Stack<GameObject> devils_right = new Stack<GameObject>();
    GameObject Boat;
    GameObject[] boatchair = new GameObject[2];

    Vector3 priestLeftPos = new Vector3(-15.5f, 2, 0);
    Vector3 priestRightPos = new Vector3(13.5f, 2, 0);
    Vector3 devilStartPos = new Vector3(-10.5f, 2, 0);
    Vector3 devilRightPos = new Vector3(8.5f, 2, 0);

    Vector3 BoatLeftPos = new Vector3(-4, 0, 0);
    Vector3 BoatRightPos = new Vector3(4, 0, 0);

    Vector3 BoatFirstClass = new Vector3(-0.25f, 1, 0);
    Vector3 BoatSecondClass = new Vector3(0.25f, 1, 0);

    Vector3 object_move = new Vector3(1, 0, 0);

    public State state = State.LEFT;
    public enum State { LEFT, LEFTTORIGHT, RIGHTTOLEFT, RIGHT, WIN, LOSE };
    public enum Identity { PRIEST, DEVIL, NONE};
    public Identity[] BoatIdentity = { Identity.NONE, Identity.NONE };

    void Awake()
    {
        SSDirector director = SSDirector.getInstance();
        director.currentSceneController = this;
        director.currentSceneController.LoadResources();
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        setPostion(priests_left, priestLeftPos, object_move);
        setPostion(devils_left, devilStartPos, object_move);
        setPostion(priests_right, priestRightPos, object_move);
        setPostion(devils_right, devilRightPos, object_move);

        if (this.state == State.LEFTTORIGHT)
        {
            Boat.transform.position = Vector3.MoveTowards(Boat.transform.position, BoatRightPos, 3 * Time.deltaTime);
            if (Boat.transform.position == BoatRightPos)
            {
                this.state = State.RIGHT;
            }
        }
        else if (this.state == State.RIGHTTOLEFT)
        {
            Boat.transform.position = Vector3.MoveTowards(Boat.transform.position, BoatLeftPos, 3 * Time.deltaTime);
            if (Boat.transform.position == BoatLeftPos)
            {
                this.state = State.LEFT;
            }
        }
        else
        {
            judge();
        }
    }

    void setPostion(Stack<GameObject> obj_stack, Vector3 first, Vector3 move)
    {
        GameObject[] temp = obj_stack.ToArray();
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i].transform.localPosition = first + i * move;
        }
    }

    int getBoatPassenger()
    {
        int num = 0;
        for (int i = 0; i < 2; i++)
        {
            if (boatchair[i] == null)
                num++;
        }
        return num;
    }

    public void RowingBoat()
    {
        if (getBoatPassenger() != 2)
        {
            if (this.state == State.LEFT)
                this.state = State.LEFTTORIGHT;
            else if (this.state == State.RIGHT)
                this.state = State.RIGHTTOLEFT;
        }
    }

    public void DevilGeton()
    {
        if (this.state == State.LEFT || this.state == State.RIGHT)
            if (getBoatPassenger() != 0)
            {
                if (this.state == State.LEFT)
                {
                    if(devils_left.Count != 0)
                    {
                        GameObject temp = devils_left.Pop();
                        temp.transform.parent = Boat.transform;
                        if (boatchair[0] == null)
                        {
                            boatchair[0] = temp;
                            temp.transform.localPosition = BoatFirstClass;
                            BoatIdentity[0] = Identity.DEVIL;
                        }
                        else
                        {
                            boatchair[1] = temp;
                            temp.transform.localPosition = BoatSecondClass;
                            BoatIdentity[1] = Identity.DEVIL;
                        }
                    }
                }
                else
                {
                    if(devils_right.Count != 0)
                    {
                        GameObject temp = devils_right.Pop();
                        temp.transform.parent = Boat.transform;
                        if (boatchair[0] == null)
                        {
                            boatchair[0] = temp;
                            temp.transform.localPosition = BoatFirstClass;
                            BoatIdentity[0] = Identity.DEVIL;
                        }
                        else
                        {
                            boatchair[1] = temp;
                            temp.transform.localPosition = BoatSecondClass;
                            BoatIdentity[1] = Identity.DEVIL;
                        }
                    }                   
                }
            }
    }

    public void PriestGeton()
    {
        if (this.state == State.LEFT || this.state == State.RIGHT)
            if (getBoatPassenger() != 0)
            {
                if (this.state == State.LEFT)
                {
                    if(priests_left.Count != 0)
                    {
                        GameObject temp = priests_left.Pop();
                        temp.transform.parent = Boat.transform;
                        if (boatchair[0] == null)
                        {
                            boatchair[0] = temp;
                            temp.transform.localPosition = BoatFirstClass;
                            BoatIdentity[0] = Identity.PRIEST;
                        }
                        else
                        {
                            boatchair[1] = temp;
                            temp.transform.localPosition = BoatSecondClass;
                            BoatIdentity[1] = Identity.PRIEST;
                        }
                    }
                }
                else
                {
                    if(priests_right.Count != 0)
                    {
                        GameObject temp = priests_right.Pop();
                        temp.transform.parent = Boat.transform;
                        if (boatchair[0] == null)
                        {
                            boatchair[0] = temp;
                            temp.transform.localPosition = BoatFirstClass;
                            BoatIdentity[0] = Identity.PRIEST;
                        }
                        else
                        {
                            boatchair[1] = temp;
                            temp.transform.localPosition = BoatSecondClass;
                            BoatIdentity[1] = Identity.PRIEST;
                        }
                    }
                }
            }
    }

    public void LeftGetoff()
    {
        if(this.state == State.LEFT || this.state == State.RIGHT)
        {
            if (BoatIdentity[0] != Identity.NONE)
            {
                if (BoatIdentity[0] == Identity.DEVIL)
                {
                    if (this.state == State.LEFT)
                        devils_left.Push(boatchair[0]);
                    else
                        devils_right.Push(boatchair[0]);
                }
                else
                {
                    if(this.state == State.LEFT)
                        priests_left.Push(boatchair[0]);
                    else
                        priests_right.Push(boatchair[0]);
                }
                boatchair[0].transform.parent = null;
                boatchair[0] = null;
                BoatIdentity[0] = Identity.NONE;
            }
        }
    }

    public void RightGetoff()
    {
        if (this.state == State.LEFT || this.state == State.RIGHT)
        {
            if (BoatIdentity[1] != Identity.NONE)
            {
                if (BoatIdentity[1] == Identity.DEVIL)
                {
                    if (this.state == State.LEFT)
                        devils_left.Push(boatchair[1]);
                    else
                        devils_right.Push(boatchair[1]);
                }
                else
                {
                    if (this.state == State.LEFT)
                        priests_left.Push(boatchair[1]);
                    else
                        priests_right.Push(boatchair[1]);
                }
                boatchair[1].transform.parent = null;
                boatchair[1] = null;
                BoatIdentity[1] = Identity.NONE;
            }
        }
    }

    public void Restart()
    {
        EditorSceneManager.LoadScene(EditorSceneManager.GetActiveScene().name);
        state = State.LEFT;
    }

    public void LoadResources()
    {
        GameObject MainPage = Instantiate(Resources.Load<GameObject>("prefabs/MainPage"), new Vector3(0, -1, 0), Quaternion.identity);
        Boat = Instantiate(Resources.Load<GameObject>("prefabs/Boat"), BoatLeftPos, Quaternion.identity);
        for (int i = 0; i < 3; ++i)
        {
            priests_left.Push(Instantiate(Resources.Load("prefabs/Priest")) as GameObject);
            devils_left.Push(Instantiate(Resources.Load("prefabs/Devil")) as GameObject);
        }
    }

    public void judge()
    {
        int P_Left = priests_left.Count, P_Right = priests_right.Count, D_Left = devils_left.Count, D_Right = devils_right.Count;
        int P_Boat = 0, D_Boat = 0;
        if(D_Right == 3 && P_Right == 3)
        {
            this.state = State.WIN;
            return;
        }
        for(int i = 0; i < 2; i++)
        {
            if (BoatIdentity[i] == Identity.PRIEST)
                P_Boat++;
            else if (BoatIdentity[i] == Identity.DEVIL)
                D_Boat++;
        }
        if(this.state == State.LEFT)
        {
            P_Left += P_Boat;
            D_Left += D_Boat;
            if(D_Left > P_Left && P_Left != 0)
            {
                this.state = State.LOSE;
            }
            if (D_Right > P_Right && P_Right != 0)
            {
                this.state = State.LOSE;
            }
        }
        else if(this.state == State.RIGHT)
        {
            P_Right += P_Boat;
            D_Right += D_Boat;
            if (D_Left > P_Left && P_Left != 0)
            {
                this.state = State.LOSE;
            }
            if (D_Right > P_Right && P_Right != 0)
            {
                this.state = State.LOSE;
            }
        }
    }

    public bool isWin()
    {
        if (this.state == State.WIN)
            return true;
        return false;
    }

    public bool isLose()
    {
        if (this.state == State.LOSE)
            return true;
        return false;
    }
}

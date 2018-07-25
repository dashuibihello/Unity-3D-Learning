# 牧师与魔鬼智能帮助
## 视频链接：
http://v.youku.com/v_show/id_XMzc0NDMwMjgyMA==.html
## 实现方法：
在原本的游戏上加了一个函数，根据当前状态来判断下一步的实现
### getNext()
	    public void getNext()
    {
        if (BoatIdentity[0] != Identity.NONE)
            LeftGetoff();
        if (BoatIdentity[1] != Identity.NONE)
            RightGetoff();

        int P_Left = priests_left.Count, P_Right = priests_right.Count, D_Left = devils_left.Count, D_Right = devils_right.Count;
        if (state == State.LEFT)
        {
            if(P_Left == 3 && D_Left == 3)
            {
                DevilGeton();
                DevilGeton();
                RowingBoat();
            }
            else if (P_Left == 3 && D_Left == 2)
            {
                DevilGeton();
                DevilGeton();
                RowingBoat();
            }
            else if (P_Left == 3 && D_Left == 1)
            {
                PriestGeton();
                PriestGeton();
                RowingBoat();
            }
            else if (P_Left == 2 && D_Left == 2)
            {
                PriestGeton();
                PriestGeton();
                RowingBoat();
            }
            else if (P_Left == 1 && D_Left == 1)
            {
                PriestGeton();
                RowingBoat();
            }
            else if (P_Left == 0 && D_Left == 3)
            {
                DevilGeton();
                DevilGeton();
                RowingBoat();
            }
            else if (P_Left == 0 && D_Left == 2)
            {
                DevilGeton();
                DevilGeton();
                RowingBoat();
            }
        }
        else if(state == State.RIGHT)
        {
            if(P_Right == 0 && D_Right == 2)
            {
                DevilGeton();
                RowingBoat();
            }
            else if (P_Right == 0 && D_Right == 3)
            {
                DevilGeton();
                RowingBoat();
            }
            else if (P_Right == 0 && D_Right == 1)
            {
                DevilGeton();
                RowingBoat();
            }
            else if (P_Right == 2 && D_Right == 2)
            {
                PriestGeton();
                DevilGeton();
                RowingBoat();
            }
            else if (P_Right == 3 && D_Right == 1)
            {
                DevilGeton();
                RowingBoat();
            }
            else if (P_Right == 3 && D_Right == 2)
            {
                DevilGeton();
                RowingBoat();
            }
        }
    }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public interface IUserAction {
        void RowingBoat();
        void DevilGeton();
        void PriestGeton();
        void LeftGetoff();
        void RightGetoff();
        void Restart();
        bool isWin();
        bool isLose();

        //AI
        void getNext();
}





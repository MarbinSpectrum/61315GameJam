using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailPopup : UI_Popup
{
    public override void ClosePopupUI()
    {
        base.ClosePopupUI();
        Managers.Game.CreateStage();
    }
}

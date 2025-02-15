using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessPopup : UI_Popup
{
  public override void ClosePopupUI()
  {
      base.ClosePopupUI();
      Managers.Game.StageNumber += 1;
      Managers.Game.CreateStage();
  }
}

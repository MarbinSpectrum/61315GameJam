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
  
  public override void Init()
  {
      base.Init();

      var mapData = Managers.Data.GetMapData(Managers.Game.StageNumber + 1);
      if (mapData == null)
      {
          //다음스테이지가 없는듯? 버튼 비활성화
          buttonClose.gameObject.SetActive(false);
      }
  }
}

using TMPro;
using UnityEngine;

public class SuccessPopup : UI_Popup
{
    [SerializeField] private GameObject objectSuccess;
    public override void Init()
    {
        base.Init();

        OnAfterCloseAction += NextStage;
        Managers.Sound.Play("Cb_Success");

        if (Managers.Game.StageNumber == Managers.Game.MaxStageNumber)
        {
            objectSuccess.SetActive(true);
            buttonClose.gameObject.SetActive(false);
        }
    }

    private void NextStage()
    {
        Managers.Game.StageNumber++;
        Managers.Game.CreateStage();
    }
}
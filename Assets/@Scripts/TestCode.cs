using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Managers.Data.Init();
        Managers.Map.Init();
        Managers.Map.CreateMap(1);

        //Managers.UI.ShowPopupUI<SuccessPopup>("SuccessPopup");
        Managers.UI.ShowPopupUI<FailPopup>("FailPopup");
    }
    
}

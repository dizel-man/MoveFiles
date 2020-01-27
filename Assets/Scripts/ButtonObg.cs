using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonObg : MonoBehaviour
{
    public   ViewModels viewModels;
 

    private void OnEnable()
    {
        viewModels = GameObject.Find("Manager").GetComponent<ViewModels>();
    }

    public void OnClick()
    {
        viewModels.GetObgectFromButton(GetComponent<Button>());
    }
}

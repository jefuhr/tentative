using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMButton : MonoBehaviour
{
    public GameObject MsgPanel;

    public void showMsgPanel()
    {
        MsgPanel.SetActive(!MsgPanel.activeSelf);
    }
}

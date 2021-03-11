using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageClose : MonoBehaviour
{
    private GameObject[] messages;
    private bool pastcurrent = false;
    private RectTransform temprect;
    public void close()
    {
        messages = GameObject.FindGameObjectsWithTag("Message");
        pastcurrent = false;
        for (int x = 0; x < messages.Length; x++)
        {
            if (pastcurrent)
            {
                temprect = messages[x].GetComponent<RectTransform>();
                temprect.localPosition = new Vector3(temprect.localPosition.x, temprect.localPosition.y + 64, 0);
            }
            else
            {
                if (messages[x].Equals(this.gameObject))
                {
                    pastcurrent = true;
                }
            }
        }

        Destroy(this.gameObject);
    }
}

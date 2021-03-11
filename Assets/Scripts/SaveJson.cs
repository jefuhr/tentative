using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text.RegularExpressions;

public class SaveJson : MonoBehaviour
{
    public AreaManager am;
    public phptest pt;

    public class JsonObj
    {
        public string action = "update_user";
        public string contents = "";

        public JsonObj(PlayerData.Player p)
        {
            contents = JsonUtility.ToJson(p);
            Debug.Log(contents);
        }
    }

    public void Save()
    {
        string jsonstring;
        int first, second;
        JsonObj jso = new JsonObj(am.SendPlayer());
        jsonstring = Regex.Unescape(JsonUtility.ToJson(jso));
        first = jsonstring.IndexOf("\"{");
        jsonstring = jsonstring.Remove(first, 1);
        second = jsonstring.Length - 2;
        jsonstring = jsonstring.Remove(second, 1);
        Debug.Log(jsonstring);
        pt.SendMsg(jsonstring);
    }
}

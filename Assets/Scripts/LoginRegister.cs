using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class LoginRegister : MonoBehaviour
{
    public GameObject username_text;
    public GameObject password_text;
    public GameObject ipaddr_text;
    public phptest pt;
    public PlayerData player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerData").GetComponent<PlayerData>();
    }

    public class UserObj
    {
        public string username = "";
        public string password = "";

        public UserObj(string user, string pass)
        {
            username = user;
            password = pass;
        }
    }

    public class RegisterObj
    {
        public string action = "register_user";
        public string contents = "";

        public RegisterObj(UserObj u)
        {
            contents = JsonUtility.ToJson(u);
        }
    }

    public class LoginObj
    {
        public string action = "login_user";
        public string contents = "";

        public LoginObj(UserObj u)
        {
            contents = JsonUtility.ToJson(u);
        }
    }

    public void onLogin()
    {
        player.SetIp(ipaddr_text.GetComponent<InputField>().text);
        string username;
        string password;
        username = username_text.GetComponent<InputField>().text;
        password = password_text.GetComponent<InputField>().text;

        string jsonstring;
        int first, second;
        LoginObj jso = new LoginObj(new UserObj(username, password));
        jsonstring = Regex.Unescape(JsonUtility.ToJson(jso));
        first = jsonstring.IndexOf("\"{");
        jsonstring = jsonstring.Remove(first, 1);
        second = jsonstring.Length - 2;
        jsonstring = jsonstring.Remove(second, 1);
        pt.SendMsg(jsonstring);
    }

    public void onRegister()
    {
        player.SetIp(ipaddr_text.GetComponent<InputField>().text);
        string username;
        string password;
        username = username_text.GetComponent<InputField>().text;
        password = password_text.GetComponent<InputField>().text;

        string jsonstring;
        int first, second;
        RegisterObj jso = new RegisterObj(new UserObj(username, password));
        jsonstring = Regex.Unescape(JsonUtility.ToJson(jso));
        first = jsonstring.IndexOf("\"{");
        jsonstring = jsonstring.Remove(first, 1);
        second = jsonstring.Length - 2;
        jsonstring = jsonstring.Remove(second, 1);
        pt.SendMsg(jsonstring);
    }
}
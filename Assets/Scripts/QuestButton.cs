using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class QuestButton : MonoBehaviour
{
    public GameObject bg;
    public Text quest_text;
    public Text reward_text;
    public Text progress_text;
    public phptest pt;
    public int questID;
    public int questResID;
    public int questRewID;
    public QuestObj qo;
    public int progress;
    public int goal;
    public int reward;

    public class QuestObj
    {
        public string action = "get_mission";
        public string contents = "{\"missionID\": \"x\"}";

        public QuestObj(int id)
        {
            contents = "{\"missionID\": \"";
            contents += id;
            contents += "\"}";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FirstClick()
    {
        int day = System.DateTime.Now.Day;
        int month = System.DateTime.Now.Month;

        Random.InitState(day + month);
        questID = Random.Range(1, 3);
        Debug.Log(questID);

        qo = new QuestObj(questID);

        string jsonstring;
        int first, second;
        jsonstring = Regex.Unescape(JsonUtility.ToJson(qo));
        jsonstring = Regex.Unescape(jsonstring);
        first = jsonstring.IndexOf("\"{");
        jsonstring = jsonstring.Remove(first, 1);
        second = jsonstring.Length - 2;
        jsonstring = jsonstring.Remove(second, 1);

        Debug.Log(jsonstring);
        pt.SendMsg(jsonstring);
    }

    public void ShowBG()
    {
        bg.SetActive(!bg.activeSelf);
    }

    public void UpdateText(string j)
    {
        int first, second;
        first = j.IndexOf("requestType") + 14;
        second = j.IndexOf("\",", first);
        string req_resource = j.Substring(first, second - first);

        if(req_resource.Equals("food"))
        {
            questResID = 0;
        }
        else if (req_resource.Equals("wood"))
        {
            questResID = 1;
        }
        else if (req_resource.Equals("stone"))
        {
            questResID = 2;
        }
        else if (req_resource.Equals("leather"))
        {
            questResID = 3;
        }
        else if (req_resource.Equals("iron"))
        {
            questResID = 4;
        }
        else if (req_resource.Equals("gold"))
        {
            questResID = 5;
        }

        first = j.IndexOf("requestCount") + 15;
        second = j.IndexOf("\",", first);
        string req_amount = j.Substring(first, second - first);

        first = j.IndexOf("rewardType") + 13;
        second = j.IndexOf("\",", first);
        string rew_resource = j.Substring(first, second - first);

        if (rew_resource.Equals("food"))
        {
            questRewID = 0;
        }
        else if (rew_resource.Equals("wood"))
        {
            questRewID = 1;
        }
        else if (rew_resource.Equals("stone"))
        {
            questRewID = 2;
        }
        else if (rew_resource.Equals("leather"))
        {
            questRewID = 3;
        }
        else if (rew_resource.Equals("iron"))
        {
            questRewID = 4;
        }
        else if (rew_resource.Equals("gold"))
        {
            questRewID = 5;
        }

        first = j.IndexOf("rewardCount") + 14;
        second = j.Length - 3;
        string rew_amount = j.Substring(first, second - first);

        quest_text.text = "Gather " + req_amount + " " + req_resource;
        reward_text.text = "Reward: " + rew_amount + " " + rew_resource;
        progress_text.text = "0/" + req_amount;
        int.TryParse(req_amount, out goal);
        int.TryParse(rew_amount, out reward);
    }

    public void UpdateProgress()
    {
        progress_text.text = progress + "/" + goal;
    }
}

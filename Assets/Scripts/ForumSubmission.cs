using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class ForumSubmission : MonoBehaviour
{
    public InputField topic;
    public InputField message;
    public PlayerData playerdata;
    public GameObject cancel;
    public bool replying;
    public int userID;
    public int topicID;
    public phptest pt;

    public class ForumObj
    {
        public int uuid;
        public string topic;
        public string message;

        public ForumObj(int id, string t, string m)
        {
            uuid = id;
            topic = t;
            message = m;
        }
    }

    public class ForumReplyObj
    {
        public int uuid;
        public int topicID;
        public string message;

        public ForumReplyObj(int id, int t, string m)
        {
            uuid = id;
            topicID = t;
            message = m;
        }
    }

    public class PostObj
    {
        public string action = "add_new_forum";
        public string contents = "";

        public PostObj(ForumObj f)
        {
            contents = JsonUtility.ToJson(f);
            Debug.Log(contents);
        }
    }

    public class ReplyObj
    {
        public string action = "add_new_reply";
        public string contents = "";

        public ReplyObj(ForumReplyObj f)
        {
            contents = JsonUtility.ToJson(f);
            Debug.Log(contents);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerdata = GameObject.FindGameObjectWithTag("PlayerData").GetComponent<PlayerData>();
    }

    public void Submission()
    {
        string jsonstring;
        int first, second;
        if(!replying)
        {
            ForumObj post = new ForumObj(playerdata.player.uuid, topic.text, message.text);
            PostObj json = new PostObj(post);
            jsonstring = Regex.Unescape(JsonUtility.ToJson(json));
            first = jsonstring.IndexOf("\"{");
            jsonstring = jsonstring.Remove(first, 1);
            second = jsonstring.Length - 2;
            jsonstring = jsonstring.Remove(second, 1);
            Debug.Log(jsonstring);
            pt.SendMsg(jsonstring);
        }
        else
        {
            ForumReplyObj post = new ForumReplyObj(playerdata.player.uuid, topicID, message.text);
            ReplyObj json = new ReplyObj(post);
            jsonstring = Regex.Unescape(JsonUtility.ToJson(json));
            first = jsonstring.IndexOf("\"{");
            jsonstring = jsonstring.Remove(first, 1);
            second = jsonstring.Length - 2;
            jsonstring = jsonstring.Remove(second, 1);
            Debug.Log(jsonstring);
            pt.SendMsg(jsonstring);
            cancelReply();
        }
    }

    public void cancelReply()
    {
        userID = 0;
        topicID = 0;
        topic.interactable = true;
        replying = false;
        topic.text = "";
        message.text = "";
        cancel.SetActive(false);
    }
}

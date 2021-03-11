using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ForumPost : MonoBehaviour
{
    public string username;
    public GameObject submission;
    public GameObject reply;
    private GameObject temp;
    private GameObject[] posts;
    bool pastcurrent = false;
    private RectTransform temprect;
    public int reply_count;
    public bool reply_out = false;

    public string topic;
    public int topicID;
    public int userID;

    public phptest pt;

    public class Reply
    {
        public int topicID;
        public int uuid;
        public string topic;
        public string message;
        public string username;
    }

    public void getReplies()
    {
        if(!reply_out)
        {
            pt.SendMsg("{\"action\" : \"get_all_forum_replies\",\"contents\" : {\"topicID\" : " + topicID + "}}");
        }
        else
        {
            makeReply("");
        }
    }

    public void makeReply(string j)
    {
        if(!reply_out)
        {
            if (!j.Equals(""))
            {
                int count_index = j.IndexOf("\"count\":") + 8;
                int count_end = j.IndexOf("}}", count_index);
                int.TryParse(j.Substring(count_index, count_end - count_index), out reply_count);
                if (reply_count == 0)
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }

        if (!reply_out)
        {
            int post_start;
            int post_end;
            string temp_json;
            Reply current_reply;

            for (int x = 0; x < reply_count; x++)
            {
                post_start = j.IndexOf("\"" + x + "\":") + 3 + x.ToString().Length;
                if (x < reply_count - 1)
                {
                    int temp = x + 1;
                    post_end = j.IndexOf("\"" + temp + "\":") - 1;
                }
                else
                {
                    post_end = j.IndexOf("\"count\":") - 1;
                }

                temp_json = j.Substring(post_start, post_end - post_start);

                Debug.Log(temp_json);

                current_reply = JsonUtility.FromJson<Reply>(temp_json);

                temp = Instantiate(reply, transform);
                temp.GetComponent<RectTransform>().localPosition = new Vector3(transform.position.x, transform.position.y - (64 * (x+1)), transform.position.z);
                temp.name = current_reply.username + "_reply";
                temp.transform.Find("Username").GetComponent<Text>().text = current_reply.username;
                temp.transform.Find("Message").GetComponent<Text>().text = current_reply.message;
            }

            posts = GameObject.FindGameObjectsWithTag("Post");
            pastcurrent = false;
            for (int x = 0; x < posts.Length; x++)
            {
                if (pastcurrent)
                {
                    temprect = posts[x].GetComponent<RectTransform>();
                    temprect.localPosition = new Vector3(temprect.localPosition.x, temprect.localPosition.y - (64 * reply_count), temprect.localPosition.z);
                }
                else
                {
                    if (posts[x].Equals(this.gameObject))
                    {
                        pastcurrent = true;
                    }
                }
            }

            reply_out = true;
        }
        else
        {
            foreach (Transform child in transform)
            {
                if(child.gameObject.CompareTag("Reply"))
                {
                    Destroy(child.gameObject);
                }
            }

            posts = GameObject.FindGameObjectsWithTag("Post");
            pastcurrent = false;
            for (int x = 0; x < posts.Length; x++)
            {
                if (pastcurrent)
                {
                    temprect = posts[x].GetComponent<RectTransform>();
                    temprect.localPosition = new Vector3(temprect.localPosition.x, temprect.localPosition.y + (64 * reply_count), temprect.localPosition.z);
                }
                else
                {
                    if (posts[x].Equals(this.gameObject))
                    {
                        pastcurrent = true;
                    }
                }
            }

            reply_out = false;
        }
    }

    public void startReply()
    {
        ForumSubmission fs = submission.GetComponent<ForumSubmission>();
        fs.topic.text = "replying to: " + username + " \"" + topic + "\"";
        fs.topic.interactable = false;
        fs.replying = true;
        fs.topicID = topicID;
        fs.cancel.SetActive(true);
    }
}

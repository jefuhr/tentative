using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ForumPanel : MonoBehaviour
{
    public GameObject submission;
    public GameObject post;
    private GameObject[] posts;
    private GameObject temp;
    public int forum_count;
    private RectTransform rect;
    private float offset;
    public phptest pt;

    public class Post
    {
        public int topicID;
        public int uuid;
        public string topic;
        public string message;
        public string username;
    }


    // Start is called before the first frame update
    void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
        offset = -1 * ((rect.offsetMin.y - rect.offsetMax.y) * 0.5f) - 88;
        getPosts();
    }

    public void getPosts()
    {
        pt.SendMsg("{\"action\" : \"get_all_forum_topics\"}");
    }

    public void setPosts(string j)
    {
        int count_index = j.IndexOf("\"count\":") + 8;
        int count_end = j.IndexOf("}}", count_index);
        int.TryParse(j.Substring(count_index, count_end - count_index), out forum_count);
        if(forum_count == 0)
        {
            return;
        }

        posts = new GameObject[forum_count];

        int post_start;
        int post_end;
        string temp_json;
        Post current_post;
        
        for (int x = 0; x < forum_count; x++)
        {
            post_start = j.IndexOf("\"" + x + "\":") + 3 + x.ToString().Length;
            if(x < forum_count - 1)
            {
                int temp = x + 1;
                post_end = j.IndexOf("\"" + temp + "\":") - 1;
            }
            else
            {
                post_end = j.IndexOf("\"count\":") - 1;
            }

            temp_json = j.Substring(post_start, post_end - post_start);

            current_post = JsonUtility.FromJson<Post>(temp_json);

            temp = Instantiate(post, transform);
            temp.GetComponent<RectTransform>().localPosition = new Vector3(-10, offset - (64 * x), transform.position.z);

            temp.transform.Find("Username").GetComponent<Text>().text = current_post.username;
            temp.transform.Find("Topic").GetComponent<Text>().text = current_post.topic;
            temp.transform.Find("Message").GetComponent<Text>().text = current_post.message;
            temp.GetComponent<ForumPost>().userID = current_post.uuid;
            temp.GetComponent<ForumPost>().topicID = current_post.topicID;
            temp.GetComponent<ForumPost>().topic = current_post.topic;
            temp.GetComponent<ForumPost>().username = current_post.username;

            temp.GetComponent<ForumPost>().submission = submission;
            temp.GetComponent<ForumPost>().pt = pt;

            posts[x] = temp;

            Canvas canvas = gameObject.GetComponentInParent<Canvas>();
            GraphicRegistry.UnregisterGraphicForCanvas(canvas, GetComponent<Graphic>());
        }
    }

    public void setReplies(string j)
    {
        int topic_index = j.IndexOf("\"topicID\":") + 11;
        int topic_end = j.IndexOf("\"", topic_index);
        int index; 
        int.TryParse(j.Substring(topic_index, topic_end - topic_index), out index);

        posts[index].GetComponent<ForumPost>().makeReply(j);
    }

    public void closeReplies()
    {
        for(int x = 0; x < posts.Length; x++)
        {
            posts[x].GetComponent<ForumPost>().makeReply("");
        }
    }
}

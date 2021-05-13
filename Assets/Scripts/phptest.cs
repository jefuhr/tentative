using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class phptest : MonoBehaviour
{
    public PlayerData player;
    public ForumPanel chat;
    public TradeButtons tb;
    public TradeButtons ptb;
    public QuestButton qb;
    public string ipaddr;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerData").GetComponent<PlayerData>();
        ipaddr = player.ipaddr;
    }

    public void SendMsg(string msg)
    {
        StartCoroutine(Upload(msg));
    }

    IEnumerator Upload(string msg)
    {
        int json_start;
        string response, json;
        WWWForm form = new WWWForm();
        form.AddField("message", msg);

        if(!player)
        {
            player = GameObject.FindGameObjectWithTag("PlayerData").GetComponent<PlayerData>();
        }

        UnityWebRequest www = UnityWebRequest.Post("http://" + player.ipaddr + "/info.php", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
        }
        else
        {
            response = www.downloadHandler.text;
            Debug.Log(response);

            json_start = response.IndexOf("[message] => ") + 13;
            json = response.Substring(json_start, response.IndexOf("\n", json_start) - json_start);
            Debug.Log(json);

            if(json.Contains("\"status\":\"200\""))
            {
                if (response.Contains("login_user"))
                {
                    onLogin(json);
                }
                else if(response.Contains("update_user"))
                {
                    onUpdate();
                }
                else if(response.Contains("get_all_currency_data"))
                {
                    onCurrencyGet(json);
                }
                else if(response.Contains("get_all_forum_topics"))
                {
                    onForumTopic(json);
                }
                else if(response.Contains("add_new_forum"))
                {
                    onNewForumTopic(json);
                }
                else if(response.Contains("get_all_forum_replies"))
                {
                    onForumReplies(json);
                }
                else if(response.Contains("add_new_reply"))
                {
                    onNewForumReplies();
                }
                else if(response.Contains("get_all_player_trades"))
                {
                    onPlayerTradesGet(json);
                }
                else if(response.Contains("accept_trade") || response.Contains("delete_trade") || response.Contains("add_new_trade"))
                {
                    onTradeReset();
                }
                else if(response.Contains("get_mission"))
                {
                    onQuestGet(json);
                }
            }
        }
    }

    public void onLogin(string j)
    {
        player.SetPlayer(j);
        SceneManager.LoadScene("Main");
    }

    public void onUpdate()
    {
        player.ZeroPlayer();
        SceneManager.LoadScene("Title");
    }

    public void onCurrencyGet(string j)
    {
        tb.setData(j);
    }

    public void onPlayerTradesGet(string j)
    {
        ptb.setPlayerTrades(j);
    }

    public void onForumTopic(string j)
    {
        chat.setPosts(j);
    }

    public void onNewForumTopic(string j)
    {
        chat.getPosts();
    }

    public void onForumReplies(string j)
    {
        chat.setReplies(j);   
    }

    public void onNewForumReplies()
    {
        chat.closeReplies();
    }

    public void onTradeReset()
    {
        ptb.findPlayerTrades();
    }

    public void onQuestGet(string j)
    {
        qb.UpdateText(j);
    }
}

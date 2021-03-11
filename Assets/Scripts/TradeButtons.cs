using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using System;

public class TradeButtons : MonoBehaviour
{
    public AreaManager am;
    public bool forcurrency = true;
    public int currency = 0;
    public Sprite[] imgs = new Sprite[9];
    public string[] types = new string[9] {"food", "wood", "stone", "leather", "iron", "gold", "currency0", "currency1", "currency2"};
    public GameObject[] trades;
    public phptest pt;
    private string json;
    private string currency0_json;
    private string currency1_json;
    private string currency2_json;

    public int current_give = 3;
    public int current_take = 3;

    public int current_give_amt = 0;
    public int current_take_amt = 0;

    public TMessages tmsg;
    public bool playertrades;

    public class Currency
    {
        public int currencyType;
        public int currentValue;
        public int food;
        public int wood;
        public int stone;
        public int leather;
        public int iron;
        public int gold;

        public int[] getVals()
        {
            int[] i = new int[6];
            i[0] = food;
            i[1] = wood;
            i[2] = stone;
            i[3] = leather;
            i[4] = iron;
            i[5] = gold;
            return i;
        }
    }

    Currency c0, c1, c2;

    public class TradeObj
    {
        public int uuid;
        public string itemType;
        public int itemQuant;
        public string requestType;
        public int requestQuant;

        public TradeObj(int u, string it, int iq, string rt, int rq)
        {
            uuid = u;
            itemType = rt;
            itemQuant = rq;
            requestType = it;
            requestQuant = iq;
        }
    }

    public class PlayerTradeObj
    {
        public int uuid;
        public int tradeID;
        public string itemType;
        public int itemQuant;
        public string requestType;
        public int requestQuant;
        public string buyerID;
        public string username;
    }

    public class JsonObj
    {
        public string action = "add_new_trade";
        public string contents = "";

        public JsonObj(TradeObj t)
        {
            contents = JsonUtility.ToJson(t);
            Debug.Log(contents);
        }
    }

    public class IdObj
    {
        public int tradeID;

        public IdObj(int t)
        {
            tradeID = t;
        }
    }

    public class UidObj
    {
        public int tradeID;
        public int uuid;

        public UidObj(int t, int u)
        {
            tradeID = t;
            uuid = u;
        }
    }

    public class ClaimObj
    {
        public string action = "delete_trade";
        public string contents = "";

        public ClaimObj(IdObj i)
        {
            contents = JsonUtility.ToJson(i);
            Debug.Log(contents);
        }
    }

    public class AcceptObj
    {
        public string action = "accept_trade";
        public string contents = "";

        public AcceptObj(UidObj u)
        {
            contents = JsonUtility.ToJson(u);
            Debug.Log(contents);
        }
    }

    public void setImgs()
    {
        Transform t;
        for (int x = 0; x < trades.Length; x++)
        {
            if(forcurrency)
            {
                t = trades[x].transform.Find("TakeItem");
                t.gameObject.GetComponent<Image>().sprite = imgs[currency + 6];
                t = trades[x].transform.Find("GiveItem");
                t.gameObject.GetComponent<Image>().sprite = imgs[x];
            }
            else
            {
                t = trades[x].transform.Find("GiveItem");
                t.gameObject.GetComponent<Image>().sprite = imgs[currency + 6];
                t = trades[x].transform.Find("TakeItem");
                t.gameObject.GetComponent<Image>().sprite = imgs[x];
            }
        }
    }

    public void setText()
    {
        Transform t;
        int[] currencyvals = new int[6];
        if (currency == 0)
        {
            currencyvals = c0.getVals();
        }
        else if (currency == 1)
        {
            currencyvals = c1.getVals();
        }
        else if (currency == 2)
        {
            currencyvals = c2.getVals();
        }

        for (int x = 0; x < trades.Length; x++)
        {
            if (forcurrency)
            {
                t = trades[x].transform.Find("Take x");
                t.gameObject.GetComponent<Text>().text = "1";
                t = trades[x].transform.Find("Give x");
                t.gameObject.GetComponent<Text>().text = currencyvals[x].ToString();
            }
            else
            {
                t = trades[x].transform.Find("Give x");
                t.gameObject.GetComponent<Text>().text = "1";
                t = trades[x].transform.Find("Take x");
                t.gameObject.GetComponent<Text>().text = currencyvals[x].ToString();
            }
        }
    }

    public void onCurrency0()
    {
        currency = 0;
        setImgs();
        setText();
    }

    public void onCurrency1()
    {
        currency = 1;
        setImgs();
        setText();
    }

    public void onCurrency2()
    {
        currency = 2;
        setImgs();
        setText();
    }

    public void onReverse()
    {
        forcurrency = !forcurrency;
        setImgs();
        setText();
    }

    public void commitTrade()
    {
        int i;
        int.TryParse(EventSystem.current.currentSelectedGameObject.name, out i);
        int[] currencyvals = new int[6];
        if (currency == 0)
        {
            currencyvals = c0.getVals();
        }
        else if (currency == 1)
        {
            currencyvals = c1.getVals();
        }
        else if (currency == 2)
        {
            currencyvals = c2.getVals();
        }

        if(forcurrency)
        {
            if(am.inventory[i] >= currencyvals[i])
            {
                am.changeInv(i, -1 * currencyvals[i]);
                am.changeInv(currency + 6, 1);
            }
        }
        else
        {
            if (am.inventory[currency + 6] >= 1)
            {
                am.changeInv(i, currencyvals[i]);
                am.changeInv(currency + 6, -1);
            }
        }
    }

    public void commitPlayerTrade()
    {
        string jsonstring;
        int first, second;

        GameObject temp = EventSystem.current.currentSelectedGameObject;
        TradeVals tv = temp.GetComponent<TradeVals>();

        int q = tv.request_quant;
        int t = Array.IndexOf(types, tv.request_type);
        int q2 = tv.item_quant;
        int t2 = Array.IndexOf(types, tv.item_type);

        if (am.inventory[t] < q)
        {
            Debug.Log("not enough for trade!");
            return;
        }
        else
        {
            am.changeInv(t, -q);
            am.changeInv(t2, q2);
        }

        UidObj uidobj = new UidObj(tv.tid, am.playerdata.player.uuid);
        AcceptObj jso = new AcceptObj(uidobj);

        jsonstring = Regex.Unescape(JsonUtility.ToJson(jso));
        first = jsonstring.IndexOf("\"{");
        jsonstring = jsonstring.Remove(first, 1);
        second = jsonstring.Length - 2;
        jsonstring = jsonstring.Remove(second, 1);
        Debug.Log(jsonstring);
        pt.SendMsg(jsonstring);
    }

    public void claimPlayerTrade()
    {
        string jsonstring;
        int first, second;

        GameObject temp = EventSystem.current.currentSelectedGameObject;
        TradeVals tv = temp.GetComponent<TradeVals>();

        int q = tv.item_quant;
        int t = Array.IndexOf(types, tv.item_type);

        am.changeInv(t, q);

        IdObj idobj = new IdObj(tv.tid);
        ClaimObj jso = new ClaimObj(idobj);

        jsonstring = Regex.Unescape(JsonUtility.ToJson(jso));
        first = jsonstring.IndexOf("\"{");
        jsonstring = jsonstring.Remove(first, 1);
        second = jsonstring.Length - 2;
        jsonstring = jsonstring.Remove(second, 1);
        Debug.Log(jsonstring);
        pt.SendMsg(jsonstring);
    }

    public void findTrades()
    {
        trades = GameObject.FindGameObjectsWithTag("Trade");
        setImgs();
        pt.SendMsg("{\"action\" : \"get_all_currency_data\"}");
    }

    public void findPlayerTrades()
    {
        tmsg.playertradecount = 0;
        pt.SendMsg("{\"action\" : \"get_all_player_trades\"}");
    }

    public void setPlayerTrades(string j)
    {
        GameObject[] loadedtrades = GameObject.FindGameObjectsWithTag("PlayerTrade");

        for(int x = 0; x < loadedtrades.Length; x++)
        {
            Destroy(loadedtrades[x]);
        }

        int count_index = j.IndexOf("\"count\":") + 8;
        int count_end = j.IndexOf("}}", count_index);
        int trade_count;
        int.TryParse(j.Substring(count_index, count_end - count_index), out trade_count);

        int post_start;
        int post_end;
        string temp_json;
        PlayerTradeObj current_trade;

        for (int x = 0; x < trade_count; x++)
        {
            post_start = j.IndexOf("\"" + x + "\":") + 3 + x.ToString().Length;
            if (x < trade_count - 1)
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

            current_trade = JsonUtility.FromJson<PlayerTradeObj>(temp_json);

            tmsg.makePlayerTrades(current_trade);
        }
    }

    public void setData(string j)
    {
        int c0i;
        int c1i;
        int c2i;
        json = j;
        c0i = json.IndexOf("\"currencyType\":\"0\"") - 1;
        c1i = json.IndexOf("\"currencyType\":\"1\"") - 1;
        c2i = json.IndexOf("\"currencyType\":\"2\"") - 1;
        currency0_json = json.Substring(c0i, c1i - c0i - 1);
        currency1_json = json.Substring(c1i, c2i - c1i - 1);
        currency2_json = json.Substring(c2i, json.IndexOf("]", c2i) - c2i);

        c0 = JsonUtility.FromJson<Currency>(currency0_json);
        c1 = JsonUtility.FromJson<Currency>(currency1_json);
        c2 = JsonUtility.FromJson<Currency>(currency2_json);
        setText();
    }

    public void ChangeGiveImg()
    {
        int i;
        int.TryParse(EventSystem.current.currentSelectedGameObject.name, out i);

        i++;
        if(i >= 9)
        {
            i = 0;
        }
        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = imgs[i];
        EventSystem.current.currentSelectedGameObject.name = i.ToString();

        current_give = i;
    }

    public void ChangeTakeImg()
    {
        int i;
        int.TryParse(EventSystem.current.currentSelectedGameObject.name, out i);

        i++;
        if (i >= 9)
        {
            i = 0;
        }
        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = imgs[i];
        EventSystem.current.currentSelectedGameObject.name = i.ToString();

        current_take = i;
    }

    public void ChangeGiveAmt()
    {
        int.TryParse(EventSystem.current.currentSelectedGameObject.GetComponent<InputField>().text, out current_give_amt);
    }

    public void ChangeTakeAmt()
    {
        int.TryParse(EventSystem.current.currentSelectedGameObject.GetComponent<InputField>().text, out current_take_amt);
    }

    public void NewTrade()
    {
        string jsonstring;
        int first, second;

        if(am.inventory[current_give] < current_give_amt)
        {
            Debug.Log("not enough for trade!");
            return;
        }
        else
        {
            am.changeInv(current_give, -current_give_amt);
        }

        TradeObj tempt = new TradeObj(am.playerdata.player.uuid, types[current_give], current_give_amt, types[current_take], current_take_amt);
        JsonObj jso = new JsonObj(tempt);

        jsonstring = Regex.Unescape(JsonUtility.ToJson(jso));
        first = jsonstring.IndexOf("\"{");
        jsonstring = jsonstring.Remove(first, 1);
        second = jsonstring.Length - 2;
        jsonstring = jsonstring.Remove(second, 1);
        Debug.Log(jsonstring);
        pt.SendMsg(jsonstring);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TMessages : MonoBehaviour
{
    public GameObject message;
    public Transform trans;
    private GameObject temp;
    public int post_count;
    private RectTransform rect;
    private float offset;
    public TradeButtons tb;
    public int playertradecount = 0;
    // Start is called before the first frame update
    void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
        offset = -1 * ((rect.offsetMin.y - rect.offsetMax.y) * 0.5f) - 88;
        if(!tb.playertrades)
        {
            for (int x = 0; x < post_count; x++)
            {
                temp = Instantiate(message, trans);
                temp.GetComponent<RectTransform>().localPosition = new Vector3(0, offset - (75 * x), transform.position.z);
                temp.transform.Find("Confirm").GetComponent<Button>().onClick.AddListener(tb.commitTrade);
                temp.transform.Find("Confirm").name = x.ToString();
            }

            tb.findTrades();
        }
        else
        {
            tb.findPlayerTrades();
        }
    }

    public void makePlayerTrades(TradeButtons.PlayerTradeObj tobj)
    {
        temp = Instantiate(message, trans);
        temp.GetComponent<RectTransform>().localPosition = new Vector3(0, offset - (75 * playertradecount), transform.position.z);

        temp.transform.Find("GiveItem").GetComponent<Image>().sprite = tb.imgs[Array.IndexOf(tb.types, tobj.requestType)];
        temp.transform.Find("TakeItem").GetComponent<Image>().sprite = tb.imgs[Array.IndexOf(tb.types, tobj.itemType)];

        temp.transform.Find("Give x").GetComponent<Text>().text = tobj.requestQuant.ToString();
        temp.transform.Find("Take x").GetComponent<Text>().text = tobj.itemQuant.ToString();

        temp.tag = "PlayerTrade";

        int current_uuid = tb.am.playerdata.player.uuid;
        int tobj_uuid = tobj.uuid;
        int buyer;
        int.TryParse(tobj.buyerID, out buyer);
        GameObject temp_confirm = temp.transform.Find("Confirm").gameObject;
        TradeVals temp_vals = temp_confirm.GetComponent<TradeVals>();

        if(!string.IsNullOrEmpty(tobj.buyerID))
        {
            temp_confirm.SetActive(false);

            if (current_uuid == tobj_uuid)
            {
                temp_confirm = temp.transform.Find("Claim").gameObject;
                temp_vals = temp_confirm.GetComponent<TradeVals>();
                temp_vals.item_type = tobj.itemType;
                temp_vals.item_quant = tobj.itemQuant;
                temp_vals.request_type = tobj.requestType;
                temp_vals.request_quant = tobj.requestQuant;
                temp_vals.tid = tobj.tradeID;
                temp_vals.uid = tobj.uuid;
                temp_confirm.SetActive(true);
                temp_confirm.GetComponent<Button>().onClick.AddListener(tb.claimPlayerTrade);
            }
        }
        else
        {
            if(current_uuid != tobj_uuid)
            {
                temp_vals.item_type = tobj.itemType;
                temp_vals.item_quant = tobj.itemQuant;
                temp_vals.request_type = tobj.requestType;
                temp_vals.request_quant = tobj.requestQuant;
                temp_vals.tid = tobj.tradeID;
                temp_vals.uid = tobj.uuid;
                temp_confirm.GetComponent<Button>().onClick.AddListener(tb.commitPlayerTrade);
            }
            else
            {
                temp_confirm.SetActive(false);
            }
        }

        playertradecount++;
    }
}

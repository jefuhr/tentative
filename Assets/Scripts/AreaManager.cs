using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AreaManager : MonoBehaviour
{
    public PlayerData playerdata;

    public Text username;

    public int[] inventory = new int[9];
    public Text[] invtext = new Text[9];
    public Sprite[] images = new Sprite[6];
    public GameObject newres;
    private GameObject g;
    private GameObject[] workers;
    // Start is called before the first frame update
    void Start()
    {
        playerdata = GameObject.FindGameObjectWithTag("PlayerData").GetComponent<PlayerData>();
        inventory = playerdata.player.getinv();
        for(int x = 0; x < 9; x++)
        {
            invtext[x].text = inventory[x].ToString();
        }
        username.text = playerdata.player.username;
        TellWorkers();
    }

    public void TellWorkers()
    {
        workers = GameObject.FindGameObjectsWithTag("Worker");
        for(int x = 0; x < workers.Length; x++)
        {
            workers[x].GetComponent<Worker>().UpdateTiles();
        }
    }

    public void GiveResource(int type, Vector2 pos)
    {
        inventory[type]++;
        invtext[type].text = inventory[type].ToString();
        g = Instantiate(newres, pos, new Quaternion(), GameObject.Find("ResourceEffect").transform);
        g.gameObject.GetComponent<Image>().sprite = images[type];
    }

    public void changeInv(int index, int amount)
    {
        inventory[index] += amount;
        invtext[index].text = inventory[index].ToString();
    }

    public PlayerData.Player SendPlayer()
    {
        playerdata.SetPlayerValues(inventory);
        return playerdata.player;
    }

    public void ZeroInv()
    {
        Array.Clear(inventory, 0, inventory.Length);
    }
}
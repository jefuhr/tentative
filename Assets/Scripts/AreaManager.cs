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
    public Sprite[] tile_sprites = new Sprite[6];
    public GameObject newres;
    private GameObject g;
    private GameObject[] workers;
    private GameObject[] tiles;
    private TileScript[] tilescripts = new TileScript[25];
    private int[,] tiledata = new int[25, 3];

    public QuestButton qb;
    // Start is called before the first frame update
    void Start()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        for (int x = 0; x < tiles.Length; x++)
        {
            tilescripts[x] = tiles[x].GetComponent<TileScript>();
        }
        GetTiles();

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
        if(type == qb.questResID)
        {
            qb.progress++;
            if(qb.progress >= qb.goal)
            {
                qb.progress = 0;
                inventory[qb.questRewID] += qb.reward;
            }

            qb.UpdateProgress();
        }

        inventory[type]++;
        invtext[type].text = inventory[type].ToString();
        g = Instantiate(newres, pos, new Quaternion(), GameObject.Find("ResourceEffect").transform);
        g.transform.position = new Vector2(g.transform.position.x + (UnityEngine.Random.value * 1) - 0.5f, g.transform.position.y + (UnityEngine.Random.value * 1) - 0.5f);
        g.gameObject.GetComponent<Image>().sprite = images[type];
    }

    public void changeInv(int index, int amount)
    {
        inventory[index] += amount;
        invtext[index].text = inventory[index].ToString();
    }

    public PlayerData.Player SendPlayer()
    {
        GetTiles();
        playerdata.SetPlayerArea(tiledata);
        playerdata.SetPlayerValues(inventory);
        return playerdata.player;
    }

    public void ZeroInv()
    {
        Array.Clear(inventory, 0, inventory.Length);
    }

    public void GetTiles()
    {
        for(int x = 0; x < 25; x++)
        {
            tiledata[x, 0] = tilescripts[x].index;
            tiledata[x, 1] = tilescripts[x].tile_type;
            tiledata[x, 2] = tilescripts[x].tile_level;
        }
    }
}
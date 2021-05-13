using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSetup : MonoBehaviour
{
    public int workercount;
    public GameObject tile;
    public GameObject worker;
    private GameObject temp;
    private Vector3 tilepos = new Vector3(-2, 2, 0);
    private Quaternion tilerot = new Quaternion();
    private SpriteRenderer sr;
    public Sprite[] tile_sprites = new Sprite[6];
    public GameObject build_button;
    public AreaManager am;
    public PlayerData playerdata;
    // Start is called before the first frame update
    void Start()
    {
        playerdata = GameObject.FindGameObjectWithTag("PlayerData").GetComponent<PlayerData>();

        for (int x = 0; x < 25; x++)
        {
            temp = Instantiate(tile, tilepos, tilerot, this.transform);
            temp.gameObject.GetComponent<TileScript>().index = x;
            temp.gameObject.GetComponent<TileScript>().tile_type = GetPlayerTileType(x);
            sr = temp.gameObject.GetComponent<SpriteRenderer>();
            sr.sprite = tile_sprites[temp.gameObject.GetComponent<TileScript>().tile_type];
            if(GetPlayerTileType(x) == 0)
            {
                sr.color = new Color(0.29f, 0.41f, 0.18f);
            }
            temp.gameObject.GetComponent<TileScript>().bb = build_button.GetComponent<BuildButton>();
            tilepos.x++;
            if(tilepos.x > 2)
            {
                tilepos.x = -2;
                tilepos.y--;
            }
            temp.tag = "Tile";
        }

        for(int x = 0; x < workercount; x++)
        {
            temp = Instantiate(worker, new Vector3(0, 0, 1), new Quaternion());
            temp.GetComponent<Worker>().workerid = x + 1;
        }

        am.tile_sprites = tile_sprites;
    }

    public int GetPlayerTileType(int index)
    {
        return playerdata.player.gettiletype(index);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

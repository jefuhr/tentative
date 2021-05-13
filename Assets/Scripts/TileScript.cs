using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileScript : MonoBehaviour
{
    public int tile_type;
    private int[] next_harvest = new int[] {-1, 10, 25, 15, 20, 30};
    public enum Tile_Types
    {
        clear,
        wild,
        farm,
        lumber,
        quarry,
        mine
    }
    public float harvest = 0;
    public int index;
    private SpriteRenderer sr;
    private SpriteRenderer tile_sr;
    private GameObject blip;
    private GameObject blip_outline;
    private BoxCollider2D col;
    private Color tilecolor;
    private Vector3 mouse;

    public AreaManager am;
    public bool harvestable = false;
    private bool told = false;
    public int hasworker = 0;

    public BuildButton bb;
    public int tile_level = 1;

    // Start is called before the first frame update
    void Start()
    {
        blip = transform.Find("blip").gameObject;
        blip_outline = transform.Find("blip_outline").gameObject;
        sr = blip.GetComponent<SpriteRenderer>();
        col = gameObject.GetComponent<BoxCollider2D>();
        tile_sr = gameObject.GetComponent<SpriteRenderer>();

        am = GameObject.Find("Area").GetComponent<AreaManager>();
        StartColor();
    }

    // Update is called once per frame
    void Update()
    {
        if(bb.building)
        {
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (col.bounds.Contains(new Vector3(mouse.x, mouse.y, this.gameObject.transform.position.z)))
            {
                //if trying to clear a cleared tile, skip. If trying to build on a non cleared tile, skip.
                if ((tile_type == (int)Tile_Types.clear && bb.build_type != (int)Tile_Types.clear) || (tile_type != (int)Tile_Types.clear && bb.build_type == (int)Tile_Types.clear))
                {
                    tile_sr.color = new Color(0.5f, 0.1f, 0.1f);
                    if(Input.GetMouseButtonDown(0))
                    {
                        if (am.inventory[bb.costs[bb.build_type, 0, 0]] >= bb.costs[bb.build_type, 0, 1])
                        {
                            if (am.inventory[bb.costs[bb.build_type, 1, 0]] >= bb.costs[bb.build_type, 1, 1])
                            {
                                if (am.inventory[bb.costs[bb.build_type, 2, 0]] >= bb.costs[bb.build_type, 2, 1])
                                {
                                    tile_type = bb.build_type;
                                    tile_sr.sprite = am.tile_sprites[tile_type];
                                    if (tile_type == (int)Tile_Types.clear)
                                    {
                                        tile_sr.color = new Color(0.29f, 0.41f, 0.18f);
                                    }
                                    else
                                    {
                                        tile_sr.color = new Color(1, 1, 1);
                                    }
                                    harvest = 0;
                                    harvestable = false;

                                    am.changeInv(bb.costs[bb.build_type, 0, 0], -bb.costs[bb.build_type, 0, 1]);
                                    am.changeInv(bb.costs[bb.build_type, 1, 0], -bb.costs[bb.build_type, 1, 1]);
                                    am.changeInv(bb.costs[bb.build_type, 2, 0], -bb.costs[bb.build_type, 2, 1]);
                                }
                            }
                        }   
                    }
                }
                else
                {
                    if (tile_type == (int)Tile_Types.clear)
                        tile_sr.color = new Color(0.29f, 0.41f, 0.18f);
                    else
                        tile_sr.color = new Color(1, 1, 1);
                }
            }
            else
            {
                if (tile_type == (int)Tile_Types.clear)
                    tile_sr.color = new Color(0.29f, 0.41f, 0.18f);
                else
                    tile_sr.color = new Color(1, 1, 1);
            }
        }

        if(tile_type != (int)Tile_Types.clear)
        {
            if (!blip.activeSelf)
            {
                blip.SetActive(true);
                blip_outline.SetActive(true);
            }
        }

        if (tile_type != (int)Tile_Types.clear && harvest >= next_harvest[tile_type])
        {
            tilecolor.r = 0;
            tilecolor.g = 0.5f;
            tilecolor.r = 0f;
            sr.color = tilecolor;
            harvestable = true;

            if(!told)
            {
                am.TellWorkers();
                told = true;
            }

            if(Input.GetMouseButtonDown(0))
            {
                mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (col.bounds.Contains(new Vector3(mouse.x, mouse.y, this.gameObject.transform.position.z)))
                {
                    Collect();
                }
            }
        }
        else if(tile_type != (int)Tile_Types.clear)
        {
            harvest += Time.deltaTime;
            tilecolor.r = 1 - harvest * (1.0f / next_harvest[tile_type]);
            tilecolor.g = harvest * (1.0f / next_harvest[tile_type]);
            tilecolor.b = 0;
            sr.color = tilecolor;
        }
        else if(blip.activeSelf)
        {
            blip.SetActive(false);
            blip_outline.SetActive(false);
        }
    }

    public void Collect()
    {
        if(harvestable)
        {
            if(tile_type == (int)Tile_Types.wild)
            {
                told = false;
                harvestable = false;
                hasworker = 0;
                am.GiveResource((int)(UnityEngine.Random.value * 6), transform.position);
                StartColor();
            }
            else if(tile_type == (int)Tile_Types.farm)
            {
                for(int x = 0; x < 3; x++)
                {
                    told = false;
                    harvestable = false;
                    hasworker = 0;
                    am.GiveResource(0, transform.position);
                    am.GiveResource(3, transform.position);
                    StartColor();
                }
            }
            else if(tile_type == (int)Tile_Types.lumber)
            {
                for(int x = 0; x < 5; x++)
                {
                    told = false;
                    harvestable = false;
                    hasworker = 0;
                    am.GiveResource(1, transform.position);
                    StartColor();
                }
            }
            else if (tile_type == (int)Tile_Types.quarry)
            {
                for (int x = 0; x < 5; x++)
                {
                    told = false;
                    harvestable = false;
                    hasworker = 0;
                    am.GiveResource(2, transform.position);
                    StartColor();
                }
            }
            else if (tile_type == (int)Tile_Types.mine)
            {
                told = false;
                harvestable = false;
                hasworker = 0;
                am.GiveResource(4, transform.position);
                am.GiveResource(4, transform.position);
                am.GiveResource(4, transform.position);
                am.GiveResource(5, transform.position);
                StartColor();
            }
        }
    }

    void StartColor()
    {
        harvest = UnityEngine.Random.value * 2;
        tilecolor = new Color(1, 0, 0);
        tilecolor.r -= harvest * 0.1f;
        tilecolor.g += harvest * 0.1f;
        tilecolor.b = 0;
        sr.color = tilecolor;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    public int workerid;
    public GameObject target;
    private bool hastarget = false;
    private Animator anim;
    private SpriteRenderer sr;
    public float speed;
    private GameObject[] tiles = new GameObject[25];
    private bool[] tilestate = new bool[25];
    private float currentdist = float.MaxValue;
    private Vector2 direction;
    private bool animset = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hastarget && currentdist > 0.1)
        {
            transform.Translate(direction * Time.deltaTime * speed);
            currentdist = Vector2.Distance(this.gameObject.transform.position, target.transform.position);
        }
        else if (currentdist <= 0.1)
        {
            target.GetComponent<TileScript>().Collect();
            UpdateTiles();
        }

        if(hastarget && !animset)
        {
            animset = true;
            if (direction.x < 0)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }
            anim.SetInteger("State", 1);
        }
        else if(!hastarget && !animset)
        {
            animset = true;
            anim.SetInteger("State", 2);
        }
    }

    public void UpdateTiles()
    {
        float dist;
        currentdist = float.MaxValue;
        hastarget = false;
        animset = false;
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        for (int x = 0; x < 25; x++)
        {
            tilestate[x] = tiles[x].GetComponent<TileScript>().harvestable;

            if (tilestate[x] == true && (tiles[x].GetComponent<TileScript>().hasworker == workerid || tiles[x].GetComponent<TileScript>().hasworker == 0))
            {
                hastarget = true;
                dist = Vector2.Distance(this.gameObject.transform.position, tiles[x].transform.position);
                if (dist < currentdist)
                {
                    tiles[x].GetComponent<TileScript>().hasworker = workerid;
                    if(target)
                    {
                        target.GetComponent<TileScript>().hasworker = 0;
                    }
                    target = tiles[x];
                    direction = target.transform.position - this.gameObject.transform.position;
                    direction.Normalize();
                    currentdist = dist;
                }
            }
        }
    }
}

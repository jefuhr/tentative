using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public float harvest = 0;
    public int index;
    private SpriteRenderer sr;
    private BoxCollider2D col;
    private Color tilecolor;
    private Vector3 mouse;

    public AreaManager am;
    public bool harvestable = false;
    private bool told = false;
    public int hasworker = 0;
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        col = gameObject.GetComponent<BoxCollider2D>();

        am = GameObject.Find("Area").GetComponent<AreaManager>();
        StartColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (harvest >= 10)
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
        else
        {
            harvest += Time.deltaTime;
            tilecolor.r = 1 - harvest * 0.1f;
            tilecolor.g = harvest * 0.1f;
            tilecolor.b = 0;
            sr.color = tilecolor;
        }
    }

    public void Collect()
    {
        if(harvestable)
        {
            told = false;
            harvestable = false;
            hasworker = 0;
            am.GiveResource((int)(Random.value * 6), transform.position);
            StartColor();
        }
    }

    void StartColor()
    {
        harvest = Random.value * 2;
        tilecolor = new Color(1, 0, 0);
        tilecolor.r -= harvest * 0.1f;
        tilecolor.g += harvest * 0.1f;
        tilecolor.b = 0;
        sr.color = tilecolor;
    }
}

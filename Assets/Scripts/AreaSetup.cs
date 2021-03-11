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
    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0; x < 25; x++)
        {
            temp = Instantiate(tile, tilepos, tilerot, this.transform);
            temp.gameObject.GetComponent<TileScript>().index = x;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPos : MonoBehaviour
{
    public Camera c;
    private Vector3 world;
    // Start is called before the first frame update
    void Start()
    {
        world = c.ScreenToWorldPoint(new Vector3(0, c.pixelHeight * 0.5f, 0));
        transform.position = new Vector3(world.x, world.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

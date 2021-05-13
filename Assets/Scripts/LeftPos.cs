using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPos : MonoBehaviour
{
    public Camera c;
    private Vector3 world;
    public GameObject top;
    public GameObject bot;
    private float scale;
    // Start is called before the first frame update
    void Start()
    {
        world = c.ScreenToWorldPoint(new Vector3(0, c.pixelHeight * 0.5f, 0));
        transform.position = new Vector3(world.x, world.y, 0);

        world = c.ScreenToWorldPoint(new Vector3(0, c.pixelHeight, 0));
        top.transform.position = new Vector3(world.x, world.y, 0);

        world = c.ScreenToWorldPoint(new Vector3(0, 0, 0));
        bot.transform.position = new Vector3(world.x, world.y, 0);

        if(Screen.width < 1200)
        {
            scale = Screen.width / 1200.0f;

            top.transform.localScale = new Vector3(scale, scale, scale);
            bot.transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

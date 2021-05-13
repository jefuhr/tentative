using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForumPos : MonoBehaviour
{
    public Camera c;
    public float offset;
    private Vector3 world;
    public GameObject top;
    public GameObject bot;
    public GameObject scroll;
    private float scale;
    // Start is called before the first frame update
    void Awake()
    {
        world = c.ScreenToWorldPoint(new Vector3(c.pixelWidth - offset, c.pixelHeight * 0.5f, 0));
        transform.position = new Vector3(world.x, world.y, 0);

        world = c.ScreenToWorldPoint(new Vector3(0, c.pixelHeight, 0));
        top.transform.position = new Vector3(top.transform.position.x, world.y, 0);

        world = c.ScreenToWorldPoint(new Vector3(0, 0, 0));
        bot.transform.position = new Vector3(bot.transform.position.x, world.y, 0);

        scale = Screen.height / 634.0f;
        scroll.transform.localScale = new Vector3(1, scale, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

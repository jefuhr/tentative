using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatScript : MonoBehaviour
{
    public bool open = false;
    public float openX;
    public float closeX;
    private float targetX;
    public float speed;
    private bool moving = false;
    private RectTransform rect;
    public Transform forum;
    // Start is called before the first frame update
    void Start()
    {
        rect = forum.gameObject.GetComponent<RectTransform>();
        openX = rect.localPosition.x - 513;
        closeX = rect.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(moving)
        {
            if (Mathf.Abs(rect.localPosition.x - targetX) >= 0.5)
            {
                forum.Translate(new Vector2(targetX - rect.localPosition.x, rect.localPosition.y) * Time.deltaTime * speed);
            }
            else
            {
                moving = false;
                open = !open;
            }
        }
       
    }

    public void Chat_State()
    {
        if(!open && !moving)
        {
            targetX = openX;
            moving = true;
        }
        else if (open && !moving)
        {
            targetX = closeX;
            moving = true;
        }
    }
}

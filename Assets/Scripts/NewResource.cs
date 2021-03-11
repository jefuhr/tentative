using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewResource : MonoBehaviour
{
    private Image i;
    public Image plus;
    public float speed;
    public float fadespeed;
    // Start is called before the first frame update
    void Start()
    {
        i = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        i.transform.Translate(Vector3.up * Time.deltaTime * speed);
        i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (fadespeed * Time.deltaTime));
        plus.color = new Color(plus.color.r, plus.color.g, plus.color.b, plus.color.a - (fadespeed * Time.deltaTime));
        if(i.color.a < 0)
        {
            Destroy(gameObject);
        }
    }
}

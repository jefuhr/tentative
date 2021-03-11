using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMessages : MonoBehaviour
{
    public GameObject message;
    public Transform trans;
    private GameObject temp;
    public int post_count;
    private RectTransform rect;
    private float offset;
    // Start is called before the first frame update
    void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
        offset = -1 * ((rect.offsetMin.y - rect.offsetMax.y) * 0.5f) - 88;
        for (int x = 0; x < post_count; x++)
        {
            temp = Instantiate(message, trans);
            temp.GetComponent<RectTransform>().localPosition = new Vector3(0, offset - (64 * x), transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

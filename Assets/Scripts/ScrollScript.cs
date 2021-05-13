using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollScript : MonoBehaviour
{
    private Scrollbar sb;
    private RectTransform rect;
    public GameObject panel;
    private float start;
    // Start is called before the first frame update
    void Start()
    {
        sb = gameObject.GetComponent<Scrollbar>();
        rect = panel.GetComponent<RectTransform>();
        start = rect.position.y;
    }

    public void Scroll()
    {
        rect.position = new Vector3(rect.position.x, start + (-sb.value * (start * 2)), rect.position.z);
    }
}

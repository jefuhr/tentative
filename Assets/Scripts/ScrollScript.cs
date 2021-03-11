using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollScript : MonoBehaviour
{
    private Scrollbar sb;
    private RectTransform rect;
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        sb = gameObject.GetComponent<Scrollbar>();
        rect = panel.GetComponent<RectTransform>();
    }

    public void Scroll()
    {
        rect.position = new Vector3(rect.position.x, -1 * (18.95f - (sb.value * (18.95f * 2))), rect.position.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour
{
    public GameObject bg;
    public Text type_text;
    public Image type_img;
    public Text timer_text;
    public Text cost1_text;
    public Image cost1_img;
    public Text cost2_text;
    public Image cost2_img;
    public Text cost3_text;
    public Image cost3_img;

    public int build_type = 0;

    public Sprite[] type_sprites = new Sprite[6];
    public Sprite[] res_sprites = new Sprite[6];
    private string[] types = new string[6] {"Clear", "Wild", "Farm", "Lumber", "Quarry", "Mine"};
    public int[,,] costs = new int[6, 3, 2] { 
                                             { {0, 5}, {1, 5}, {2, 5} },
                                             { {0, 10}, {1, 15}, {5, 1} },
                                             { {0, 20}, {1, 20}, {4, 10} },
                                             { {1, 20}, {2, 20}, {4, 10} },
                                             { {1, 20}, {4, 15}, {5, 5} },
                                             { {1, 30}, {4, 25}, {5, 15} } 
                                             };
    private string[] timers = new string[6] { "N/A", "10s", "25s", "15s", "20s", "30s" };

    public bool building = false;

    public BuildButton other;

    private void ChangeTypeVals()
    {
        type_text.text = types[build_type];
        timer_text.text = "Time: " + timers[build_type];
        cost1_text.text = "x" + costs[build_type, 0, 1];
        cost2_text.text = "x" + costs[build_type, 1, 1];
        cost3_text.text = "x" + costs[build_type, 2, 1];

        cost1_img.sprite = res_sprites[costs[build_type, 0, 0]];
        cost2_img.sprite = res_sprites[costs[build_type, 1, 0]];
        cost3_img.sprite = res_sprites[costs[build_type, 2, 0]];

        type_img.sprite = type_sprites[build_type];
        if(build_type == 0)
        {
            type_img.color = new Color(0.29f, 0.41f, 0.18f);
        }
        else
        {
            type_img.color = new Color(1, 1, 1);
        }
    }

    public void ShowBG()
    {
        bg.SetActive(!bg.activeSelf);
        building = bg.activeSelf;
    }

    public void SwapType()
    {
        build_type++;
        if(build_type > 5)
        {
            build_type = 0;
        }

        other.build_type = build_type;

        ChangeTypeVals();
    }

    private void Start()
    {
        ChangeTypeVals();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text.RegularExpressions;

public class PlayerData : MonoBehaviour
{
    public Player player;
    public string playerjson;

    public string ipaddr;

    public class JsonObj
    {
        public string status = "";
        public string contents = "";

        public JsonObj(string act)
        {
            status = act;
        }
    }

    public class AreaObj
    {
        public TileObj[] tiles = new TileObj[25];

        public AreaObj(TileObj[] t)
        {
            for(int x = 0; x < t.Length; x++)
            {
                tiles[x] = t[x];
            }
        }
    }

    public class TileObj
    {
        public int id;
        public int type;
        public int level;

        public TileObj(int i, int t, int l)
        {
            id = i;
            type = t;
            level = l;
        }
    }

    public class Player
    {
        public int uuid;
        public string username;
        public int food;
        public int wood;
        public int stone;
        public int leather;
        public int iron;
        public int gold;
        public int currency0;
        public int currency1;
        public int currency2;
        public int workerCount;
        public string tileJson;
        public AreaObj area;
        //more to come
        public Player()
        {
            uuid = 0;
            username = "";
            food = 0;
            wood = 0;
            stone = 0;
            leather = 0;
            iron = 0;
            gold = 0;
            currency0 = 0;
            currency1 = 0;
            currency2 = 0;
            workerCount = 0;
        }

        public int[] getinv()
        {
            int[] inv = new int[9];
            inv[0] = food;
            inv[1] = wood;
            inv[2] = stone;
            inv[3] = leather;
            inv[4] = iron;
            inv[5] = gold;
            inv[6] = currency0;
            inv[7] = currency1;
            inv[8] = currency2;
            return inv;
        }

        public void setinv(int[] inv)
        {
            food = inv[0];
            wood = inv[1];
            stone = inv[2];
            leather = inv[3];
            iron = inv[4];
            gold = inv[5];
            currency0 = inv[6];
            currency1 = inv[7];
            currency2 = inv[8];
        }

        public void makestring()
        {
            tileJson = "[";
            for(int x = 0; x < 25; x++)
            {
                if(x < 24)
                {
                    tileJson += Regex.Unescape(JsonUtility.ToJson(area.tiles[x]) + ",");
                }
                else
                {
                    tileJson += Regex.Unescape(JsonUtility.ToJson(area.tiles[x]));
                }
            }
            tileJson += "]";
        }

        public void setarea(int[,] tdata)
        {
            TileObj[] temp = new TileObj[25];
            for(int x = 0; x < temp.Length; x++)
            {
                temp[x] = new TileObj(tdata[x, 0], tdata[x, 1], tdata[x, 2]);
            }

            area = new AreaObj(temp);
            makestring();
        }

        public int gettiletype(int index)
        {
            return area.tiles[index].type;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetPlayer(string json)
    {
        playerjson = json.Substring(json.IndexOf("\"contents\":") + 11, json.Length - (json.IndexOf("\"contents\":") + 11) - 1);
        player = JsonUtility.FromJson<Player>(playerjson);

        string temptile = "";
        int first = 0;
        int second = 0;
        TileObj[] temp = new TileObj[25];
        for (int x = 0; x < 25; x++)
        {
            if (player.tileJson.Equals("null"))
            {
                temp[x] = new TileObj(1, 1, 1);
            }
            else
            {
                first = player.tileJson.IndexOf("{", second);
                second = player.tileJson.IndexOf("}", first) + 1;
                temptile = player.tileJson.Substring(first, second - first);

                temp[x] = JsonUtility.FromJson<TileObj>(temptile);
            }
        }

        player.area = new AreaObj(temp);
    }

    public void SetPlayerValues(int[] inv)
    {
        player.setinv(inv);
    }

    public void SetPlayerArea(int [,] area)
    {
        player.setarea(area);
    }

    public void ZeroPlayer()
    {
        player = new Player();
    }

    public void SetIp(string ip)
    {
        ipaddr = ip;
    }
}

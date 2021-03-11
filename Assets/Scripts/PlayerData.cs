using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public Player player;
    public string playerjson;

    public class JsonObj
    {
        public string status = "";
        public string contents = "";

        public JsonObj(string act)
        {
            status = act;
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
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetPlayer(string json)
    {
        playerjson = json.Substring(json.IndexOf("\"contents\":") + 11, json.Length - (json.IndexOf("\"contents\":") + 11) - 1);
        player = JsonUtility.FromJson<Player>(playerjson);
    }

    public void SetPlayerValues(int[] inv)
    {
        player.setinv(inv);
    }

    public void ZeroPlayer()
    {
        player = new Player();
    }
}

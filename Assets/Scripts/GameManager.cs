using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    static GameManager mInstance;
    public Player player;
    public static GameManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                GameObject go = new GameObject();
                mInstance = go.AddComponent<GameManager>();
            }
            return mInstance;
        }
    }

    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>())
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
    }
}

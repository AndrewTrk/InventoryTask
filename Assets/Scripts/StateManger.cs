using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManger : MonoBehaviour
{
    public State state;
    public enum State { Awake , Asleep , inShop1 , inShop2 , InInventory}
    // Start is called before the first frame update
    void SetPlayerState(State state) {
        this.state = state;
    }

}

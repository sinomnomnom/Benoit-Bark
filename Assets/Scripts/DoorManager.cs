using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DoorManager : MonoBehaviour
{
    public GameObject[] doors;

    [YarnCommand("unlock_door")]
    public void UnlockDoor(int doorId)
    {
        if (doors.Length >= doorId * 2)
        {
            doors[doorId *  2].GetComponent<ItemController>().locked = false;
            doors[doorId / 2 + 1].GetComponent<ItemController>().locked = false;
        }
    }
}

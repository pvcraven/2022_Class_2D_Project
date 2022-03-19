using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSwitch : MonoBehaviour
{
    public GameObject objectToTurnOn;
    public GameObject objectToTurnOff;

    public void GameObjectOnOff()
    {
        objectToTurnOn.SetActive(true);
        objectToTurnOff.SetActive(false);
    }
}

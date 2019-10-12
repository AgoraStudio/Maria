using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialog
{
    public GameObject Character;
    public string CharacterName;
    public string DialogText;
    public UnityEvent Event;
    internal bool EventInvoked;
}

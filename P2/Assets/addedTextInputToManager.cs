using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class addedTextInputToManager : MonoBehaviour
{
    public InputField inputField;
    void Start()
    {
        GameManganer.Instance.userName = inputField;
    }

}

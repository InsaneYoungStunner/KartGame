﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
    public InputField myOpenId;
    public Text myPlayerId;
    public Button loginBtn;

    private void Start()
    {
        myOpenId.text = string.Format("Player#{0}", UnityEngine.Random.Range(1, 1000));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    PlayerMovement2D playerMovement2D;
    public TMP_Text emeraldsText;

    public Image healthBar;
    

    // Start is called before the first frame update
    void Start()
    {
        playerMovement2D = GameObject.FindGameObjectWithTag("playerTag").GetComponent<PlayerMovement2D>();
    }

    // Update is called once per frame
    void Update()
    {
        emeraldsText.text = "" + playerMovement2D.gems;
        healthBar.fillAmount = playerMovement2D.currentPlayerHealth / playerMovement2D.maxPlayerHealth;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    PlayerMovement2D playerMovement2D; // Reference to the PlayerMovement2D script.
    public TMP_Text emeraldsText; // Text field to display the player's gem count.
    public Image healthBar; // Image to represent the player's health bar.


    // Start is called before the first frame update
    void Start()
    {
        // Get that script from the game object with the player. 
        playerMovement2D = GameObject.FindGameObjectWithTag("playerTag").GetComponent<PlayerMovement2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the emeraldsText to display the player's gem count.
        emeraldsText.text = "" + playerMovement2D.gems;
        // Update the healthBar's fill amount to represent the player's health as a ratio of current to max health.
        healthBar.fillAmount = playerMovement2D.currentPlayerHealth / playerMovement2D.maxPlayerHealth;
    }
}

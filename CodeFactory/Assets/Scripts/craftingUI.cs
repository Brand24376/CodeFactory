/****************************** Script Header ******************************\
Script Name: craftingUI.cs
Project: CodeFactory
Author: Brandon
Editors: Brandon
Last Edited: February 29th, 2024

<Description>
Contains the functions for the UI of the crafting system.
\***************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class buildingUI : MonoBehaviour
{
    // Holds the crafting system's UI gameobject
    public GameObject crafting;

    // Holds the crafting system's UI icon
    public GameObject craftingIcon;

    // Holds the crafting system's on and off icons
    public Sprite craftingIconOn;
    public Sprite buildingIconOff;

    // Creates a keycode for the interact button
    public KeyCode InteractButton;

    //Create a scirpt variable for Player's controller
    public PlayerController pc;

    // Used to check if the crafting system's UI is open
    public bool open = false;

    // Update is called once per frame
    void Update()
    {
        // If the interact button is pressed and the crafting system's UI is not open
        if (Input.GetKeyDown(InteractButton) && open == false && pc.playing == true && pc.open == false)
        {
            // Turns the inventory icon to its on state
            buildingIcon.GetComponent<Image>().sprite = buildingIconOn;
            // Sets pc.playing to false
            pc.playing = false;
            // Activate the crafting system's UI
            crafting.SetActive(true);
            // Set cursor lock state to none, which allows the player to move the mouse freely
            Cursor.lockState = CursorLockMode.None;
            // Set cursor visibility to true
            Cursor.visible = true;
            // Set open to true
            open = true;
            return;
        }
        // If the interact button is pressed and the crafting system's UI is open
        if (Input.GetKeyDown(InteractButton) && open == true && pc.playing == false)
        {
            // Sets pc.playing to true
            pc.playing = true;
            // Deactivate the crafting system's UI
            crafting.SetActive(false);
            // Set cursor lock state to locked, which prevents the cursor from leaving the game
            Cursor.lockState = CursorLockMode.Locked;
            // Set cursor visibility to false
            Cursor.visible = false;
            // Set open to false
            open = false;
            return;
        }
        // If the inventory is not open
        if (open == false)
        {
            // Turns the crafting system's icon to its off state
            buildingIcon.GetComponent<Image>().sprite = buildingIconOff;
        }
    }
}

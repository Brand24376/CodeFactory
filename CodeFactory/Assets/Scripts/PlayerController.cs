/****************************** Script Header ******************************\
Script Name: PlayerController.cs
Project: CodeFactory
Author: Macarios
Editors: Macarios, Brandon
Last Edited: February 29th, 2024

<Description>
Controls the players movement options.
\***************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Create a float to hold the current players move speed
    public float movespeed;
    //Create a float to hold the current players move speed perc (percent)
    public float movespeedperc;
    //Create a bool to hold if the game is paused or not (playing or not)
    public bool playing = true;
    public bool open = false;
    //Create a transform
    public Transform tf;
    //Create a Quaternion (A quaternion is essentially the rotation of an object, -- 
    // -- with the added w axis, which I'm not sure what it does, but quaternions --
    // -- are more accurate when changing rotations and have more options for objects
    public Quaternion ttf;
    //Create a gameobject for the entire settings menu
    public GameObject SettingsMenu;

    // Creates gameobjects for the UI's below
    public GameObject CodeBlocksMenu;
    public GameObject UI_Shop;
    public GameObject UI_Inventory;
    public GameObject UI_Crafting;
    public GameObject UI_Building;
    public GameObject UI_Icons;

    public CodeBlocks codeBlocks;
    public SalesCubeSystem salesCubeSystem;
    public inventoryUI inventoryUI;
    public buildingUI buildingUI;

    //Create a event for when a key is pressed
    public Event keyHappen;

    //Create a gameobject for the camera's position to "snap" to (return to)
    public GameObject Cam1SnapPos;

    //Create a variable to hold the PlayerMovementLock script
    public PlayerMovementLock LockActivate;
    //Create a variable to hold the PlayerMovementFree script
    public PlayerMovementFree FreeActivate;

    //Create a keycode for changing cameras
    public KeyCode CameraStateKey;
    //Create a keycode for opening the inventory
    public KeyCode InventoryStateKey;

    //Create a camera object for the players camera
    public Camera playerCamera;
    //Create a float for the current mouse's sensitivity
    public float mousesens;
    //Create a float for the rotation on the X axis rotation
    public float rotationX;
    //Create a float for the rotation on the Y axis rotation
    public float rotationY;
    //Create a int for what state the camera is in
    public int CameraState = 1;

    //Create a float for the Y axis
    public float axisY;

    //Create a slider variable for the players move speed
    public Slider MovementSpeed;
    //Create a slider variable for the players mouse sensitivity
    public Slider MouseSensitivity;
    //Create a dropdown variable for the camera's toggling key
    public Dropdown CameraToggleKey;

    //Create a gameobject for the player's body
    public GameObject PlayerBody;

    //Create a bool for resetting
    public bool reset;

    //Create a text variable for the Camera's state on the players UI
    public Text CamText;

    // Start is called before the first frame update
    public void Start()
    {
        //Set the quaternion to the transforms local rotation (not relative to the world, just to the object)
        ttf = tf.transform.localRotation;
    }

    public void Update()
    {
        //Set the movement speed of the player to what ever value the slider is at
        movespeed = MovementSpeed.value;
        //Set the mouse's sensitivity to what ever value the slider is at
        mousesens = MouseSensitivity.value;

        //If the key "Escape" is pressed and the settings menu is not open
        if (Input.GetKeyDown(KeyCode.Escape) && open == false)
        {
            // Disables the UI's listed below
            CodeBlocksMenu.SetActive(false);
            UI_Shop.SetActive(false);
            UI_Inventory.SetActive(false);
            UI_Building.SetActive(false);
            UI_Icons.SetActive(false);
            //Player is in the pause menu now, set playing to false and open to true
            playing = false;
            open = true;
            //Unlock the cursor so that the player can move their cursor without moving the players body
            Cursor.lockState = CursorLockMode.None;
            //Make the players cursor visible
            Cursor.visible = true;
            //Activate the settings menu to show up for the player
            SettingsMenu.SetActive(true);
            //Return to leave the void instead of running the code below
            return;
        }
        //If the key "Escape" is pressed and the settings menu is open
        if (Input.GetKeyDown(KeyCode.Escape) && open == true)
        {
            // Enables the UI's listed below
            UI_Icons.SetActive(true);
            codeBlocks.open = false;
            salesCubeSystem.open = false;
            inventoryUI.open = false;
            buildingUI.open = false;
            SettingsMenu.SetActive(true);
            //Player isn't in the pause menu now, set playing to true and open to false
            playing = true;
            open = false;
            //Deactivate the settings menu and put the player back into the game
            SettingsMenu.SetActive(false);
            //Return to leave the void instead of running the code above
            return;
        }

        //If the key to change camera state is pressed, and the camera is currently free (2)
        if (Input.GetKeyDown(CameraStateKey) && CameraState == 2)
        {
            Debug.Log("Start of camera state switch to 1");
            //Change the text on the player's UI to say "Lock Cam"
            CamText.text = "Lock Cam";
            //Change the color of the text to red
            CamText.color = new Color(255, 0, 0, 255);
            //Set the quaternion of the rotation's y to 0
            ttf.y = 0;
            //ttf.y = PlayerBody.transform.rotation.y; <- Not sure
            //Set this position to the "snap" position
            this.transform.position = Cam1SnapPos.transform.position;
            //Set this rotation to the "snap" rotation
            this.transform.rotation = Cam1SnapPos.transform.rotation;
            //Set the camera state to 1 (Locked)
            CameraState = 1;
            //Set reset to true
            reset = true;
            Debug.Log("End of camera state switch to 1");
            //Return to leave the void instead of running the code below
            return;
        }
        //If the key to change camera state is pressed, and the camera is currently locked (1)
        if (Input.GetKeyDown(CameraStateKey) && CameraState == 1)
        {
            Debug.Log("Start of camera state switch to 2");
            //Change the text on the player's UI to say "Free Cam"
            CamText.text = "Free Cam";
            //Change the color of the text to green
            CamText.color = new Color(0, 255, 0, 255);
            //Using the scripts determined earlier, use that scripts y
            FreeActivate.ttf.y = LockActivate.ttf.y;
            //Set camera state to 2 (Free)
            CameraState = 2;
            //Set reset to true
            reset = true;
            Debug.Log("End of camera state switch to 2");
            //Return to leave the void instead of running code above
            return;
        }

        if (playing == true && open == false)
        {
            // Set cursor lock state to locked, which prevents the cursor from leaving the game
            Cursor.lockState = CursorLockMode.Locked;
            // Set cursor visibility to false
            Cursor.visible = false;
            return;
        }
    }

    // Update is called once per frame (-Slower than Update()- + not fps dependent)
    public void FixedUpdate()
    {
        //If the player is playing
        if (playing == true)
        {
            //If camera state is 1
            if (CameraState == 1)
            {
                //Enabled the LockActivate script
                LockActivate.enabled = true;
                //Disable the FreeActivate script
                FreeActivate.enabled = false;
            }
            //If camera state is 2
            if (CameraState == 2)
            {
                //Disable the LockActivate script
                LockActivate.enabled = false;
                //Enable the FreeActivate script
                FreeActivate.enabled = true;
            }
        }
    }
}

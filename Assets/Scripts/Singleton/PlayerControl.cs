﻿using UnityEngine;

//
// PlayerControl.cs
//
// Author: Eric Thompson (Dead Battery Games)
// Purpose: Handles input for all types of controls
//

public struct ControlObject {
    // Movement
    public float forwardBack;
    public float rightLeft;
    public float upDown;
    public float roll;
    public bool jump;
    public bool changeAssist;
    public bool toggleAssist;
    public bool quantumJump;

    // Aiming
    public float horizontalLook;
    public float verticalLook;
    public bool changeCamera;

    // Player action
    public bool firePrimary;
    public bool fireSecondary;
    public bool aim;
    public bool interact;
    public bool chargeShieldCell;
    public bool light;

    // Weapon types
    public bool changeEquipment;
    public bool yellow;
    public bool blue;
    public bool red;

    // UI
    public bool menuRight;
    public bool menuLeft;
    public bool menuUp;
    public bool menuDown;
}

public class PlayerControl : MonoBehaviour {

    public static PlayerControl instance = null;

    IControllable controlActor = null;

    void Awake() {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    ControlObject currentInput;

    void Start() {
        currentInput = new ControlObject();
        LockMouse();
    }

    void OnApplicationFocus(bool focus) {
        if (focus) LockMouse();
    }

    void LockMouse() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        if (controlActor != null) {
            currentInput.forwardBack = Input.GetAxis("Move Forward/Back");
            currentInput.rightLeft = Input.GetAxis("Move Right/Left");
            currentInput.upDown = Input.GetAxis("Move Up/Down");
            currentInput.roll = Input.GetAxis("Roll");
            currentInput.jump = Input.GetButtonDown("Jump");
            currentInput.changeAssist = Input.GetButtonDown("Change Assist Mode");
            currentInput.toggleAssist = Input.GetButtonDown("Toggle Flight Assist");
            currentInput.quantumJump = Input.GetButtonDown("Quantum Jump");

            currentInput.horizontalLook = Input.GetAxis("Horizontal Look");
            currentInput.verticalLook = Input.GetAxis("Vertical Look");
            currentInput.changeCamera = Input.GetButtonDown("Change Camera");

            currentInput.firePrimary = Input.GetButtonDown("Fire Primary");
            currentInput.fireSecondary = Input.GetButtonDown("Fire Secondary");
            currentInput.aim = Input.GetButtonDown("Equip Energy Pack/Freelook");
            currentInput.interact = Input.GetButtonDown("Interact");
            currentInput.chargeShieldCell = Input.GetButton("Charge Shield Cell");
            currentInput.light = Input.GetButtonDown("Light");

            currentInput.changeEquipment = Input.GetButtonDown("Change Equipment");
            currentInput.yellow = Input.GetButtonDown("Multicannon Yellow");
            currentInput.blue = Input.GetButtonDown("Multicannon Blue");
            currentInput.red = Input.GetButtonDown("Multicannon Red");

            currentInput.menuRight = Input.GetButtonDown("Menu Right");
            currentInput.menuLeft = Input.GetButtonDown("Menu Left");
            currentInput.menuUp = Input.GetButtonDown("Menu Up");
            currentInput.menuDown = Input.GetButtonDown("Menu Down");

            controlActor.CheckInput(currentInput);
        }
    }

    public void TakeControl(IControllable actor) {
        controlActor = actor;
        actor.SetCam(PlayerCamera.instance);
    }

    public void RemoveControl() {
        controlActor = null;
    }

    public bool InControl() {
        return controlActor != null;
    }

    public IControllable GetControllingActor() {
        return controlActor;
    }
}

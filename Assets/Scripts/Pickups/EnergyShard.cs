﻿using UnityEngine;
using System.Collections;

//
// Energy.cs
//
// Author: Eric Thompson (Dead Battery Games)
// Purpose: Pickup script for energy
//

public class EnergyShard : MonoBehaviour, ICollectable {

    Rigidbody rb;
    static float energyUnits = 10f;

    void Awake() {
        // Add rigidbody to scene
        rb = GetComponent<Rigidbody>();
        GameManager.instance.AddGravityBody(rb);

        StartCoroutine("DestroyRigidbody");
    }

    public void Pickup() {
        StartCoroutine("Vapourize");
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (other.GetComponentInChildren<ModuleSlot>().connectedModule) {
                FuelPack fuelPack = other.GetComponentInChildren<FuelPack>();
                if (!fuelPack.IsFull()) {
                    fuelPack.AddFuel(energyUnits);
                    PlayerHUD.instance.ShowEnergySplash();
                    Pickup();
                } else PlayerHUD.instance.SetInfoPrompt("Fuel Pack full");
            } else PlayerHUD.instance.SetInfoPrompt("Not wearing a Fuel Pack");
        }
    }

    IEnumerator DestroyRigidbody() {
        yield return new WaitForSeconds(2f);

        Rigidbody rb = GetComponent<Rigidbody>();
        GameManager.instance.RemoveGravityBody(rb);
        Destroy(rb);
        Destroy(GetComponent<MeshCollider>());
    }

    IEnumerator Vapourize() {
        Destroy(GetComponent<SphereCollider>());
        Destroy(GetComponent<MeshRenderer>());
        Destroy(GetComponent<MeshCollider>());
        GameManager.instance.RemoveGravityBody(rb);
        Destroy(rb);

        GetComponent<ParticleSystem>().Emit(50);

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}

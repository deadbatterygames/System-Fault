﻿using UnityEngine;

//
// Tree.cs
//
// Author: Eric Thompson (Dead Battery Games)
// Purpose: Knocks over trees when they are damaged
//

public class TreeKnocker : MonoBehaviour, IDamageable {

    [SerializeField] GameObject brokenTree;
    [SerializeField] GameObject stump;
    [SerializeField] FlockingObstacle obstacle;

    static float maxHealth = 100f;
    float currentHealth = maxHealth;
    bool separated;

    public void Separate(Vector3 separationVelocity) {
        //FlockingController.DestroySeparator(obstacle.separator);
        //FlockingController.DestroyAttractor(obstacle.attractor);

        Instantiate(stump, transform.position, transform.rotation).transform.localScale = transform.localScale;
        Rigidbody treeRB = Instantiate(brokenTree, transform.position, transform.rotation).GetComponent<Rigidbody>();
        treeRB.AddForce(separationVelocity, ForceMode.Impulse);
        treeRB.transform.localScale = transform.localScale;
        GameManager.instance.AddGravityBody(treeRB);
        separated = true;

        Destroy(gameObject);
    }

    public void Damage(float amount, GameTypes.DamageType damageType, Vector3 damageForce) {
        currentHealth -= amount;
        if (currentHealth <= 0 && !separated) Separate(damageForce);
    }
}

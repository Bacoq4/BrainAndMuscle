using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMoveTrigger : MonoBehaviour
{
    public Rigidbody Rb;
    public Rigidbody Rb2;

    private void Awake() {
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            Rb.AddForce(new Vector3(0,0,-800));
            Rb2.AddForce(new Vector3(0, 0, -800));
        }
    }
}

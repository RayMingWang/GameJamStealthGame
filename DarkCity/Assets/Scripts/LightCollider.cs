using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCollider : MonoBehaviour {

    void OnTriggerStay(Collider other)
    {
        gameObject.SendMessage("EnemySees");
    }
}

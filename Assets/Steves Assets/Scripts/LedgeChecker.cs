using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeChecker : MonoBehaviour
{
    [SerializeField] private Vector3 _handPos;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "LedgeGrabChecker")
        {
            var player = other.transform.parent.GetComponent<Player>();
            player.LedgeGrab(_handPos);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbTopChecker : MonoBehaviour
{
    // Start is called before the first frame update

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LedgeGrabChecker")
        {
            other.GetComponentInParent<Player>().ReachTheTop();
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }
}

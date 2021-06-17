using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderChecker : MonoBehaviour
{
    [SerializeField] BoxCollider climbTopChecker;
    [SerializeField] Player _player;

void Start()
{
    climbTopChecker.enabled = false;
    
}
  void OnTriggerStay(Collider other)
  {
      climbTopChecker.enabled=true;
      if(other.tag == "LadderEnterChecker")
      {
          if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
          {

                  other.GetComponentInParent<Player>().ClimbLadder();

              
          }
      }
  }
}

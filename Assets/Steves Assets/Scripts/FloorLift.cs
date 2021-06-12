using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLift : MonoBehaviour
{
    [SerializeField] private List<Transform> _floors = new List<Transform>();
    private int counter = 1;
    [SerializeField] private float _floorSpeed = 6f;
    [SerializeField] private float _floorStopTimeToWait = 5f;

    private bool _canCheckFloors = true;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _floors[counter].position, _floorSpeed * Time.deltaTime);

        if (_canCheckFloors)
        {            
            if (transform.position == _floors[counter].position)
            {
                StartCoroutine(FloorStop());            
            }
        }


    }

    IEnumerator FloorStop()
    {
        _canCheckFloors=false;
        yield return new WaitForSeconds(_floorStopTimeToWait);
        if (counter == _floors.Count-1)
            {
                counter = 0;
            }
            else
            {
                counter ++;
            }
        yield return new WaitForSeconds(1);
        _canCheckFloors=true;
    }
}
  
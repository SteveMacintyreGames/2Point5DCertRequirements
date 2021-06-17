using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private List<Transform> _wayPoints = new List<Transform>();
    [SerializeField] private int _counter = 0;
    [SerializeField] private float _speed = 6;
    [SerializeField] private float _pauseTime = 2;

    private bool _canCheckWaypoints = true;

    

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        transform.position = Vector3.MoveTowards(transform.position,
         _wayPoints[_counter].position, _speed * Time.deltaTime);
        CheckPosition();
    }

    private void CheckPosition()
    {
        if(_canCheckWaypoints)
        {
            
            if (transform.position == _wayPoints[_counter].position)
            {
                StartCoroutine(PauseBetweenWaypoints());
            }
        }

    }

    IEnumerator PauseBetweenWaypoints()
    {
        _canCheckWaypoints = false;
        yield return new WaitForSeconds(_pauseTime);

        if(_counter == _wayPoints.Count -1)
            {
                _counter = 0;
            }
            else
            {
                _counter++;
            }
        
        yield return new WaitForSeconds(1f);
        _canCheckWaypoints = true;
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger Entered");
        if (other.tag == "Player")
        {
            Debug.Log("Player touching");
            other.transform.parent = this.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}

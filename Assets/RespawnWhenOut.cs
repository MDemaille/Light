using UnityEngine;
using System.Collections;

public class RespawnWhenOut : MonoBehaviour
{
    private Transform _departureTransform;

    void Start()
    {
        _departureTransform = GameObject.FindGameObjectWithTag(Tags.Departure).transform;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Player))
        {
            collision.transform.position = _departureTransform.position;
        }
    }	
}

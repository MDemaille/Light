using UnityEngine;
using System.Collections;

public class Departure : MonoBehaviour {

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Player))
        {
            //TODO : LaunchTimer
        }
    }
}

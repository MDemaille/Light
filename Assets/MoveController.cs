using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour
{
    [HideInInspector]
    public Vector2 Direction;
    public float Speed;

    public virtual void Update()
    {
        transform.Translate(Direction * Speed*Time.deltaTime, Space.World);
    }
}

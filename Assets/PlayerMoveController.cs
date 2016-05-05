using UnityEngine;
using System.Collections;

public class PlayerMoveController : MoveController
{
	public override void Update ()
	{
        Direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        base.Update();
	}
}

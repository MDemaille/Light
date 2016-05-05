using UnityEngine;
using System.Collections;

public class Layer : MonoBehaviour
{
    public ColorEnum Color;
    private LevelManager _manager;

	void Awake ()
	{
	    _manager = GameObject.FindGameObjectWithTag(Tags.LevelManager).GetComponent<LevelManager>();
        _manager.AddElement(gameObject, Color);
	}
}

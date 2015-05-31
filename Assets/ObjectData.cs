using UnityEngine;
using System.Collections;
using System;

public class ObjectData : IComparable<ObjectData> {

	public Vector3 pos;
	public Quaternion rot;
	
	public ObjectData(Vector3 _pos, Quaternion _rot)
	{
		pos = _pos;
		rot = _rot;
	}

	public int CompareTo(ObjectData other)
	{
		if(other == null)
		{
			return 1;
		}

		return (int)pos.x - (int)other.pos.x;
	}
}

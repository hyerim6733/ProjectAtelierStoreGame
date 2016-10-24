﻿using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class FurnitureObject : MonoBehaviour
{
	// field
	[SerializeField] FurnitureData data;
	[SerializeField] bool allocateMode;
	[SerializeField] bool allocatePossible;
	[SerializeField] MeshRenderer objectRenderer;
	[SerializeField] Collider objectCollider;
	[SerializeField] Collider[] tempSet;

	// property
	public FurnitureData Furniture { get { return data; } set { data = value; } }

	public bool AllocateMode { get { return allocateMode; } set { allocateMode = value; } }

	public bool AllocatePossible { get { return allocatePossible; } set { allocatePossible = value; } }

	// unity standard method
	// awake -> set element
	void Awake()
	{
		LinkComponentElement();
	}

	// private method
	// set cast point -> 0.5f set
	private Vector3 SetPoint( Vector3 point, float storeFieldScale )
	{
		float pointX, pointZ;

		// set x point
		if( Mathf.Abs( point.x - ( ( int ) point.x ) ) < 0.25f )
			pointX = ( int ) point.x;
		else if( Mathf.Abs( point.x - ( ( int ) point.x ) ) < 0.75 )
			pointX = ( int ) point.x + 0.5f;
		else
			pointX = ( int ) point.x + 1f;

		Debug.Log( "Point X : " + pointX );

		// set y point
		if( Mathf.Abs( point.z - ( ( int ) point.z ) ) < 0.25f )
			pointZ = ( int ) point.z;
		else if( Mathf.Abs( point.z - ( ( int ) point.z ) ) < 0.75 )
			pointZ = ( int ) point.z + 0.5f;
		else
			pointZ = ( int ) point.z + 1f;

		Debug.Log( "Point Z : " + pointZ );

		return new Vector3( Mathf.Clamp( pointX, ( data.WidthX / 2f ), storeFieldScale - ( data.WidthX / 2f ) ), 0f, Mathf.Clamp( pointZ, ( data.WidthZ / 2f ), storeFieldScale - ( data.WidthZ / 2f ) ) );

		
	}

	// public method
	// Link element & initialize data
	public void LinkComponentElement()
	{
		allocateMode = false;
		objectRenderer = GetComponent<MeshRenderer>();
		objectCollider = GetComponent<Collider>();
	}

	// change object renderer state
	public void ChangeObjectState( bool state )
	{
		objectRenderer.enabled = state;
	}

	// change object position
	public void ChangeObjectPosition( Vector3 point, float storeFieldScale )
	{
		// check overlap furniture
		CheckAllocatePossible();

		// set position
		this.gameObject.transform.position = SetPoint( point, storeFieldScale );
		data.Position = this.gameObject.transform.position;

		// set rotation
		data.Rotation = transform.rotation;
	}

	// change object rotation
	public void ChangeObjectRotation( string direction )
	{
		switch( direction )
		{
			case "Right":
				transform.Rotate( 0, 90, 0 );
				break;
			case "Left":
				transform.Rotate( 0, -90, 0 );
				break;
			case "Reset":
				transform.rotation = new Quaternion( 0f, 0f, 0f, 0f );
				break;	
			case "WallLeft":
				transform.rotation = new Quaternion( 0f, -0.7f, 0f, 0.7f );
				break;
			case "WallRight":
				transform.rotation = new Quaternion( 0f, 0f, 0f, 0f );
				break;
		}


	}

	// check allocate possible
	public bool CheckAllocatePossible()
	{
		tempSet = Physics.OverlapBox( transform.position, new Vector3( data.WidthX, data.Height, data.WidthZ ), transform.rotation, 1 << LayerMask.NameToLayer( "Furniture" ) );

		if( tempSet.Length == 1 )
		{
			allocatePossible = true;
			return true;
		}
		else
		{
			allocatePossible = false;
			return false;
		}
	}
}

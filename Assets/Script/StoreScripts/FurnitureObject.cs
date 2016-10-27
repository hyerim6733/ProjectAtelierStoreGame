﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FurnitureObject : MonoBehaviour
{
	// field
	[SerializeField] FurnitureData data;
	[SerializeField] bool isAllocated;
	[SerializeField] bool allocateMode;
	[SerializeField] bool allocatePossible;
	[SerializeField] Image allocateTexture;
	[SerializeField] Collider[] tempSet;

	// property
	public FurnitureData Furniture { get { return data; } set { data = value; } }

	public bool AllocateMode { get { return allocateMode; } set { allocateMode = allocateTexture.enabled = value; } }

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

		// z axis parallel rotation
		if( Mathf.Abs( gameObject.transform.rotation.y ) == 0.7071068f )
		{
			// set x point
			if( Mathf.Abs( point.x - ( ( int ) point.x ) ) < 0.25f )
				pointX = ( int ) point.x;
			else if( Mathf.Abs( point.x - ( ( int ) point.x ) ) < 0.75 )
				pointX = ( int ) point.x + 0.5f;
			else
				pointX = ( int ) point.x + 1f;
			
			// set y point
			if( Mathf.Abs( point.z - ( ( int ) point.z ) ) < 0.25f )
				pointZ = ( int ) point.z;
			else if( Mathf.Abs( point.z - ( ( int ) point.z ) ) < 0.75 )
				pointZ = ( int ) point.z + 0.5f;
			else
				pointZ = ( int ) point.z + 1f;
			
			// set limit and return position
			return new Vector3( Mathf.Clamp( pointX, ( data.WidthZ / 2f ), storeFieldScale - ( data.WidthZ / 2f ) ), 0f, Mathf.Clamp( pointZ, ( data.WidthX / 2f ), storeFieldScale - ( data.WidthX / 2f ) ) );

		}
		// normal rotation
		else
		{
			// set x point
			if( Mathf.Abs( point.x - ( ( int ) point.x ) ) < 0.25f )
				pointX = ( int ) point.x;
			else if( Mathf.Abs( point.x - ( ( int ) point.x ) ) < 0.75 )
				pointX = ( int ) point.x + 0.5f;
			else
				pointX = ( int ) point.x + 1f;

			// set y point
			if( Mathf.Abs( point.z - ( ( int ) point.z ) ) < 0.25f )
				pointZ = ( int ) point.z;
			else if( Mathf.Abs( point.z - ( ( int ) point.z ) ) < 0.75 )
				pointZ = ( int ) point.z + 0.5f;
			else
				pointZ = ( int ) point.z + 1f;

			// set limit and return position
			return new Vector3( Mathf.Clamp( pointX, ( data.WidthX / 2f ), storeFieldScale - ( data.WidthX / 2f ) ), 0f, Mathf.Clamp( pointZ, ( data.WidthZ / 2f ), storeFieldScale - ( data.WidthZ / 2f ) ) );
		}
		
	}

	// public method
	// Link element & initialize data
	public void LinkComponentElement()
	{		
		// set allocate texture
		GameObject temp = ( GameObject ) Instantiate( Resources.Load<GameObject>( "StoreObject/FurnitureObject/FurnitureAllocateTexture" ), 
		                                              transform.position + new Vector3( 0f, 0.01f, 0f ), 
		                                              Quaternion.Euler( new Vector3( 90f, transform.rotation.eulerAngles.y, 0f ) ) );
		temp.transform.SetParent( this.transform );
		temp.GetComponent<RectTransform>().sizeDelta = new Vector2( data.WidthX, data.WidthZ );
		allocateTexture = temp.GetComponent<Image>();

		AllocateMode = false;
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
		tempSet = Physics.OverlapBox( transform.position, new Vector3( data.WidthX / 2f, data.Height / 2f, data.WidthZ / 2f ), transform.rotation, 1 << LayerMask.NameToLayer( "Furniture" ) );

		if( tempSet.Length == 1 )
		{
			allocatePossible = true;
			allocateTexture.sprite = Resources.Load<Sprite>( "Image/FurnitureTexture/FurnitureTextureGreen" );
			return true;
		}
		else
		{
			allocatePossible = false;
			allocateTexture.sprite = Resources.Load<Sprite>( "Image/FurnitureTexture/FurnitureTextureRed" );
			return false;
		}
	}
}

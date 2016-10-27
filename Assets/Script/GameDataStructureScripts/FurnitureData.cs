﻿using System;
using UnityEngine;

[System.Serializable]
public class FurnitureData
{
	// field
	[SerializeField] int uid;
	[SerializeField] string name;
	[SerializeField] Vector3 position;
	[SerializeField] Quaternion rotation;
	[SerializeField] int height;
	[SerializeField] int widthX;
	[SerializeField] int widthZ;
	[SerializeField] bool isAllocated;
	[SerializeField] FunctionType functionType;
	[SerializeField] AllocateType allocateType;

	// property
	public int UID { get { return uid; } }

	public Vector3 Position { get { return position; } set { position = value; } }

	public Quaternion Rotation { get { return rotation; } set { rotation = value; } }

	public int Height { get { return height; } }

	public int WidthX { get { return widthX; } }

	public int WidthZ { get { return widthZ; } }

	public bool IsAllocated { get { return isAllocated; } }

	public FunctionType Function { get { return functionType; } }

	public AllocateType Allocate { get { return allocateType; } }

	// enum type
	// function type
	public enum FunctionType : int
	{
		Default = 0,
		CreateObject = 1,
		SellObject = 2,
		DecorateObject = 3,
		StorageObject = 4}

	;

	// allocate type
	public enum AllocateType : int
	{
		Default = 0,
		Field = 1,
		Wall = 2,
		Wherever = 3}

	;

	// constructor - no parameter -> set default;
	public FurnitureData()
	{
		uid = 0;
		position = Vector3.zero;
		height = 0;
		widthX = 0;
		widthZ = 0;
		isAllocated = false;
		functionType = FunctionType.Default;
		allocateType = AllocateType.Default;
	}

	// constructor - all parameter -> set up data
	public FurnitureData( int _uid, Vector3 _position, int _height, int _widthX, int _widthZ, bool _isAllocated, FunctionType _functionType, AllocateType _allocateType )
	{
		uid = _uid;
		position = _position;
		height = _height;
		widthX = _widthX;
		widthZ = _widthZ;
		isAllocated = _isAllocated;
		functionType = _functionType;
		allocateType = _allocateType;
	}

	// constructor - data parameter -> make same data instance
	public FurnitureData( FurnitureData data )
	{
		uid = data.uid;
		position = data.position;
		height = data.height;
		widthX = data.widthX;
		widthZ = data.widthZ;
		isAllocated = data.isAllocated;
		functionType = data.functionType;
		allocateType = data.allocateType;
	}
}
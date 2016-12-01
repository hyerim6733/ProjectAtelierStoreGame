﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class UIManager : MonoBehaviour
{
	// high structure
	[SerializeField] GameManager manager;

	// component element
	[SerializeField] GameObject storeUI;
	[SerializeField] StoreUI storeUILogic;

	[SerializeField] GameObject storageUI;
	[SerializeField] StorageUI storageUILogic;

	[SerializeField] GameObject storeCustomizingSet;
	[SerializeField] CustomizeUI storeCustomizingSetLogic;

	[SerializeField] GameObject fieldUI;
	[SerializeField] FieldUI fieldUILogic;

	[SerializeField] GameObject mixUI;
	[SerializeField] MixUI mixUILogic;

	[SerializeField] GameObject sellItemSettingUI;
	[SerializeField] SellItemSettingUI sellItemSettingUILogic;

	[SerializeField] GameObject furnitureMarket;
	[SerializeField] FurnitureMarketUI furnitureMarketUI;

	[SerializeField] GameObject stageUI;
	[SerializeField] StageUI stageUILogic;

	[SerializeField] GameObject loadingScene;
	[SerializeField] Text testField;

	// property
	public GameObject StorageUI { get { return storageUI; } }

	// unity method
	// awake
	void Awake()
	{		
		LinkComponentElement();
	}

	// public method
	// link element component
	public void LinkComponentElement()
	{
		// link structure
		manager = GameObject.FindWithTag( "GameLogic" ).GetComponent<GameManager>();

		// link element component
		storeUI = transform.Find( "StoreUI" ).gameObject;
		storeUILogic = storeUI.GetComponent<StoreUI>();
		storeUILogic.LinkComponentElement();

		storageUI = transform.Find( "StorageUI" ).gameObject;
		storageUILogic = storageUI.GetComponent<StorageUI>();

		storeCustomizingSet = transform.Find( "CustomizeUI" ).gameObject;
		storeCustomizingSetLogic = storeCustomizingSet.GetComponent<CustomizeUI>();
		storeCustomizingSetLogic.LinkComponentElement();

		fieldUI = transform.Find( "FieldUI" ).gameObject;
		fieldUILogic = fieldUI.GetComponent<FieldUI>();

		mixUI = transform.Find( "MixUI" ).gameObject;
		mixUILogic = mixUI.GetComponent<MixUI>();

		sellItemSettingUI = transform.Find( "SellItemSettingUI" ).gameObject;
		sellItemSettingUILogic = sellItemSettingUI.GetComponent<SellItemSettingUI>();

		furnitureMarket = transform.Find( "FurnitureMarketUI" ).gameObject;
		furnitureMarketUI = furnitureMarket.GetComponent<FurnitureMarketUI>();
		furnitureMarketUI.LinkComponentElement();

		stageUI = transform.Find( "StageUI" ).gameObject;
		stageUILogic = stageUI.GetComponent<StageUI>();

		loadingScene = transform.Find( "LoadingScene" ).gameObject;
	}

	// update ui component
	public void UIUpdate()
	{

		if( storeUI.activeSelf )
			storeUILogic.UpdateComponentElement();

		if( storeCustomizingSet.activeSelf )
			storeCustomizingSetLogic.UpdateComponentElement();

		if( fieldUI.activeSelf )
			fieldUILogic.UpdateComponentElement();

		if( furnitureMarket.activeSelf )
			furnitureMarketUI.UpdateComponentElement();

		if( mixUI.activeSelf )
			mixUILogic.CurrentCountManager();

		if( storageUI.activeSelf )
			storageUILogic.UpdateComponentElement();

		if( stageUI.activeSelf )
			stageUILogic.UpdateComponentElement();

		if( sellItemSettingUI.activeSelf )
			sellItemSettingUILogic.UpdateComponentElement();
		
		testField.text = "x:" + Camera.main.transform.position.x + ", y: " + Camera.main.transform.position.y + ", z: " + Camera.main.transform.position.z;
	
	}
	
	// mode change
	public void UIModeChange()
	{
		if( manager == null )
			LinkComponentElement();
		
		switch( manager.PresentMode )
		{
			case GameManager.GameMode.Start:
				storeUI.SetActive( false );
				storageUI.SetActive( false );
				storeCustomizingSet.SetActive( false );
				fieldUI.SetActive( false );
				stageUI.SetActive( false );
				sellItemSettingUI.SetActive( false );
				loadingScene.SetActive( true );
				break;
			case GameManager.GameMode.Loading:
				storeUI.SetActive( false );
				storageUI.SetActive( false );
				storeCustomizingSet.SetActive( false );
				fieldUI.SetActive( false );
				stageUI.SetActive( false );
				sellItemSettingUI.SetActive( false );
				loadingScene.SetActive( true );
				break;
			case GameManager.GameMode.Store:
				storeUI.SetActive( true );
				storageUI.SetActive( false );
				storeCustomizingSet.SetActive( false );
				fieldUI.SetActive( false );
				stageUI.SetActive( false );
				sellItemSettingUI.SetActive( false );
				loadingScene.SetActive( false );
				break;
			case GameManager.GameMode.StoreCustomizing:
				storeUI.SetActive( false );
				storageUI.SetActive( false );
				storeCustomizingSet.SetActive( true );
				fieldUI.SetActive( false );
				break;
			case GameManager.GameMode.StoreOpenPreprocess:
				storeUI.SetActive( false );
				storageUI.SetActive( false );
				storeCustomizingSet.SetActive( false );
				fieldUI.SetActive( false );
				stageUI.SetActive( true );
				sellItemSettingUI.SetActive( false );
				loadingScene.SetActive( false );
				break;
			case GameManager.GameMode.Village:
				break;
			case GameManager.GameMode.Field:
				storeUI.SetActive( false );
				storageUI.SetActive( false );
				storeCustomizingSet.SetActive( false );
				fieldUI.SetActive( true );
				fieldUILogic.ResetCard();
				break;
		}
	}
	
	// set loading scene
	public void LoadingSceneState( bool state )
	{
		loadingScene.SetActive( state );
	}
	

	// set sell item setting io
	public void ActivateSellItemSettingUI()
	{
		sellItemSettingUI.SetActive( true );
		sellItemSettingUILogic.InitializeElement();
		storageUI.SetActive( true );
	}

	// set mix ui activate
	public void ActivateMixUI()
	{
		mixUI.SetActive( true );
		mixUILogic.MixViewButton();
	}

	// set create ui
	
}
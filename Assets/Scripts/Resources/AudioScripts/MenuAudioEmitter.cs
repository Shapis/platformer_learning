using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static AudioClipCatalog;

public class MenuAudioEmitter : BaseAudioEmitter, IMenuEvents, ISceneHandlerEvents
{
    private List<PopupMenuController> testControler = new List<PopupMenuController>();
    public override void InitAwake()
    {

    }

    private void OnCancelPressed(object sender, EventArgs e)
    {
        PlaySfxPermanent(SfxName.MenuClicked, relativeVolume: 1f);
    }

    public override void InitStart()
    {
        SubscribeToEvents();
        SceneManager.sceneLoaded += OnSceneLoad;
        // SceneHandler.OnSceneLoadEvent += OnSceneLoad; // subscribe to scene load event, Only needs to be done once since SceneHandler is static and AudioHandler never gets destroyed.
    }

    public void OnSceneLoad(Scene arg0, LoadSceneMode arg1)
    {
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        FindObjectOfType<InputHandler>().OnCancelPressedEvent += OnCancelPressed;
        foreach (var item in FindObjectsOfType<PopupMenuController>())
        {
            item.OnMenuOpenEvent += OnMenuOpen;
            item.OnMenuCloseEvent += OnMenuClose;
        }
        foreach (var item in FindObjectsOfType<Button>())
        {
            item.onClick.AddListener(() => OnMenuButtonClick(this, EventArgs.Empty));
        }
    }

    public void OnMenuButtonClick(object sender, EventArgs e)
    {
        Debug.Log("MenuButtonClick");
        PlaySfxPermanent(SfxName.MenuClicked, relativeVolume: 1f);
    }
    public void OnMenuOpen(object sender, EventArgs e)
    {
        PlaySfxPermanent(SfxName.MenuClicked, relativeVolume: 1f);
    }
    public void OnMenuClose(object sender, EventArgs e)
    {
        PlaySfxPermanent(SfxName.MenuClicked, relativeVolume: 1f);
    }
    public void OnMenuHover(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

}
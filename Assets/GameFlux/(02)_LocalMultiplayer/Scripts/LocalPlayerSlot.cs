using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayerSlot : MonoBehaviour
{
    public CanvasGroup pressCanvasGroup;
    public bool playerConnected;
    public bool playerReady;
    public bool isFading;
    public int connectedPlayerIndex = -1;

    private void Start()
    {
        isFading = false;
    }

    public void ConnectPlayer(int connectedPlayerIndex)
    {
        if (isFading)
            return;

        playerConnected = true;
        this.connectedPlayerIndex = connectedPlayerIndex;
        isFading = true;

        StartCoroutine(UITools.FadeCanvasGroup(pressCanvasGroup, 0, 0.2f, () =>
        {
            isFading = false;
        }));
    }

    public void DisconnectPlayer()
    {
        if (isFading)
            return;

        isFading = true;

        StartCoroutine(UITools.FadeCanvasGroup(pressCanvasGroup, 1, 0.2f, () =>
        {
            isFading = false;
            connectedPlayerIndex = -1;
            playerConnected = false;
        }));
    }

    public void Ready()
    {
        if (playerReady)
            return;

        playerReady = true;
    }

    public void Cancel()
    {
        if (!playerReady)
            return;

        playerReady = false;
    }
}

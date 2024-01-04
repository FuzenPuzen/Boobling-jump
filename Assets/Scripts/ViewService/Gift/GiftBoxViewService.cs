using System.Collections;
using UnityEngine;

public class GiftBoxViewService
{
    private GiftBoxView _giftBoxView;
    public void ActivateService(GiftBoxView giftBoxView)
    {
        _giftBoxView = giftBoxView;
    }
}
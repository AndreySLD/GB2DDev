using Model.Shop;
using System;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class GoldShopController : BaseController
{
    private ProfilePlayer _profilePlayer;
    private ShopTools _shopTools;
    private MainMenuView _view;
    private List<ShopProduct> _shopProducts;

    [SerializeField] private int _smallPackGoldAmount = 50;

    public GoldShopController(ProfilePlayer profilePlayer, ShopTools shopTools, MainMenuView view, List<ShopProduct> shopProducts)
    {
        _profilePlayer = profilePlayer;
        _shopTools = shopTools;
        _view = view;
        _shopProducts = shopProducts;

        _shopTools.OnSuccessPurchase.SubscribeOnChange(AddSmallGoldPackToPlayer);
    }
    private void AddSmallGoldPackToPlayer()
    {
        _profilePlayer.GoldAmount =+ _smallPackGoldAmount;
        _view.UpdateGoldPanel(_profilePlayer.GoldAmount);
    }
    protected override void OnDispose()
    {
        _shopTools.OnFailedPurchase.UnSubscriptionOnChange(AddSmallGoldPackToPlayer);
        base.OnDispose();
    }

}

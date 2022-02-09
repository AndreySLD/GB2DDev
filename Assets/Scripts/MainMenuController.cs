using System.Collections.Generic;
using Model.Analytic;
using Model.Shop;
using Profile;
using Tools.Ads;
using UnityEngine;

public class MainMenuController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/mainMenu"};
    private readonly ProfilePlayer _profilePlayer;
    private readonly IAnalyticTools _analytics;
    private readonly IAdsShower _ads;
    private readonly MainMenuView _view;

    private GoldShopController _goldShopController;
    private ShopTools _shopTools;

    List<ShopProduct> _shopProducts;

    public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, IAnalyticTools analytics, IAdsShower ads)
    {
        _profilePlayer = profilePlayer;
        _analytics = analytics;
        _ads = ads;
        _view = LoadView(placeForUi);
        _view.Init(StartGame, _profilePlayer.GoldAmount);

        _shopProducts = new List<ShopProduct>()
        {
            new ShopProduct ("com.c1.racing.goldPack", UnityEngine.Purchasing.ProductType.Consumable),
        };

        _shopTools = new ShopTools(_shopProducts);
        _goldShopController = new GoldShopController(_profilePlayer, _shopTools, _view, _shopProducts);
        AddController(_goldShopController);
    }
    private MainMenuView LoadView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObjects(objectView);
        
        return objectView.GetComponent<MainMenuView>();
    }

    private void StartGame()
    {
        _analytics.SendMessage("Start", new Dictionary<string, object>());
        _ads.ShowInterstitial();
        _profilePlayer.CurrentState.Value = GameState.Game;
    }
}


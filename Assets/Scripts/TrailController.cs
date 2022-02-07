using Profile;
using Tools;
using UnityEngine;

public class TrailController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/Trail" };
    private readonly ProfilePlayer _profilePlayer;
    private readonly TrailView _view;

    public TrailController()
    {
        _view = LoadView();
        _view.Init();
    }
    private TrailView LoadView()
    {
        var view = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
        AddGameObjects(view);

        return view.GetComponent<TrailView>();
    }
}

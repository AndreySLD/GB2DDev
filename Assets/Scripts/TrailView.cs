using JoostenProductions;
using UnityEngine;

public class TrailView : MonoBehaviour
{
    [SerializeField] private GameObject TrailPivot;
    [SerializeField] private GameObject TrailPrefab;
    [SerializeField] private float _cameraPivot;
    
    public void Init()
    {
        TrailPrefab.SetActive(false);
        TrailPivot.transform.position = Vector3.zero;
        UpdateManager.SubscribeToUpdate(OnUpdate);
    }
    private void OnUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            TrailPrefab.SetActive(true);
            var x = Input.mousePosition.x;
            var y = Input.mousePosition.y;
            var z = _cameraPivot;
            var cameraVector = new Vector3(x, y, z);
            TrailPivot.transform.position = Camera.main.ScreenToWorldPoint(cameraVector);
        }
        else if (Input.touchCount > 0)
        {
            TrailPrefab.SetActive(true);
            var touch = Input.GetTouch(0);
            var x = touch.position.x;
            var y = touch.position.y;
            var z = _cameraPivot;
            var cameraVector = new Vector3(x, y, z);
            TrailPivot.transform.position = Camera.main.ScreenToWorldPoint(cameraVector);
        } 
        else
        {
            TrailPrefab.SetActive(false);
        }
    }
    protected void OnDestroy()
    {
        UpdateManager.UnsubscribeFromUpdate(OnUpdate);
        Destroy(TrailPrefab);
    }
}

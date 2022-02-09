using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _buttonStart;
    [SerializeField] private Text _goldAmount;

    public void Init(UnityAction startGame, int goldValue)
    {
        _buttonStart.onClick.AddListener(startGame);
        UpdateGoldPanel(goldValue);
    }
    public void UpdateGoldPanel(int value)
    {
        _goldAmount.text = value.ToString();
    }
    protected void OnDestroy()
    {
        _buttonStart.onClick.RemoveAllListeners();
    }
}
using JoostenProductions;
using Tools;
using UnityEngine;

public class InputSwipeView : BaseInputView
{
    private Vector2 _startPosition;
    private Vector2 _finishPosition;
    private Vector3 _swipe;

    [SerializeField] private float _acceleration = 1.0f;
    [SerializeField] private float _slowDownPerSecond = 0.2f;
    [SerializeField] private float _maxSpeed = 1.0f;

    public override void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, float speed)
    {
        base.Init(leftMove, rightMove, speed);
        UpdateManager.SubscribeToUpdate(OnUpdate);
    }
    private void OnUpdate()
    {
        if (Input.touches.Length > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                _startPosition = new Vector2(touch.position.x, touch.position.y);
            }
            if (touch.phase == TouchPhase.Ended)
            {
                _finishPosition = new Vector2(touch.position.x, touch.position.y);
                _swipe = new Vector3(_finishPosition.x - _startPosition.x, _finishPosition.y - _finishPosition.x);
                _swipe.Normalize();
            }
            if (_swipe.x < 0)
            {
                AddAcceleration(-_acceleration);
            }
            if (_swipe.x > 0)
            {
                AddAcceleration(_acceleration);
            }
        }

        Move();
        AddSlowdown();
    }
    private void Move()
    {
        if (_speed < 0)
            OnLeftMove(_speed);
        else
            OnRightMove(_speed);
    }
    private void AddAcceleration(float acc)
    {
        _speed = Mathf.Clamp(_speed + acc, -_maxSpeed, _maxSpeed);
    }
    private void AddSlowdown()
    {
        var sgn = Mathf.Sign(_speed);
        _speed = Mathf.Clamp01(Mathf.Abs(_speed) - _slowDownPerSecond * Time.deltaTime) * sgn;
    }
    private void OnDestroy()
    {
        UpdateManager.UnsubscribeFromUpdate(OnUpdate);
    }
}

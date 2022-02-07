using JoostenProductions;
using Tools;
using UnityEngine;

public class GyroInputView : BaseInputView
{
    public override void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, float speed)
    {
        base.Init(leftMove, rightMove, speed);
        Input.gyro.enabled = true;
        UpdateManager.SubscribeToUpdate(Move);
    }
    private void Move()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Quaternion quaternion = Input.gyro.attitude;
            quaternion.Normalize();
            OnRightMove((quaternion.x + quaternion.y) * Time.deltaTime * _speed);
        }
        else
        {
            return;
        }
    }
    private void OnDestroy()
    {
        UpdateManager.UnsubscribeFromUpdate(Move);
    }
}

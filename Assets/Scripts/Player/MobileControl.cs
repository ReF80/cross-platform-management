using UnityEngine;

public class MobileControlState : PlayerControlState
{
    private Joystick _joystick;

    public MobileControlState(Joystick joystick)
    {
        _joystick = joystick;
    }

    public override void HandleInput(PlayerController player)
    {
        Vector2 input = new Vector2(
            _joystick.Horizontal,
            _joystick.Vertical
        );

        MovePlayer(player, input);
    }

    public override void UpdateState(PlayerController player)
    {
        // Дополнительная логика обновления для мобильных устройств
    }
}
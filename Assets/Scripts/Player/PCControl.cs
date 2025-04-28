using UnityEngine;

public class PCControlState : PlayerControlState
{
    public override void HandleInput(PlayerController player)
    {
        Vector2 input = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        MovePlayer(player, input);
    }

    public override void UpdateState(PlayerController player)
    {
        // Дополнительная логика обновления для ПК
    }
}
using UnityEngine;

// Интерфейс состояния управления
public interface IPlayerControlState
{
    void HandleInput(PlayerController player);
    void UpdateState(PlayerController player);
}

// Базовый класс состояния
public abstract class PlayerControlState : IPlayerControlState
{
    public abstract void HandleInput(PlayerController player);
    public abstract void UpdateState(PlayerController player);
    
    protected void MovePlayer(PlayerController player, Vector2 input)
    {
        player.ProcessMovement(input);
    }
}
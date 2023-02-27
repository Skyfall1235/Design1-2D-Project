using UnityEngine;

public enum WeaponType
{
    Stick,
    Knife,
    Sword
}
public enum Action
{
    jump,
    Dash,
    WallClimb
}


[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public Vector2 playerPosition;
    public Vector2 cameraPosition;
    public int coinAmount;
    public float walkSpeed;
    public float dashDistance;
    public float jumpForce;
    public int currentHealth;
    public WeaponType weaponType;
}

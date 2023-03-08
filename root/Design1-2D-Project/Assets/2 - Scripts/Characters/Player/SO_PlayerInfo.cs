using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public Vector2 playerPosition;
    //is needed to return the payers posisiton to exactly where it was
    public bool facingRight;
    //this contains an array of an array. each LEVEL is the stagecameraarray, and each CAMERA position FOR THAT LEVEL is stored inside the inner array.
    public Array[] stageCameraArray;
    //this is required to hold the stage name and its camera positions
    public int[] cameraLocation;
    //this exists currently to potentially be used
    public int coinAmount;
    //the base walkspeed of the player
    public float walkSpeed;
    //the amount of FORCE applied to a DASH
    public float dashDistance;
    //the amount of forced applied for a JUMP
    public float jumpForce;
    //the current health value of the player
    public int currentHealth;
    //the urrent weapontype
    public WeaponType weaponType;
}

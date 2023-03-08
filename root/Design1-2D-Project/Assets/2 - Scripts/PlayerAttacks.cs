using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    //what varaibles do i need? i need all the instantiation locations on the player, he cooldown of the attacks for the player?
    //and maybe the weapon types fo at leas t the animation triggers.

    //we can let the inantiation determine the damage through a seperate script rather than require it to be set here
    //(on the fly assignment is tricky because of frame alignment)

    //first, i need control for the availavle actions that the player can do.
    //probably a switch case



    //each method should be a coroutine as it will need to  run over a series of TIME, NOT JUST BE INSTANTANIOUS
    //each method will take the instantiation locations and spawn the respective prefab for it

}

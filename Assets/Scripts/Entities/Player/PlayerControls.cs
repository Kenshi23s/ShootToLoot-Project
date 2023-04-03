using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : IPlayerControler
{
    //touchControls
    PlayerMovement PlayerMovement;
    public DragJoystick[] Stick;
    public PlayerControls(PlayerMovement playerMovement, DragJoystick[] stick)
    {
       PlayerMovement = playerMovement;
       Stick = stick;
    }
    public void InitializeControler(PlayerMovement p)
    {
        PlayerMovement = p;
    }
    public void AsignVirtualSticks(DragJoystick[] Stick)
    {
        Debug.Log(Stick);
        this.Stick = Stick;
    }
    public void ControlerArtificialUpdate()
    {
        
        if (Stick[0].ValueStick != Vector3.zero)
        {
            
            PlayerMovement.Move(Stick[0].ValueStick);
        }
        else
        {

            PlayerMovement.StopMoving();
        }

        PlayerMovement.DarRotacion(Stick[1].ValueStick);
    }

    
    
}

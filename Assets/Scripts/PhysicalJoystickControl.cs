using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PhysicalJoystickControl : IPlayerControler
{
   public PhysicalJoystickControl (PlayerMovement P,Transform _transform)
   {
       _playerMovement=P;
       this._transform = _transform;
   }
    PlayerMovement _playerMovement;
    Transform _transform;

    public void InitializeControler(PlayerMovement p)
    {
        _playerMovement = p;
       
    }

    public void ControlerArtificialUpdate()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");
        if (xMove != 0|| yMove != 0)
        {
          

            _playerMovement.Move(new Vector3(xMove,yMove,0));
        }
        else
        {

            _playerMovement.StopMoving();
        }
        float xRotation = Input.GetAxisRaw("HorizontalCam");
        float yRotation = Input.GetAxisRaw("VerticalCam");

        Vector3 origin = new Vector2(0,0f);
        Vector3 resolution = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);

        Vector3 dir =  Input.mousePosition - origin - (resolution / 2);
       
       


        _playerMovement.DarRotacion(new Vector3(dir.x, dir.y, 0));
    }

    
}

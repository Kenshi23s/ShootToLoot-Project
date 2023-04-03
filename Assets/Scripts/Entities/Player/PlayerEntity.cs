using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerEntity : Entity
{
    public static PlayerEntity PlayerInstance;

    [SerializeField]
    public DragJoystick[] Stick = new DragJoystick[2];

    private Rigidbody rb;

    public float maxVelocity = 10, speed = 5, BrakeLimit = 0.1f;

    [Range(0, 1)]
    public float slip = 0.3f;

    [Range(0, 1)]
    public float InfluenceLimit = 0.5f;

    // no puede pasar a slip

    //MVC
    IPlayerControler _controls;
    PlayerMovement _movement;

    public LayerMask PlayerLayerMask;


    #region MVC

    public override void StartMethod() => GameManager.instance.InitializeController += ActualControl;
   
 

    void ActualControl(GameManager.InputMethod myInput)
    {
        // primero hacemos el movement y despues se lo pasamos al controls,
        // porque sino le estariamos pasando null
        

        rb = this.GetComponent<Rigidbody>();
        _movement = new PlayerMovement(this.transform, rb, speed, maxVelocity, slip, BrakeLimit, InfluenceLimit);

        switch (myInput)
        {
            
            case GameManager.InputMethod.E_VirtualJoystick:
                {
                    _controls= new PlayerControls(_movement, Stick);
                    break;
                }                
                
            case GameManager.InputMethod.E_Joystick:
                {
                    _controls = new PhysicalJoystickControl(_movement,transform);
                    //Cursor.lockState = CursorLockMode.Locked;
                    break;
                }

            default:
                {
                    _controls = new PhysicalJoystickControl(_movement,transform);
                    //Cursor.lockState = CursorLockMode.Locked;
                    break;
                }                      

        }
    }

    private void Update() => _controls.ControlerArtificialUpdate();
    
      
       
    
    #endregion

    #region EntityHerency
    public override void OnTakeDamage(int dmg)
    {
        FeedBackDamage(dmg, Aclip);
        life -= dmg;

        if (life<=0)
        {
            Die();
        }
    }

    public override void Die()
    {

        
        GameManager.instance.EndLevel(false);
        return;
    }

    public override void OnDestroyCheck()
    {
        return;
    }
    #endregion;

    private void OnDrawGizmos()
    {
        Vector3 dir = Input.mousePosition - transform.position;
        dir.Normalize();
        Gizmos.DrawLine(transform.position, dir);
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public static GunManager instance;

    public DragJoystick ShootStick;
    public GunFather defaultGun, actualGun;

    public Action InputAnswer;

    [SerializeField]
    float ShootDeadZone=60f;

    public enum chooseGun
    {
        Epistol,
        Eshotgun,
        EAssaultRifle
    }
    private chooseGun gun;
    [SerializeField] private Pistol _pistol;
    [SerializeField] private SubMachineGun _subMachineGun;
    [SerializeField] private Shotgun _shotgun;
    public List<GunFather> Guns = new List<GunFather>();

    private void Awake()=> instance = this;



    private void Start()
    {
        GameManager.instance.InitializeController += ActualInputMethod;
        
        Guns.Add(_pistol);
        Guns.Add(_shotgun);
        Guns.Add(_subMachineGun);       
        if (actualGun == null)
        {
            ChangeGun(defaultGun);
        }
        // hacer un switch para definir a q tipo de input le hace caso, como por ahora solo esta hecho el de celular dejo ese
       
        //ActualInputMethod(out InputAnswer);
    }
    void ActualInputMethod(GameManager.InputMethod InputMethod)
    {
        switch (InputMethod)
        {

            case GameManager.InputMethod.E_VirtualJoystick:
                {

                    InputAnswer = CheckMobileInputs;
                    break;
                }

            case GameManager.InputMethod.E_Joystick:
                {
                    InputAnswer = CheckPCInputs;
                    break;
                }

            default:
                {

                    InputAnswer = CheckPCInputs;
                    break;
                }



        }
    }

    private void Update() => InputAnswer?.Invoke();



    void CheckMobileInputs()
    {
        if (ShootStick!=null)
        {
            if (ShootStick.ValueStick.magnitude > ShootDeadZone)
            {
                PullTrigger();                
            }
            else
            {
                if (actualGun.GunAnimator != null)
                {
                    actualGun.GunAnimator.SetBool("IsShooting", false);
                }

            }

        }
        ////disparo especial, para futuro
        //if (Input.GetKey(KeyCode.K))
        //{
        //    TryToUseSpecial();
        //}
    }

    void CheckPCInputs()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            PullTrigger();
        }
        else
        {
            if (actualGun.GunAnimator != null)
            {
                actualGun.GunAnimator.SetBool("IsShooting", false);
            }

        }
    }

    void PullTrigger()
    {
        actualGun.OnTriggerPull();

        if (actualGun.GunAnimator != null)
        {
            actualGun.GunAnimator.SetBool("IsShooting", true);
        }

    }
    /// <summary>
    /// este metodo activa el arma actual, y desactiva las otras 
    /// </summary>
    /// <param name="gun"></param>
    void ChangeGun(GunFather gun)
     {
        actualGun = gun;

        for (int i = 0; i < Guns.Count; i++)
        {

            if (Guns[i] != actualGun)
            {
                Guns[i].gameObject.SetActive(false);
            }
            else
            {
                SpecialGuns is_Special = gun.GetComponent<SpecialGuns>();
                if (is_Special != null)
                {

                }
                Guns[i].gameObject.SetActive(true);
                Guns[i].canShoot = true;
                Guns[i].ToEquip();



            }

        }
        //print(actualGun.name);

     }
    /// <summary>
    /// este metodo le dice al gun manager que arma deberia usar el player, 
    /// nesecita que se le pase por parametro un enum del tipo "choose gun"
    /// </summary>
    public void EquipGun(chooseGun gun)
    {

        switch (gun)
        {
            case chooseGun.EAssaultRifle:
                ChangeGun(_subMachineGun);
                break;
            case chooseGun.Eshotgun:
                ChangeGun(_shotgun);
                break;
            default:
                ChangeGun(_pistol);
                break;
        }

    }

    void TryToUseSpecial()=> actualGun.SpecialAbilityTrigger();

    void ChargeSpecial(int charge)=> actualGun.AddGunCharge(charge);
   
}

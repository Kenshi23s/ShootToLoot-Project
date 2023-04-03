using UnityEngine;

public class GunGraber : SuckTowardsTrigger
{
    public GunManager.chooseGun myGun;     
    public override void EffectTrigger(Collider other)
    {
        var is_player = other.gameObject.GetComponent<PlayerEntity>();
        if (is_player != null)
        {
            GunManager.instance.EquipGun(myGun);
            Destroy(this.gameObject);

        }
    }
}

using UnityEngine;

public class GrabGems : SuckTowardsTrigger
{
    public AudioClip sound_clip;
    [SerializeField]
    int value;

    public override void EffectTrigger(Collider other)
    {
        var Player = other.transform.gameObject.GetComponent<PlayerEntity>();

        if (Player != null)
        {
            AudioPool.instance.SpawnAudio(sound_clip,transform.position);
            GameManager.instance.AddGems(value);
            Destroy(this.gameObject);
        }
    }
}

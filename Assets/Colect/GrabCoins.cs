using UnityEngine;

public class GrabCoins : SuckTowardsTrigger
{
    public AudioClip sound_clip;
    [SerializeField]
    int _value=1;

    public override void EffectTrigger(Collider other)
    {
        var Player = other.transform.gameObject.GetComponent<PlayerEntity>();

        if (Player != null)
        {
            AudioPool.instance.SpawnAudio(sound_clip, transform.position);
            GameManager.instance.AddCoins(_value);
            Destroy(this.gameObject);
        }
    }
}

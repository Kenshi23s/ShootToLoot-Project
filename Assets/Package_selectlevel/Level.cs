using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct LevelData
{
    public bool Locked;
    public string NameLevel;

    public float Rocks;
    public float Enemies;
    public float Boxes;
    public float Guns;

    public int Alto;
    public int Ancho;
}

public class Level : MonoBehaviour
{
    public TextMeshPro nombre;
    public Renderer VisualIcon;

    public LevelData MainLevelData;

    public Material MatLock;
    public Material MatUnlock;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,1.4f);
    }

    public void ActualizarLevel()
    {
        if (MainLevelData.Locked)
        {
            this.VisualIcon.material = this.MatLock;
            this.nombre.alpha = 0;
        }
        else
        {
            this.VisualIcon.material = this.MatUnlock;
            this.nombre.text = this.MainLevelData.NameLevel;
            this.nombre.alpha = 1;
        }
    }

}

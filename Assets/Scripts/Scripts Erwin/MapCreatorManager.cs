using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreatorManager : MonoBehaviour
{
    public static MapCreatorManager instance;
    public MapCreator MainMapCreator;

    public static LevelData StaticLevelData;

    private void Awake()
    {
        instance = this;

        if (MainMapCreator==null)
        {
            ConfigMapCreator(GetParameters());
        }
    }
    public GameObject InstanciarObjeto(GameObject objeto, Vector3 pos, Quaternion quat) => Instantiate(objeto, pos, quat);
  

    public void ConfigMapCreator(LevelData MainLevelData)
    {

        MainMapCreator.Ancho = MainLevelData.Ancho;
        MainMapCreator.Largo = MainLevelData.Alto;
        MainMapCreator.EnemigosPorcentaje = MainLevelData.Enemies;
        MainMapCreator.CantidadCajas = MainLevelData.Boxes;
        MainMapCreator.CantidadArmas = MainLevelData.Guns;
        MainMapCreator.CantidadPiedas = MainLevelData.Rocks;

    }

    public static void SetParameters(LevelData MainLevelData)
    {
        StaticLevelData = MainLevelData;
    }

    public static LevelData GetParameters()
    {
        return StaticLevelData;
    }
}

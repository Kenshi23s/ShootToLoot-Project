using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapCreator : MonoBehaviour
{
    //recursos
    public GameObject[] Objeto;
    public GameObject[] Muros;
    public GameObject[,] Chunk;
    public GameObject ChunkBase;
    public Transform Player;
    public Transform FocusCam;


    public int[,] Grilla;

    //el limite maximo del mapa es de 200
    public int Largo = 200;
    public int Ancho = 200;
    public int DivisionDeChunks = 20;

    //parametros de habitaciones

    int AnchoHabitacionMax = 6;
    int AnchoHabitacionMin = 4;

    int DistanciaEntreHabitacionesMax = 17;
    int DistanciaEntreHabitacionesMin = 15;

    public int CantidadHabitaciones = 30;
    public float Grosor = 1.5f;
    int Margen = 1;

    public int Borde = 8;
    public float TamañoDeBloque = 1.7f;

    public float CantidadCajas = 30;
    public float CantidadCarpas = 30;
    public float CantidadPiedrasGrandes = 30;
    public float CantidadArmas = 0.4f;
    public float CantidadPiedas = 0.4f;

    public float EnemigosPorcentaje = 0.3f;

    //parametros de actualizacion
    int CantidadLlaves = 3;
    public float DistanciaDeDibujado = 40;

    // Start is called before the first frame update
    public virtual void Start()
    {
        MapaDefaultAleatorio();

        GrillaCrearMapa(Grilla, TamañoDeBloque, Chunk, Objeto, DivisionDeChunks, ChunkBase);

    }

    public void ConfiguracionVacia(int llenar)
    {
        Grilla = new int[Largo, Ancho];

        Chunk = new GameObject[(Largo / DivisionDeChunks) + 1, (Ancho / DivisionDeChunks) + 1];

        GrillaMuros(Muros, TamañoDeBloque, Largo, Ancho);

        Grilla = GrillaReset(Grilla, llenar);
    }

    //Metodo predeterminado para todo
    void MapaDefaultAleatorio()
    {
        ConfiguracionVacia(1);

        //----------------------------


        Grilla = GrillaHabitaciones(Grilla, Borde, AnchoHabitacionMax, AnchoHabitacionMin, DistanciaEntreHabitacionesMax, DistanciaEntreHabitacionesMin, Margen, CantidadHabitaciones, Grosor);

        Grilla = PonerLLaveFinal(Grilla, 3, 11, 10, 4);

        //----------------------------

        Grilla = GrillaDarBordes(Grilla, Borde, 1);

        Grilla = SuavizadoProps(Grilla);

        //Objectos particulares
        Grilla = PonerCarpasSpawn(Grilla, CantidadCarpas);

        Grilla = PonerPiedrasGrandes(Grilla, CantidadPiedrasGrandes);

        Grilla = PonerSpawnsEnemigos(Grilla, EnemigosPorcentaje, 9);

        Grilla = PonerSpawnsEnemigos(Grilla, EnemigosPorcentaje, 10);

        Grilla = PonerSpawnsEnemigos(Grilla, EnemigosPorcentaje, 12);

        //Objetos adiccionales de destruccion

        //poner piedras (indestructible)
        Grilla = GrillaCambiarObjetosPorOtros(Grilla, 1, 3, 10);

        //poner cajas
        Grilla = GrillaCambiarObjetosPorOtros(Grilla, 1, 2, CantidadCajas);

        //provisorio para probar cosas

        //poner escopeta (provisorio) -----------------------------<<<
        Grilla = GrillaCambiarObjetosPorOtros(Grilla, 0, 8, CantidadArmas);

        //poner metralleta (provisorio) -----------------------------<<<
        Grilla = GrillaCambiarObjetosPorOtros(Grilla, 0, 7, CantidadArmas);


        Debug.Log(GrillaPrintear(Grilla, false));
    }

    // Update is called once per frame
    public virtual void LateUpdate()
    {
        PopDeObjetos(Chunk, DistanciaDeDibujado);
    }

    int[,] GrillaReset(int[,] Grilla, int valor)
    {
        for (int i = 0; i < Grilla.GetLength(0); i++)
        {
            for (int j = 0; j < Grilla.GetLength(1); j++)
            {
                Grilla[i, j] = valor;
            }
        }

        return Grilla;
    }

    string GrillaPrintear(int[,] Grilla, bool Ilustrado)
    {
        string Print = "";

        for (int i = 0; i < Grilla.GetLength(0); i++)
        {
            for (int j = 0; j < Grilla.GetLength(1); j++)
            {
                if (Ilustrado)
                {
                    switch (Grilla[i, j])
                    {
                        case 0:
                            Print += " ";
                            break;
                        case 1:
                            Print += "?";
                            break;
                        default:
                            Print += "X";
                            break;
                    }
                }
                else
                {
                    Print += Grilla[i, j];
                }

                Print += "";

            }

            Print += "\n";
        }

        Debug.Log(Grilla.GetLength(0) + " - " + Grilla.GetLength(1));

        return Print;
    }

    public void GrillaCrearMapa(int[,] Grilla, float TamañoDeBloque, GameObject[,] Chunk, GameObject[] Objetos, int DivisionDeChunks, GameObject ChunkBase)
    {
        for (int i = 0; i < Chunk.GetLength(0); i++)
        {
            for (int j = 0; j < Chunk.GetLength(1); j++)
            {

                Chunk[i, j] = MapCreatorManager.instance.InstanciarObjeto(ChunkBase, new Vector3((j + 0.5f) * TamañoDeBloque * DivisionDeChunks, 0, -(i + 0.5f) * TamañoDeBloque * DivisionDeChunks),Quaternion.identity);

            }
        }

        for (int i = 0; i < Grilla.GetLength(0); i++)
        {
            for (int j = 0; j < Grilla.GetLength(1); j++)
            {

                if (Grilla[i, j] >= 0)
                {
                    if (Objeto[Grilla[i, j]] != null)
                    {
                        GameObject Tempo = MapCreatorManager.instance.InstanciarObjeto(Objeto[Grilla[i, j]], new Vector3(j * TamañoDeBloque, 0, -i * TamañoDeBloque), Quaternion.identity);

                        Tempo.transform.parent = Chunk[(int)i / DivisionDeChunks, (int)j / DivisionDeChunks].transform;
                    }
                }
            }
        }
    }

    int[,] GrillaHabitaciones(int[,] Grilla, int Borde, int MaxTamañoDeHabitacion, int MinTamañoDeHabitacion, int MaxPosHabitacion, int MinPosHabitacion, int Margen, int CantidadHabitaciones, float Grosor)
    {

        //colocar spawn del jugador
        int SpawnY = Random.Range(0 + Borde + MaxTamañoDeHabitacion, Grilla.GetLength(0) - Borde - MaxTamañoDeHabitacion - 1);
        int SpawnX = Random.Range(0 + Borde + MaxTamañoDeHabitacion, Grilla.GetLength(1) - Borde - MaxTamañoDeHabitacion - 1);

        Grilla[SpawnY, SpawnX] = 4;


        //primera habitacion
        int SpawnHabitacionAncho = Random.Range(MinTamañoDeHabitacion, MaxTamañoDeHabitacion);
        int SpawnHabitacionAlto = Random.Range(MinTamañoDeHabitacion, MaxTamañoDeHabitacion);

        for (int i = SpawnY - SpawnHabitacionAlto; i < SpawnY + SpawnHabitacionAlto + 1; i++)
        {
            for (int j = SpawnX - SpawnHabitacionAncho; j < SpawnX + SpawnHabitacionAncho + 1; j++)
            {
                if (Grilla[i, j] != 4)
                {
                    Grilla[i, j] = 0;
                }

            }
        }


        //generar mas habitaciones desde spawn

        int PorcentajeDeHabitacionDesdeSpawn = 25;

        bool SalidaCreada = false;

        do
        {
            for (int i = 1; i < 5; i++)
            {

                if (i <= 2)
                {
                    if (0 != i % 2)
                    {
                        int PosHabitacion = Random.Range(MinPosHabitacion, MaxPosHabitacion + 1);
                        int MargenHabitacion = Random.Range(-Margen, Margen + 1);

                        //generar habitacion para +Y
                        if (Random.Range(0, 101) < PorcentajeDeHabitacionDesdeSpawn)
                        {
                            if (GrillaChequearValorExistente(Grilla, SpawnY + PosHabitacion, SpawnX + MargenHabitacion, Borde))
                            {
                                if (Grilla[SpawnY + PosHabitacion, SpawnX + MargenHabitacion] == 1)
                                {
                                    Grilla[SpawnY + PosHabitacion, SpawnX + MargenHabitacion] = 2;

                                    Grilla = GrillaCrearPuente(Grilla, SpawnX, SpawnY, SpawnX + MargenHabitacion, SpawnY + PosHabitacion, Grosor);

                                    SalidaCreada = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        int PosHabitacion = Random.Range(MinPosHabitacion, MaxPosHabitacion + 1);
                        int MargenHabitacion = Random.Range(-Margen, Margen + 1);

                        //generar habitacion para +X
                        if (Random.Range(0, 101) < PorcentajeDeHabitacionDesdeSpawn)
                        {
                            if (GrillaChequearValorExistente(Grilla, SpawnY + MargenHabitacion, SpawnX + PosHabitacion, Borde))
                            {
                                if (Grilla[SpawnY + MargenHabitacion, SpawnX + PosHabitacion] == 1)
                                {
                                    Grilla[SpawnY + MargenHabitacion, SpawnX + PosHabitacion] = 2;

                                    Grilla = GrillaCrearPuente(Grilla, SpawnX, SpawnY, SpawnX + PosHabitacion, SpawnY + MargenHabitacion, Grosor);

                                    SalidaCreada = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (0 != i % 2)
                    {
                        int PosHabitacion = Random.Range(MinPosHabitacion, MaxPosHabitacion + 1);
                        int MargenHabitacion = Random.Range(-Margen, Margen + 1);

                        //generar habitacion para -Y
                        if (Random.Range(0, 101) < PorcentajeDeHabitacionDesdeSpawn)
                        {
                            if (GrillaChequearValorExistente(Grilla, SpawnY - PosHabitacion, SpawnX + MargenHabitacion, Borde))
                            {
                                if (Grilla[SpawnY - PosHabitacion, SpawnX + MargenHabitacion] == 1)
                                {
                                    Grilla[SpawnY - PosHabitacion, SpawnX + MargenHabitacion] = 2;

                                    Grilla = GrillaCrearPuente(Grilla, SpawnX, SpawnY, SpawnX + MargenHabitacion, SpawnY - PosHabitacion, Grosor);

                                    SalidaCreada = true;
                                }
                            }


                        }
                    }
                    else
                    {
                        int PosHabitacion = Random.Range(MinPosHabitacion, MaxPosHabitacion + 1);
                        int MargenHabitacion = Random.Range(-Margen, Margen + 1);

                        //generar habitacion para -X
                        if (Random.Range(0, 101) < PorcentajeDeHabitacionDesdeSpawn)
                        {
                            if (GrillaChequearValorExistente(Grilla, SpawnY + MargenHabitacion, SpawnX - PosHabitacion, Borde))
                            {
                                if (Grilla[SpawnY + MargenHabitacion, SpawnX - PosHabitacion] == 1)
                                {
                                    Grilla[SpawnY + MargenHabitacion, SpawnX - PosHabitacion] = 2;

                                    Grilla = GrillaCrearPuente(Grilla, SpawnX, SpawnY, SpawnX - PosHabitacion, SpawnY + MargenHabitacion, Grosor);

                                    SalidaCreada = true;
                                }
                            }


                        }
                    }
                }
            }

        } while (!SalidaCreada);


        //crear habitaciones en loop que chocan con spawn

        int PorcentajeDeHabitacion = 50;

        for (int cant = 0; cant < CantidadHabitaciones; cant++)
        {
            for (int i = 0; i < Grilla.GetLength(0); i++)
            {
                for (int j = 0; j < Grilla.GetLength(1); j++)
                {
                    if (Grilla[i, j] == 2)
                    {
                        Grilla[i, j] = 3;

                        int HabitacionAlto = Random.Range(MinTamañoDeHabitacion, MaxTamañoDeHabitacion);
                        int HabitacionAncho = Random.Range(MinTamañoDeHabitacion, MaxTamañoDeHabitacion);

                        Grilla = GrillaCrearHabitacionesVecinas(Grilla, MaxPosHabitacion, MinPosHabitacion, Margen, PorcentajeDeHabitacion, i, j, Borde, Grosor);

                        Grilla = GrillaCrearHabitacion(Grilla, i, j, HabitacionAncho, HabitacionAlto);
                    }
                }
            }

            bool NoCreoNada = true;

            for (int i = 0; i < Grilla.GetLength(0); i++)
            {
                for (int j = 0; j < Grilla.GetLength(1); j++)
                {
                    if (Grilla[i, j] == 2)
                    {
                        NoCreoNada = false;
                    }
                }
            }

            //Si no crea habitaciones recuerda las anteriores para repetir
            if (NoCreoNada)
            {
                for (int i = 0; i < Grilla.GetLength(0); i++)
                {
                    for (int j = 0; j < Grilla.GetLength(1); j++)
                    {
                        if (Grilla[i, j] == 3)
                        {
                            Grilla[i, j] = 2;
                        }

                        if (Grilla[i, j] == 5)
                        {
                            Grilla[i, j] = 3;
                        }

                        if (Grilla[i, j] == 6)
                        {
                            Grilla[i, j] = 5;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < Grilla.GetLength(0); i++)
                {
                    for (int j = 0; j < Grilla.GetLength(1); j++)
                    {
                        if (Grilla[i, j] == 5)
                        {
                            Grilla[i, j] = 6;
                        }

                        if (Grilla[i, j] == 3)
                        {
                            Grilla[i, j] = 5;
                        }
                    }
                }
            }
        }

        //una vez que termina limpiar todo lo no deseado
        for (int i = 0; i < Grilla.GetLength(0); i++)
        {
            for (int j = 0; j < Grilla.GetLength(1); j++)
            {
                if (Grilla[i, j] > 1 && Grilla[i, j] != 4 && Grilla[i, j] != 2)
                {
                    Grilla[i, j] = 0;
                    
                }

            }
        }

        return Grilla;
    }

    int[,] GrillaDarBordes(int[,] Grilla, int Borde, int TipoDeBorde)
    {
        for (int i = 0; i < Grilla.GetLength(0); i++)
        {
            for (int j = 0; j < Grilla.GetLength(1); j++)
            {
                if (i < Borde || i > Grilla.GetLength(0) - 1 - Borde)
                {
                    Grilla[i, j] = TipoDeBorde;
                }
                else if (j < Borde || j > Grilla.GetLength(1) - 1 - Borde)
                {
                    Grilla[i, j] = TipoDeBorde;
                }
            }
        }

        return Grilla;
    }

    void GrillaMuros(GameObject[] Muros, float TamañoDeBloque, int Largo, int Ancho)
    {
        for (int i = 0; i < 4; i++)
        {
            if (i >= 2)
            {
                if (0 == i % 2)
                {
                    Muros[i].transform.position = new Vector3(Largo * TamañoDeBloque + 0.2f, -0.09000015f, -166.1f);
                }
                else
                {
                    Muros[i].transform.position = new Vector3(166.1f, -0.09000015f, Ancho * -TamañoDeBloque - 0.2f);
                }
            }
        }
    }

    bool GrillaChequearValorExistente(int[,] Grilla, int Y, int X, int Borde)
    {
        if (Grilla.GetLength(0) - Borde > Y && Borde <= Y && Grilla.GetLength(1) - Borde > X && Borde <= X)
        {
            return true;
        }

        return false;
    }

    int[,] GrillaCrearHabitacion(int[,] Grilla, int PosY, int PosX, int HabitacionAncho, int HabitacionAlto)
    {
        for (int i = PosY - HabitacionAlto; i < PosY + HabitacionAlto + 1; i++)
        {
            for (int j = PosX - HabitacionAncho; j < PosX + HabitacionAncho + 1; j++)
            {
                if (Grilla[i, j] == 1)
                {
                    Grilla[i, j] = 0;
                }
            }
        }

        return Grilla;
    }

    int[,] PonerCarpasSpawn(int[,] Grilla, float CantidadCarpas)
    {
        for (int i = 0 + Borde; i < Grilla.GetLength(0) - 1 - Borde; i++)
        {
            for (int j = 0 + Borde; j < Grilla.GetLength(1) - Borde; j++)
            {
                if (Grilla[i, j] == 0 && Grilla[i - 1, j] == 1)
                {

                    bool EspacioIndicado = true;

                    if (Random.Range(0, 101) < CantidadCarpas)
                    {
                        for (int k = -4; k < 3; k++)
                        {
                            for (int l = 0; l < 4; l++)
                            {
                                if (k >= 0)
                                {
                                    if (Grilla[k + i, l + j] != 0)
                                    {
                                        EspacioIndicado = false;
                                    }
                                }
                                else
                                {
                                    if (Grilla[k + i, l + j] != 1)
                                    {
                                        EspacioIndicado = false;
                                    }
                                }
                            }
                        }


                        if (EspacioIndicado)
                        {
                            //una vez que se ejecuta limpia el resto de bloques que ocupa la carpa
                            for (int k = -4; k < 3; k++)
                            {
                                for (int l = 0; l < 4; l++)
                                {
                                    if (k > 0)
                                    {
                                        Grilla[k + i, l + j] = 0;
                                    }
                                    else
                                    {
                                        Grilla[k + i, l + j] = -1;
                                    }
                                }
                            }

                            Grilla[i, j] = 5;
                        }
                    }
                }
            }
        }

        return Grilla;
    }

    int[,] GrillaCrearHabitacionesVecinas(int[,] Grilla, int MaxPosHabitacion, int MinPosHabitacion, int Margen, int PorcentajeDeHabitacion, int posY, int posX, int Borde, float Grosor)
    {
        for (int i = 1; i < 5; i++)
        {

            if (i <= 2)
            {
                if (0 != i % 2)
                {
                    int PosHabitacion = Random.Range(MinPosHabitacion, MaxPosHabitacion + 1);
                    int MargenHabitacion = Random.Range(-Margen, Margen + 1);

                    //generar habitacion para +Y
                    if (Random.Range(0, 101) < PorcentajeDeHabitacion)
                    {
                        if (GrillaChequearValorExistente(Grilla, posY + PosHabitacion, posX + MargenHabitacion, Borde))
                        {
                            if (Grilla[posY + PosHabitacion, posX + MargenHabitacion] == 1)
                            {
                                Grilla[posY + PosHabitacion, posX + MargenHabitacion] = 2;

                                Grilla = GrillaCrearPuente(Grilla, posX, posY, posX + MargenHabitacion, posY + PosHabitacion, Grosor);
                            }
                        }
                    }
                }
                else
                {
                    int PosHabitacion = Random.Range(MinPosHabitacion, MaxPosHabitacion + 1);
                    int MargenHabitacion = Random.Range(-Margen, Margen + 1);

                    //generar habitacion para +X
                    if (Random.Range(0, 101) < PorcentajeDeHabitacion)
                    {
                        if (GrillaChequearValorExistente(Grilla, posY + MargenHabitacion, posX + PosHabitacion, Borde))
                        {
                            if (Grilla[posY + MargenHabitacion, posX + PosHabitacion] == 1)
                            {
                                Grilla[posY + MargenHabitacion, posX + PosHabitacion] = 2;

                                Grilla = GrillaCrearPuente(Grilla, posX, posY, posX + PosHabitacion, posY + MargenHabitacion, Grosor);
                            }
                        }
                    }
                }
            }
            else
            {
                if (0 != i % 2)
                {
                    int PosHabitacion = Random.Range(MinPosHabitacion, MaxPosHabitacion + 1);
                    int MargenHabitacion = Random.Range(-Margen, Margen + 1);

                    //generar habitacion para -Y
                    if (Random.Range(0, 101) < PorcentajeDeHabitacion)
                    {
                        if (GrillaChequearValorExistente(Grilla, posY - PosHabitacion, posX + MargenHabitacion, Borde))
                        {
                            if (Grilla[posY - PosHabitacion, posX + MargenHabitacion] == 1)
                            {
                                Grilla[posY - PosHabitacion, posX + MargenHabitacion] = 2;

                                Grilla = GrillaCrearPuente(Grilla, posX, posY, posX + MargenHabitacion, posY - PosHabitacion, Grosor);
                            }
                        }


                    }
                }
                else
                {
                    int PosHabitacion = Random.Range(MinPosHabitacion, MaxPosHabitacion + 1);
                    int MargenHabitacion = Random.Range(-Margen, Margen + 1);

                    //generar habitacion para -X
                    if (Random.Range(0, 101) < PorcentajeDeHabitacion)
                    {
                        if (GrillaChequearValorExistente(Grilla, posY + MargenHabitacion, posX - PosHabitacion, Borde))
                        {
                            if (Grilla[posY + MargenHabitacion, posX - PosHabitacion] == 1)
                            {
                                Grilla[posY + MargenHabitacion, posX - PosHabitacion] = 2;

                                Grilla = GrillaCrearPuente(Grilla, posX, posY, posX - PosHabitacion, posY + MargenHabitacion, Grosor);
                            }
                        }


                    }
                }
            }
        }

        return Grilla;
    }

    int[,] GrillaCambiarObjetosPorOtros(int[,] Grilla, int Obj1, int Obj2, float Porcentaje)
    {
        for (int i = 0; i < Grilla.GetLength(0); i++)
        {
            for (int j = 0; j < Grilla.GetLength(1); j++)
            {
                if (Grilla[i, j] == Obj1 && Random.Range(0, 101) < Porcentaje)
                {
                    Grilla[i, j] = Obj2;
                }
            }
        }
        return Grilla;
    }

    int[,] GrillaCrearPuente(int[,] Grilla, int X1, int Y1, int X2, int Y2, float Grosor)
    {
        int MinY = Y2;
        int MaxY = Y2;
        int MinX = Y2;
        int MaxX = Y2;

        if (X1 > X2)
        {
            MaxX = X1;
            MinX = X2;
        }
        else
        {
            MaxX = X2;
            MinX = X1;
        }

        if (Y1 > Y2)
        {
            MaxY = Y1;
            MinY = Y2;
        }
        else
        {
            MaxY = Y2;
            MinY = Y1;
        }

        int X, Y;

        //hacer que el puente tenga direcciones random
        if (Random.Range(0, 101) < 50)
        {
            X = X1;
            Y = Y2;
        }
        else
        {
            X = X2;
            Y = Y1;
        }

        //crear los puentes
        for (int i = 0; i < Grilla.GetLength(0); i++)
        {
            for (int j = 0; j < Grilla.GetLength(1); j++)
            {
                if (i > MinY - Grosor && i < MaxY + Grosor && j > X - Grosor && j < X + Grosor)
                {
                    if (Grilla[i, j] == 1)
                    {
                        Grilla[i, j] = 0;
                    }
                }

                if (j > MinX - Grosor && j < MaxX + -Grosor && i > Y - Grosor && i < Y + Grosor)
                {
                    if (Grilla[i, j] == 1)
                    {
                        Grilla[i, j] = 0;
                    }
                }
            }
        }


        return Grilla;
    }

    void PopDeObjetos(GameObject[,] Chunk, float DistanciaDeDibujado)
    {
        if (Player)
        {
            for (int i = 0; i < Chunk.GetLength(0); i++)
            {
                for (int j = 0; j < Chunk.GetLength(1); j++)
                {
                    Vector3 Distance = FocusCam.position - Chunk[i, j].transform.position;

                    if (Distance.magnitude > DistanciaDeDibujado)
                    {
                        Chunk[i, j].SetActive(false);
                    }
                    else
                    {
                        Chunk[i, j].SetActive(true);
                    }
                }
            }
        }
    }

    int[,] SuavizadoProps(int[,] Grilla)
    {

        int Uniones = 0;
        int Uniones2 = 0;

        //tapa 1 suavizar hacia afuera
        for (int i = 1; i < Grilla.GetLength(0) - 1; i++)
        {
            for (int j = 1; j < Grilla.GetLength(1) - 1; j++)
            {
                if (Grilla[i, j] == 1)
                {
                    if (Grilla[i + 1, j] == 0)
                    {
                        Uniones++;
                    }
                    if (Grilla[i - 1, j] == 0)
                    {
                        Uniones--;
                    }
                    if (Grilla[i, j + 1] == 0)
                    {
                        Uniones2++;
                    }
                    if (Grilla[i, j - 1] == 0)
                    {
                        Uniones2--;
                    }

                    if (Uniones != 0 && Uniones2 != 0)
                    {
                        Grilla[i, j] = 2;
                    }
                }

                Uniones = 0;
            }
        }

        Uniones2 = 0;
        Uniones = 0;

        //tapa 2 suavizar hacia adentro
        for (int i = 1; i < Grilla.GetLength(0) - 1; i++)
        {
            for (int j = 1; j < Grilla.GetLength(1) - 1; j++)
            {
                if (Grilla[i, j] == 0)
                {
                    if (Grilla[i + 1, j] == 1)
                    {
                        Uniones++;
                    }
                    if (Grilla[i - 1, j] == 1)
                    {
                        Uniones++;
                    }
                    if (Grilla[i, j + 1] == 1)
                    {
                        Uniones++;
                    }
                    if (Grilla[i, j - 1] == 1)
                    {
                        Uniones++;
                    }

                    if (Uniones >= 2)
                    {
                        Grilla[i, j] = 3;
                    }
                }

                Uniones = 0;
            }
        }

        //limpiar informacion sobrante
        for (int i = 0; i < Grilla.GetLength(0); i++)
        {
            for (int j = 0; j < Grilla.GetLength(1); j++)
            {
                if (Grilla[i, j] == 3)
                {
                    Grilla[i, j] = 1;
                }
                else if (Grilla[i, j] == 2)
                {
                    Grilla[i, j] = 0;
                }
            }
        }

        return Grilla;
    }

    int[,] PonerPiedrasGrandes(int[,] Grilla, float CantidadPiedrasGrandes)
    {
        for (int i = 1; i < Grilla.GetLength(0); i++)
        {
            for (int j = 0; j < Grilla.GetLength(1) - 1; j++)
            {
                if (Grilla[i, j] == 1)
                {

                    bool EspacioIndicado = true;

                    if (Random.Range(0, 101) < CantidadPiedrasGrandes)
                    {
                        for (int k = -1; k < 1; k++)
                        {
                            for (int l = 0; l < 2; l++)
                            {

                                if (Grilla[k + i, l + j] != 1)
                                {
                                    EspacioIndicado = false;
                                }
                            }
                        }


                        if (EspacioIndicado)
                        {
                            //una vez que se ejecuta limpia el resto de bloques que ocupa la carpa
                            for (int k = -1; k < 1; k++)
                            {
                                for (int l = 0; l < 2; l++)
                                {

                                    Grilla[k + i, l + j] = -1;

                                }
                            }

                            Grilla[i, j] = 6;
                        }
                    }
                }
            }
        }

        return Grilla;
    }

    int[,] PonerSpawnsEnemigos(int[,] Grilla, float ProbabilidadDeSpawns, int SpawnEnemy)
    {

        for (int i = 1; i < Grilla.GetLength(0)-1; i++)
        {
            for (int j = 1; j < Grilla.GetLength(1)-1; j++)
            {

                bool EspacioIndicado = true;

                if (Grilla[i,j] == 0 )
                {
                    for (int k = -1; k < 1; k++)
                    {
                        for (int l = -1; l < 1; l++)
                        {
                            if (Grilla[i+k, j+l] != 0)
                            {
                                EspacioIndicado = false;
                            }
                        }
                    }

                    //dar el spawn si cumple
                    if (EspacioIndicado == true && Random.Range(0f,101f)<ProbabilidadDeSpawns)
                    {
                        Grilla[i, j] = SpawnEnemy;
                    }


                }

            }
        }

        return Grilla;
    }

    int[,] PonerLLaveFinal(int[,] Grilla, int EspacioLlave, int Llave, int DistanciaDelPlayer, int Spawn)
    {

        //chequear y ubicar los ejes de habitacion finales

        List<Vector2> Ejes = new List<Vector2>();

        for (int i = 0; i < Grilla.GetLength(0); i++)
        {
            for (int j = 0; j < Grilla.GetLength(1); j++)
            {
                if (Grilla[i,j] == 2)
                {
                    Ejes.Add(new Vector2(i, j));
                }
            }
        }

        List<Vector2> VectorLlave = new List<Vector2>();
        //una vez teniendo los ejes se elije uno random
        if (Ejes.Count > 0)
        {
            for (int i = 0; i < 3; i++)
            {
                int index = Random.Range(0, Ejes.Count);
                VectorLlave.Add(Ejes[index]);
                Ejes.Remove(Ejes[index]);
            }

            CantidadLlaves = VectorLlave.Count;
            GameManager.instance.LlavesRestantes = CantidadLlaves;
        }
        else
        {
            Debug.LogWarning("NO SE CREO LA SALIDA, no se encontro un VHF");
        }

        //darle a la llave la posicion
        for (int i = 0; i < VectorLlave.Count; i++)
        {
            if (VectorLlave[i].x >= 0)
            {
                Grilla[(int)VectorLlave[i].x, (int)VectorLlave[i].y] = Llave;
            }
        }
        

        return Grilla;
    }
}
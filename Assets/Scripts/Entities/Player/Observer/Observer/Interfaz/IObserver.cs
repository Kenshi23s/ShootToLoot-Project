using UnityEngine;

public interface IObserver
{
    
    //se pueden crear multiples "Notify" con diferentes sobrecarga de operadores para usar en distintas ocasiones

    /// <summary>
    /// Se usa para notificar la vida actual del player, el daño que le fue causado y donde le ocurrio  al player 
    /// </summary>
    /// <param name="ActualLife"></param>
    /// <param name="DamageTaken"></param>
    void Notify(int ActualLife,int DamageTaken,Vector3 Where,AudioClip clip);

    /// <summary>
    /// se usa para notificar algo, no requier nada por parametro
    /// </summary>
    void Notify();
}

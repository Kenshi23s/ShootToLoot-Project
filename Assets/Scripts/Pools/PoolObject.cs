using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PoolObject<T>
{
    public PoolObject() { }

    [SerializeField]public Stack<T> pool = new Stack<T>();

    Action<T> turnOn;
    Action<T> turnOff;
    Func<T> build;

    int prewarm = 0;

    // al crearse una "PoolObject" se le deberan pasar estas referencias para que funcione correctamente
    public void Intialize(Action<T> _turnOn, Action<T> _turnOff, Func<T> _build, int prewarm = 5)
    {
        this.turnOn = _turnOn;           
        this.turnOff = _turnOff;
        this.build = _build;
        this.prewarm = prewarm;

        AddMore();
    }
    // obtiene el objeto de la lista, si no puede obtener ninguno instancia mas
    public T Get()
    {
        if(pool.Count <= 0) AddMore();
        var obj = pool.Pop();
        turnOn(obj);
        return obj;
    }
    // devuelve el objeto a la lista y llama a su metodo de apagado
    public void Return(T obj)
    {
        pool.Push(obj);
        turnOff(obj);
    }
    // añade mas objetos a la lista
    void AddMore()
    {
        for (int i = 0; i < prewarm; i++)
        {
            var obj = build.Invoke();
            pool.Push(obj);
            turnOff(obj);
        }
    }

}

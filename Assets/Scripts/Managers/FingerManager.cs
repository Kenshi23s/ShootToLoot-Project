using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FingerManager
{

    public FingerManager(List<int> fingersBeingUsed)
    {
        this.fingersBeingUsed = fingersBeingUsed;
    }

    public List<int> fingersBeingUsed = new List<int>();

    public void AddFingerToList(int _idFinger)
    {
        if (!fingersBeingUsed.Contains(_idFinger))
        {
            fingersBeingUsed.Add(_idFinger);
        }

    }
    public void RemoveFingerList(int _idFinger)
    {
        if (fingersBeingUsed.Contains(_idFinger))
        {
            fingersBeingUsed.Remove(_idFinger);
        }
    }

    public bool CheckFingerUsed(out Touch t)
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch ActualTouch = Input.GetTouch(i);

            if (!fingersBeingUsed.Contains(ActualTouch.fingerId))
            {
                t = ActualTouch;
                return true;
            }

        }
        t = default;
        return false;

    }
}

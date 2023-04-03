using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DragJoystick : MonoBehaviour
{
    public Vector3 ValueStick = Vector3.zero;
    Vector3 _OriginalPos;
    public float distanceStick = 200;
    int _idfinger;
   

    void Start()
    {
        _OriginalPos = transform.position;
        //print(_OriginalPos);
       
    }

    public void DownDrag()
    {
        Touch actualTouch;
        // checkea si el dedo en pantalla esta sin uso
        if (GameManager.instance.CheckFingerUsed(out actualTouch))
        {
            _idfinger = actualTouch.fingerId;
            //si lo esta, lo añade a una lista de dedos
            GameManager.instance.AddFingerToList(_idfinger);
        }    

    }

    public void OnDrag()
    {
        Touch touch;
        if (GetMyTouchId(_idfinger, out touch))
        {
            MoveJoystick(touch.position);
        }
      

        //print("stick1:" + ValueStick);

    }
    // se le pasa la posicion actual del dedo para ejecutar el movimiento/rotacion del personaje
    void MoveJoystick(Vector3 currentPos)
    {
        //Vector3 CurrentPos = ActualTouch.position;
        ValueStick = currentPos - _OriginalPos;
        print("OnDrag");
        if (distanceStick > 0)
        {
            ValueStick = Vector3.ClampMagnitude(ValueStick, distanceStick);
        }

        transform.position = ValueStick + _OriginalPos;
    }
    /// <summary>
    /// revisa si el dedo que esta en pantalla coincide con el que me habia guardado de antes
    /// </summary>
    /// <param name="fingerID"></param>
    /// <param name="touch"></param>
    /// <returns></returns>
    bool GetMyTouchId(int fingerID, out Touch touch)
    {
        //por cada dedo que tengo en pantalla
        for (int i = 0; i < Input.touchCount; i++)
        {
            //revisar si el id I == a mi id guardado 
            if (Input.touches[i].fingerId == fingerID)
            {
                //si es, lo devuelvo
                touch = Input.touches[i];
                return true;
            }
        }
        //sino devuelvo default
        touch = default;
        return false;
    }
    public void UpDrag()
    {
       
        ValueStick = Vector3.zero;
        transform.position = _OriginalPos;
        GameManager.instance.RemoveFingerList(_idfinger); 

    }
    
}

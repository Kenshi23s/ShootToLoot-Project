using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

namespace FacundoColomboMethods
{
    public static class ColomboMethods
    {
        //metodos utiles que puede ayudar a la hora de desarrollar el juego

        public static T[] GetChildrenComponents<T>(Transform Father)
        {
            T[] Components = new T[Father.childCount];
            for (int i = 0; i < Father.childCount; i++)
            {
                var item = Father.transform.GetChild(i).GetComponent<T>();
                if (item != null)
                {
                    Components[i] = item;
                }
            }
            return Components;
        }

        public static List<T> GetChildrenComponentsList<T>(Transform Father)
        {
            List<T> Components = new List<T>();
            for (int i = 0; i < Father.childCount; i++)
            {
                var item = Father.transform.GetChild(i).GetComponent<T>();
                if (item != null)
                {
                    Components.Add(item);

                }
            }
            Components.RemoveAt(0);

            return Components;
        }

        public static List<T> CheckNearby<T>(Vector3 pos, float radius)
        {
            List<T> list = new List<T>();
            Collider[] colliders = Physics.OverlapSphere(pos, radius);

            foreach (Collider Object in colliders)
            {
                var item = Object.GetComponent<T>();
                if (item != null)
                {
                    list.Add(item);
                }
            }

            return list;
        }

        public static Vector3 CheckForwardRayColision(Vector3 pos, Vector3 dir, float range = 15)
        {
            //aca se guardan los datos de con lo que impacte el rayo
            RaycastHit hit;

            //si el rayo choco contra algo
            if (Physics.Raycast(pos, dir, out hit, range))
            {
                return hit.point;

            }

            return new Vector3(pos.x, pos.y, pos.z + range);

        }

        public static T CheckNearest<T>(T[] objPosition, Vector3 myPos) where T : MonoBehaviour
        {
            int nearestIndex = 0;

            float nearestMagnitude = (objPosition[0].transform.position - myPos).magnitude;

            for (int i = 1; i < objPosition.Length; i++)
            {
                float tempMagnitude = (objPosition[i].transform.position - myPos).magnitude;

                if (nearestMagnitude > tempMagnitude)
                {
                    nearestMagnitude = tempMagnitude;
                    nearestIndex = i;
                }

            }

            return objPosition[nearestIndex];
        }
        public static T CheckNearest<T>(Transform[] objPosition, Vector3 myPos)
        {
            int nearestIndex = 0;

            float nearestMagnitude = (objPosition[0].transform.position - myPos).magnitude;

            for (int i = 1; i < objPosition.Length; i++)
            {
                float tempMagnitude = (objPosition[i].transform.position - myPos).magnitude;

                if (nearestMagnitude > tempMagnitude)
                {
                    nearestMagnitude = tempMagnitude;
                    nearestIndex = i;
                }

            }

            return objPosition[nearestIndex].GetComponent<T>();
        }

        public static T[] WhichAreOnSight<T>(T[] items, Vector3 pos) where T : MonoBehaviour
        {
            List<T> list = new List<T>();
            foreach (T item in items)
            {              
                Vector3 dir = item.transform.position - pos;
                RaycastHit hit;
                if (!Physics.Raycast(pos,dir,out hit, dir.magnitude))
                {
                    list.Add(item);
                }
                else
                {
                    int HitObject = hit.transform.gameObject.GetHashCode();

                    if (HitObject == item.GetHashCode())
                    {
                       list.Add(item); 
                    }
                }
            }
            return list.ToArray();
        }

        public static T IsOnSight<T>(T item, Vector3 pos) where T : MonoBehaviour
        {
           RaycastHit hit;
           Vector3 dir = item.transform.position - pos;
           if (!Physics.Raycast(pos, dir, out hit, dir.magnitude))
           {
              return item;
           }
           else
           {
               int HitObject= hit.transform.gameObject.GetHashCode();
               if (HitObject == item.GetHashCode())
               {
                    return item;
               }
           }
            return null;
          
          
        }

    }
    
}

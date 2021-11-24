using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension
{

    //Transform¢¯¢® ¢¥??? ?¢ç??¢¬¨­¨ù¡©??
    #region 
    public static GameObject GetRootObject(this Transform current)
    {
        GameObject result = current.gameObject;

        while (result.transform.parent)
        {
            result = result.transform.parent.gameObject;
        }

        return result;
    }

    public static List<GameObject> GetChildsForList(this Transform current)
    {
        List<GameObject> result = new List<GameObject>();

        for (int i = 0; i < current.childCount; i++)
        {
            result.Add(current.GetChild(i).gameObject);
        }

        return result;

    }
    #endregion
}
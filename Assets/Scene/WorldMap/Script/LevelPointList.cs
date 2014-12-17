using UnityEngine;
using System.Collections;

public class LevelPointList : MonoBehaviour {

    public LevelPoint GetLevelPoint(string pLevelId)
    {
        if (string.IsNullOrEmpty(pLevelId)) return null;

        LevelPoint[] points = GetComponentsInChildren<LevelPoint>();
        foreach(LevelPoint point in points)
        {
            if(point.Id.Equals(pLevelId))
            {
                return point;
            }
        }

        return null;
    }

    public Transform GetLevelPointTransform(string pLevelId)
    {
        if (string.IsNullOrEmpty(pLevelId)) return null;

        for(int i = 0; i < transform.childCount; i++)
        {
            Transform pointTransform = transform.GetChild(i);
            LevelPoint point = pointTransform.GetComponent<LevelPoint>();

            if(point != null && point.Id.Equals(pLevelId))
            {
                return pointTransform;
            }
        }

        return null;
    }
}

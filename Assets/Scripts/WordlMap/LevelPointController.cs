using Radix.Error;
using UnityEngine;

public class LevelPointController : MonoBehaviour {

    public LevelPoint GetLevelPoint(int pLevelId)
    {
        Assert.Check(pLevelId.IsBetweenInclusively(0, 10));

        LevelPoint[] points = GetComponentsInChildren<LevelPoint>();
        foreach (LevelPoint point in points)
        {
            if (point.ID.Equals(pLevelId))
            {
                return point;
            }
        }
        return null;
    }

    public Transform GetLevelPointTransform(int pLevelId)
    {
        return GetLevelPoint(pLevelId).transform;
    }
}

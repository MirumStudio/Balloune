using UnityEngine;
using System.Collections;

public class LevelPoint : MonoBehaviour {
    [SerializeField]
    private string m_Id = string.Empty;
    public string Id { get { return m_Id; } }

    [SerializeField]
    private string m_LevelName = string.Empty;
    public string Name { get { return m_LevelName; } }

    [SerializeField]
    private string m_RightLevelPoint = string.Empty;
    public string RightLevelPoint { get { return m_RightLevelPoint; } }

    [SerializeField]
    private string m_LeftLevelPoint = string.Empty;
    public string LeftLevelPoint { get { return m_LeftLevelPoint; } }

    [SerializeField]
    private string m_UpLevelPoint = string.Empty;
    public string UpLevelPoint { get { return m_UpLevelPoint; } }

    [SerializeField]
    private string m_BottomLevelPoint = string.Empty;
    public string BottomLevelPoint { get { return m_BottomLevelPoint; } }
}

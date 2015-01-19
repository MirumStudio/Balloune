using UnityEngine;
using System.Collections;

public class WorldMapView : MonoBehaviour {

    [SerializeField]
    private Canvas m_LevelUI = null;

    [SerializeField]
    private GameObject m_Character = null;
    private WorldMapCharacterController m_CharacterController;

    [SerializeField]
    private GameObject m_LevelPoint = null;

	void Start () {
        m_CharacterController = m_Character.GetComponent<WorldMapCharacterController>();
        m_CharacterController.OnLevelChanged += OnLevelChanged;
        m_CharacterController.OnLevelExited += OnLevelExited;
        OnLevelChanged(m_CharacterController.GetCurrentLevelPoint().Id);
	}

    private void OnLevelExited(object pSender, object pParam)
    {
        m_LevelUI.GetComponent<LevelInfoUI>().FadeOut();
    }

    private void OnLevelChanged(string pLevelId)
    {
        m_LevelUI.GetComponent<LevelInfoUI>().ChangeLevel(m_CharacterController.GetCurrentLevelPoint());
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelDot : MonoBehaviour {

    private Image mImage;
    private Image mLock;
    private UILevelInfo mUILevelInfo;

    private void Start()
    {
        mImage = GetComponent<Image>();
        mLock = GetComponentsInChildren<Image>()[1];
 //       mLock.color = Color.green;
//        mLock.sprite = Resources.Load("Image/UI/UnlockLevel.png");
    }

	public void SetLevelInfo(UILevelInfo pInfo)
    {
        mUILevelInfo = pInfo;
        if (!mUILevelInfo.IsUnlocked)
        {
            mImage.color = Color.gray;
        }
    }

    public void Select()
    {
    {
        mImage.color = Color.red;
    }
    }

    public void Unselect()
    {
        if (!mUILevelInfo.IsUnlocked)
        {
            mImage.color = Color.gray;
        } else
        {
            mImage.color = Color.white;
        }
    }

    public void OnClick()
    {

    }
}

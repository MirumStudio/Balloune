using UnityEngine;
using System.Collections;

public class WorldMapCharacterController : MonoBehaviour {

    [SerializeField]
    private GameObject m_LevelPoint;
    private LevelPointList mLevelPointList;

    private int currentPointIndex = 0;
    private bool mIsMoving = false;
    private Vector3 mDestination;

	// Use this for initialization
	void Start () {
        mLevelPointList = m_LevelPoint.GetComponent<LevelPointList>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!mIsMoving)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                mDestination = mLevelPointList.m_Test[1].transform.position;
                mDestination.y += 0.35f;
                mDestination.x -= 0.05f;
                mIsMoving = true;
                
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                mDestination = mLevelPointList.m_Test[0].transform.position;
                mDestination.y += 0.35f;
                mDestination.x -= 0.05f;
                mIsMoving = true;
            }
        }
        else
        {
            var step = 2 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, mDestination, step);
            if(mDestination.Equals(transform.position))
            {
                mIsMoving = false;
            }
        }
	}
}

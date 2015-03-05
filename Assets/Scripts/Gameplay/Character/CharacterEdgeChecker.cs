using UnityEngine;
using System.Collections;
using Radix.Error;

/*In this class, there are a lot of small ajustement and magic number.
TODO: Refactor this class when we will have the real Asset*/

[RequireComponent(typeof(Collider2D))]
public class CharacterEdgeChecker : MonoBehaviour {
    private const string GROUND_LAYER_NAME = "Ground"; 

    private float mWidth;
    private float mHeight;

    Collider2D mCollider;

	void Start () {
        mCollider = GetComponent<Collider2D>();
        mWidth = mCollider.bounds.size.x;
        mHeight = mCollider.bounds.size.y;
	}

	void Update (){
		DrawDebugLine(); //For testing
	}

    private void DrawDebugLine()
    {
        Debug.DrawLine(GetTopRightCorner(), GetTopLeftCorner(), Color.red);
        Debug.DrawLine(GetTopLeftCorner(), GetBottomLeftCorner(), Color.green);
        Debug.DrawLine(GetTopRightCorner(), GetBottomRightCorner(), Color.blue);
        Debug.DrawLine(GetBottomLeftCorner(), GetBottomRightCorner(), Color.yellow);
    }

    public bool TouchSomething(EEdge edge)
    {
        if(edge == EEdge.RIGHT)
        {
            Vector2 vector = GetBottomRightCorner();
            vector.y += 0.05f;
            Debug.DrawLine(GetTopRightCorner(), vector, Color.blue);
            return Physics2D.Linecast(GetTopRightCorner(), vector, GroundLayerMask);
        }
        else if(edge == EEdge.LEFT)
        {
            Vector2 vector = GetBottomLeftCorner();
            vector.y += 0.05f;
            Debug.DrawLine(GetTopLeftCorner(), vector, Color.green);
            return Physics2D.Linecast(GetTopLeftCorner(), vector, GroundLayerMask);
        }
        else if(edge == EEdge.TOP)
        {
            return Physics2D.Linecast(GetTopRightCorner(), GetTopLeftCorner(), GroundLayerMask);
        }
        else if(edge == EEdge.BOTTOM)
        {
            Vector2 right = GetBottomRightCorner();
            right.x -= 0.1f;
            Vector2 left = GetBottomLeftCorner();
            left.x += 0.1f;
            return Physics2D.Linecast(left, right, GroundLayerMask);
        }
        else
        {
            Error.Create("Edge to check is undefined...", EErrorSeverity.MINOR);
            return false;
        }
    }

    private Vector2 GetTopRightCorner()
    {
        return new Vector2(GetRightEdge(), GetTopEdge());
    }

    private Vector2 GetTopLeftCorner()
    {
        return new Vector2(GetLeftEdge(), GetTopEdge());
    }

    private Vector2 GetBottomRightCorner()
    {
        return new Vector2(GetRightEdge(), GetBottomEdge());
    }

    private Vector2 GetBottomLeftCorner()
    {
        return new Vector2(GetLeftEdge(), GetBottomEdge());
    }

    private float GetTopEdge()
    {
        return GetCenter().y + mHeight / 2;
    }

    private float GetBottomEdge()
    {
        return GetCenter().y - mHeight / 2 - 0.05f;
    }

    private float GetLeftEdge()
    {
        return GetCenter().x - mWidth / 2 - 0.1f;
    }

    private float GetRightEdge()
    {
        return GetCenter().x + mWidth / 2 + 0.1f;
    }

    private Vector2 GetCenter()
    {
        return mCollider.bounds.center;
    }

    private int GroundLayerMask
    {
        get
        {
            return 1 << LayerMask.NameToLayer(GROUND_LAYER_NAME);
        }
    }
}

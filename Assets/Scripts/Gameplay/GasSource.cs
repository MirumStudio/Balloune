using UnityEngine;
using System.Collections;
using Radix.Event;

public class GasSource : MonoBehaviour {

	[SerializeField]
	private EBalloonType m_GasType = EBalloonType.TOXIC;

    [SerializeField]
    private float m_MinimumDistance = 4.0f;

    Vector2 prev_vec = new Vector2();
    Vector2 curr_vec = new Vector2();
    float total_angle = 0f;
    float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (Input.touchCount > 0 && timer > 0.1)
        {
            timer = 0;
            VerivyCircle(Camera.main.ScreenToWorldPoint(Input.touches[0].position));
        }

        if (total_angle != 0 && Input.touchCount == 0)
        {
            if(total_angle > 330 || total_angle < -330)
            {
				EventService.DipatchEvent(EGameEvent.INFLATE_BALLOON, m_GasType);
            }


            total_angle = 0;
            prev_vec = new Vector2();
            curr_vec = new Vector2();
        }
    }

    void VerivyCircle(Vector2 pos)
    {
        prev_vec = new Vector2(curr_vec.x, curr_vec.y);
        curr_vec = new Vector2(pos.x - transform.position.x, pos.y - transform.position.y);

        if (curr_vec.magnitude <= m_MinimumDistance)
        {

            var constants_value = Mathf.Min(1, Vector2.Dot(prev_vec, curr_vec) / (prev_vec.magnitude * curr_vec.magnitude));

            if (!float.IsNaN(constants_value))
            {
                var delta_angle = Mathf.Acos(constants_value);

                var direction = CrossProduct(prev_vec, curr_vec) > 0 ? 1 : -1;

                total_angle += (Mathf.Rad2Deg * delta_angle) * direction;
            }
        }
    }

    float CrossProduct(Vector2 v1, Vector2 v2)
    {
        return (v1.x * v2.y) - (v1.y * v2.x);
    }
}

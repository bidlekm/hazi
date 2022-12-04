using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypontFollow : MonoBehaviour
{
    public GameObject[] m_waypoints;
    private int m_direction;
    public float m_movingSpeed;
    // Start is called before the first frame update
    void Start()
    {
        m_direction = 0;
        m_movingSpeed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(m_waypoints[m_direction].transform.position, transform.position) < 0.1f)
        {
            m_direction = (++m_direction) % m_waypoints.Length;
        }
        transform.position = Vector2.MoveTowards(transform.position, m_waypoints[m_direction].transform.position, m_movingSpeed * Time.deltaTime);

    }
}

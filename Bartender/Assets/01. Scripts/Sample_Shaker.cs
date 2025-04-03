using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_Shaker : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_Shakers;
    [SerializeField]
    private float m_Max;
    [SerializeField]
    private float m_Min;

    private void Start()
    {
        m_Max += gameObject.transform.position.y;
        m_Min += gameObject.transform.position.y;
    }

    private void Update()
    {
        //foreach (var shaker in m_Shakers)
        //{
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 현재 오브젝트를 현위치에 고정합니다.
/// </summary>
class FixedWorldPosition : MonoBehaviour
{
    [SerializeField] Vector3 position;

    private void Start()
    {
        SetPosition(transform.position);
    }

    private void LateUpdate()
    {
        transform.position = position;
    }

    public void SetPosition(Vector3 position)
    {
        this.position = position;
    }
}

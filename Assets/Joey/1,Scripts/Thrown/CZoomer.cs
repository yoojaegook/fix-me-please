using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CZoomer : MonoBehaviour
{
    public AnimationCurve   ac;
    public GameObject       ZoomObj;
    public Transform        TargetObj;
    public CObjMover        MovObj;

    public float        _defaultYPosition;
    public float        _ZoomSizePerDist;
    private float       _totalDistToTarget;
    public bool         _isDefaultSeted = false;

    
    private void Update()
    {
        if (MovObj._isMove)
        {
            _isDefaultSeted = false;
            float currentY = (ZoomObj.transform.position.y-_defaultYPosition)/_totalDistToTarget;
            ZoomObj.transform.localScale = new Vector3(ac.Evaluate(currentY)*0.7f+0.3f, ac.Evaluate(currentY) * 0.7f+0.3f, 1);
        }else
        {
            if(!_isDefaultSeted)
            {
                SetDefault();
            }
        }
    }

    void SetDefault()
    {
        ZoomObj.transform.localScale    = new Vector3(1,1,1);
        _defaultYPosition               = ZoomObj.transform.position.y;
        _totalDistToTarget              = CalDisOfY();
        //_ZoomSizePerDist                = 0.7f/_totalDistToTarget;
        _isDefaultSeted                 = true;
    }

    float CalDisOfY()
    {
        return TargetObj.position.y - ZoomObj.transform.position.y;
    }
}

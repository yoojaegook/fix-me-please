using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CObjMover : MonoBehaviour
{
    public Transform StartPosition;


    [Range(0, 5f)]
    public float _movSpeed = 1f;
    [Range(0, 5f)]
    public float _velocity = 2f;
    [Range(-5, 5f)]
    public float _defaultSidePow = 2f;
    [Range(-5f, 5f)]
    public float _defaultaSpin = 2f;
    private float _sunpunggi = 1f;
    private float _magnet = 1f;

    public bool _isMove = false;
    private float _sideMovePower;
    private float _aSpin = 2f;

    // Start is called before the first frame update
    void Start()
    {
        float r = Mathf.Atan2(1, 0) * Mathf.Rad2Deg;
        Debug.Log(r);
        DefaultSet();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isMove)
        {
            MoveStraighteFunc();
            MoveSideFunc();
            SpinMoveFunc();
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            DefaultSet();
            _isMove = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            DefaultSet();
        }
    }

    #region Func
    public void DefaultSet()
    {
        _isMove = false;
        transform.position = StartPosition.position;
        _sideMovePower = _defaultSidePow;
        _aSpin = _defaultaSpin;
    }
    public void MoveStraighteFunc()
    {
        transform.Translate(0, _velocity * Time.fixedDeltaTime * _movSpeed, 0);
    }
    public void MoveSideFunc()
    {
        transform.Translate(_sideMovePower * Time.fixedDeltaTime * _movSpeed, 0, 0);
    }
    public void SpinMoveFunc()
    {
        _sideMovePower += (_aSpin * Time.fixedDeltaTime * _movSpeed);
    }
    public void MagnetMoveFunc()
    {
        transform.position += new Vector3(-_magnet * Time.fixedDeltaTime * _movSpeed, 0, 0);
    }
    public void SunpunggiMoveFunc()
    {
        _aSpin += (_sunpunggi * _movSpeed);
    }

    #endregion

    #region Coll
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TriggerEnter" + collision.tag);
        switch (collision.tag)
        {
            case "Sunpunggi":
                _sunpunggi = collision.gameObject.GetComponent<CCollSizeController>().GetPower();
                SunpunggiMoveFunc();
                break;
            case "Magnet":
                _magnet = collision.gameObject.GetComponent<CCollSizeController>().GetPower();
                break;
            default:
                break;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("TriggerStay" + collision.tag);
        switch (collision.tag)
        {
            case "Sunpunggi":
                break;
            case "Magnet":
                MagnetMoveFunc();
                break;
            default:
                break;
        }
    }

    #endregion
}
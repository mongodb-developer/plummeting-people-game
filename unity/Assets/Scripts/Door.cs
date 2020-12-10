using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private GameObject _doorTopGameObject;
    private GameObject _doorBottomGameObject;
    private Rigidbody2D _doorTopRbd;
    private Rigidbody2D _doorBottomRbd;

    public bool isFakeDoor;

    // Start is called before the first frame update
    void Awake()
    {
        _doorBottomGameObject = transform.Find("Bottom").gameObject;
        _doorTopGameObject = transform.Find("Top").gameObject;
        _doorBottomRbd = _doorBottomGameObject.GetComponent<Rigidbody2D>();
        _doorTopRbd = _doorTopGameObject.GetComponent<Rigidbody2D>();
        if(isFakeDoor) {
            _doorBottomRbd.bodyType = RigidbodyType2D.Static;
            _doorTopRbd.bodyType = RigidbodyType2D.Static;
        } 
        else 
        {
            _doorBottomRbd.bodyType = RigidbodyType2D.Dynamic;
            _doorTopRbd.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    public void SetIsFakeDoor(bool isFakeDoor) {
        if(isFakeDoor) {
            _doorBottomRbd.bodyType = RigidbodyType2D.Static;
            _doorTopRbd.bodyType = RigidbodyType2D.Static;
        } 
        else 
        {
            _doorBottomRbd.bodyType = RigidbodyType2D.Dynamic;
            _doorTopRbd.bodyType = RigidbodyType2D.Dynamic;
        }
    }

}

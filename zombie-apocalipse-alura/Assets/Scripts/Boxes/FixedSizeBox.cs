using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedSizeBox : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _quantity;

    private Stack<GameObject> _objectBox;
    private void Awake()
    {
        this._objectBox = new Stack<GameObject>();
        this.CreateAllObjects();
    }

    private void CreateAllObjects()
    {
        for (int i = 0; i < this._quantity; i++)
        {
            this.CreateObject();
        }
    }

    private void CreateObject()
    {
        GameObject obj = GameObject.Instantiate(this._prefab, this.transform);
		var control = obj.GetComponent<Zombie>();
		control.SetBox(this);
        this.ReturnObject(obj);
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        this._objectBox.Push(obj);
    }

    public GameObject GetObject()
    {
        GameObject obj = this._objectBox.Pop();
        obj.SetActive(true);
        return obj;
    }
    public bool StackIsNotEmpity()
    {
        return this._objectBox.Count > 0;
    }
}

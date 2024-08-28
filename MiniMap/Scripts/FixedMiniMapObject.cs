using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixedMiniMapObject : MonoBehaviour
{
    public int FixedMiniMapId = 1;
    [SerializeField] Sprite MyShape;
    [SerializeField] Vector2 size = new Vector2(20, 20);
    public GameObject Prefab;
    public bool Rotatable = true;
    GameObject myMark;
    Transform parent;
    Vector2 areafrom,areato;

    public GameObject MyMark
    {
        get => myMark;
    }
    void Start()
    {
        parent = FixedMiniMap.Ins[FixedMiniMapId].gameObject.transform.GetChild(1);
        myMark = Instantiate(Prefab, parent);
        myMark.GetComponent<Image>().sprite = MyShape;
        myMark.GetComponent<RectTransform>().sizeDelta = size;
    }
    void Update()
    {
        if (FixedMiniMap.Ins[FixedMiniMapId].MapShape==MapShape.Rectangular) 
        {
            areafrom = FixedMiniMap.Ins[FixedMiniMapId].RectAreaFrom;
            areato = FixedMiniMap.Ins[FixedMiniMapId].RectAreaTo;
            if (transform.position.x > areato.x || transform.position.x < areafrom.x || transform.position.z > areato.y || transform.position.z < areafrom.y)
            {
                Debug.LogWarning("Out Of Rectangular Map");
                float percentx = (transform.position.x - areafrom.x) / (areato.x - areafrom.x);
                float percenty = (transform.position.z - areafrom.y) / (areato.y - areafrom.y);
                myMark.transform.localPosition = new Vector3(percentx * FixedMiniMap.Ins[FixedMiniMapId].gameObject.GetComponent<RectTransform>().sizeDelta.x, percenty * FixedMiniMap.Ins[FixedMiniMapId].gameObject.GetComponent<RectTransform>().sizeDelta.y, 0);
            }
            else
            {
                float percentx = (transform.position.x - areafrom.x) / (areato.x - areafrom.x);
                float percenty = (transform.position.z - areafrom.y) / (areato.y - areafrom.y);
                myMark.transform.localPosition = new Vector3(percentx * FixedMiniMap.Ins[FixedMiniMapId].gameObject.GetComponent<RectTransform>().sizeDelta.x, percenty * FixedMiniMap.Ins[FixedMiniMapId].gameObject.GetComponent<RectTransform>().sizeDelta.y, 0);
            }
        }
        else
        {
            float distance = new Vector2(transform.position.x - FixedMiniMap.Ins[FixedMiniMapId].CircularCenter.x, transform.position.z - FixedMiniMap.Ins[FixedMiniMapId].CircularCenter.y).magnitude;
            areafrom = new Vector2(FixedMiniMap.Ins[FixedMiniMapId].CircularCenter.x- FixedMiniMap.Ins[FixedMiniMapId].CircularRadius, FixedMiniMap.Ins[FixedMiniMapId].CircularCenter.y - FixedMiniMap.Ins[FixedMiniMapId].CircularRadius);
            areato = new Vector2(FixedMiniMap.Ins[FixedMiniMapId].CircularCenter.x + FixedMiniMap.Ins[FixedMiniMapId].CircularRadius, FixedMiniMap.Ins[FixedMiniMapId].CircularCenter.y + FixedMiniMap.Ins[FixedMiniMapId].CircularRadius);
            if (distance> FixedMiniMap.Ins[FixedMiniMapId].CircularRadius)
            {
                Debug.LogWarning("Out Of Circular Map");
                float percentx = (transform.position.x - areafrom.x) / (areato.x - areafrom.x);
                float percenty = (transform.position.z - areafrom.y) / (areato.y - areafrom.y);
                myMark.transform.localPosition = new Vector3(percentx * FixedMiniMap.Ins[FixedMiniMapId].gameObject.GetComponent<RectTransform>().sizeDelta.x, percenty * FixedMiniMap.Ins[FixedMiniMapId].gameObject.GetComponent<RectTransform>().sizeDelta.y, 0);
            }
            else
            {
                float percentx = (transform.position.x - areafrom.x) / (areato.x - areafrom.x);
                float percenty = (transform.position.z - areafrom.y) / (areato.y - areafrom.y);
                myMark.transform.localPosition = new Vector3(percentx * FixedMiniMap.Ins[FixedMiniMapId].gameObject.GetComponent<RectTransform>().sizeDelta.x, percenty * FixedMiniMap.Ins[FixedMiniMapId].gameObject.GetComponent<RectTransform>().sizeDelta.y, 0);
            }
        }
        if(Rotatable)MyMark.transform.rotation = Quaternion.Euler(0, 0, -transform.rotation.eulerAngles.y);
    }
    private void OnDestroy()
    {
        Destroy(myMark);
    }
}

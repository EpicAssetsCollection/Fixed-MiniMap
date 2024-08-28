using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum MapShape
{
    Rectangular,
    Circular
}
public class FixedMiniMap : MonoBehaviour
{
    public static Dictionary<int, FixedMiniMap> Ins = new Dictionary<int, FixedMiniMap>();
    [SerializeField] int id = 1;
    [SerializeField] bool ChangeSpriteOnAwake = true;
    public Sprite Map;
    public MapShape MapShape;
    public Vector2 RectAreaFrom = new Vector2(0,0);
    public Vector2 RectAreaTo = new Vector2(1000,1000);
    public Vector2 CircularCenter = new Vector2(500, 500);
    public float CircularRadius = 500;

    public int Id
    {
        get => id;
    }
    // Start is called before the first frame update
    void Awake()
    {
        Ins.Add(id, this);
        if (Map != null)
        {
            transform.GetChild(0).GetComponent<Image>().sprite = Map;
        }
    }
    private void Update()
    {
        transform.GetChild(1).localPosition = new Vector3(-GetComponent<RectTransform>().sizeDelta.x/2, -GetComponent<RectTransform>().sizeDelta.y / 2, 0);
    }
    public void ChangeSize(Vector2 NewSize)
    {
        GetComponent<RectTransform>().sizeDelta = NewSize;
        transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = NewSize;
    }
}

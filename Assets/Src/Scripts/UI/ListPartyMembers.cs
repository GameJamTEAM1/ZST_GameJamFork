using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ListPartyMembers : MonoBehaviour
{
    public GameObject text;
    public Vector2 position;
    ScrollRect scrollRect;
    Transform content;
    int n = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        content = transform.Find("Viewport/Content");
        scrollRect = GetComponent<ScrollRect>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Add(text);
        }
    }

    public void Add(GameObject element)
    {
        GameObject tmp = Instantiate(element);
        tmp.transform.SetParent(content);
        tmp.transform.localPosition = new Vector2(0, n * scrollRect.verticalScrollbarSpacing);
        tmp.transform.localScale = Vector3.one;
        n++;
    }

    public void Display(List<CPartyMember> members)
    {

    }    
}

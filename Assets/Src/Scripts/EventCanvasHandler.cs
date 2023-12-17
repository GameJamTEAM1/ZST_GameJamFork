using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventCanvasHandler : MonoBehaviour {
    private GameObject prefab;

    public EventCanvasHandler(GameObject prefab) {
        this.prefab = prefab;
    }

    public void DispatchEventCanvas(EventProvider eventProvider) {
        var obj = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
        var canvas = obj.transform.GetChild(0);
        var comp = canvas.Find("Text").GetComponent<TMPro.TextMeshProUGUI>();

        comp.text = eventProvider.header;

        canvas.Find("Button").GetComponent<Button>().onClick.AddListener(() => {
            GameObject.Destroy(obj);
        });
    }
}

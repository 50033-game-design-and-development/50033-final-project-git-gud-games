using System.Collections.Generic;
using UnityEngine;

public class PlaceCandles : MonoBehaviour {
    private List<GameObject> candles;
    public void AddCandle() {
        //check if all purple candles have been removed: polish add some sfx showing error dragging
        if (GameObject.FindWithTag("purple_candle")) {
            return;
        }
        candles[0].GetComponent<MeshRenderer>().enabled = true;
        candles.RemoveAt(0);

        if (candles.Count == 0) {
            Event.L2.placeAllCandles.Raise();
            this.GetComponent<DragDoppable>().retainItem = false;
        }
    }
    // Start is called before the first frame update
    private void Start() {
        candles = new List<GameObject>();
        candles.Add(GameObject.Find("Yellow_Candle_1"));
        candles.Add(GameObject.Find("Yellow_Candle_2"));
        candles.Add(GameObject.Find("Yellow_Candle_3"));
        candles.Add(GameObject.Find("Yellow_Candle_4"));
        candles.Add(GameObject.Find("Yellow_Candle_5"));
    }
}

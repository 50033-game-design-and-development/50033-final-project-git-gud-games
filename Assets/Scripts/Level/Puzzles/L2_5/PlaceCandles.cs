using System.Collections.Generic;
using UnityEngine;

public class PlaceCandles : SFXAbstract {
    private List<GameObject> _candles;
    public void AddCandle() {
        //check if all purple candles have been removed: polish add some sfx showing error dragging
        if (GameObject.FindWithTag("purple_candle")) {
            return;
        }
        PlaySFX();
        _candles[0].SetActive(true);
        _candles.RemoveAt(0);

        if (_candles.Count != 0) {
            return;
        }
        Event.L2.placeAllCandles.Raise();
        GetComponent<DragDoppable>().retainItem = false;
    }
    // Start is called before the first frame update
    private void Start() {
        base.Start();
        _candles = new List<GameObject>();
        _candles.Add(GameObject.Find("Yellow_Candle_1"));
        _candles.Add(GameObject.Find("Yellow_Candle_2"));
        _candles.Add(GameObject.Find("Yellow_Candle_3"));
        _candles.Add(GameObject.Find("Yellow_Candle_4"));
        _candles.Add(GameObject.Find("Yellow_Candle_5"));

        for (int i = 0; i < _candles.Count; i++) {
            _candles[i].SetActive(false);
        }
    }
}

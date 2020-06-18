using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Text = UnityEngine.UI.Text;
public class SpeechBubble : MonoBehaviour
{
    [SerializeField]
    private Text _text;


    [SerializeField]
    [Range(1f, 20f)]
    private float _maxShowDuration = 10f;

    public string Text
    {
        get => _text == null ? null : _text.text;
        set => _text.text = value;
    }

    private float _showDuration;

    private float _currentTime;
    // Start is called before the first frame update
    void Start()
    {
        if (_text == null)
        {
            _text = GetComponentInChildren<Text>();
        }

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_showDuration > _maxShowDuration)
        {
            gameObject.SetActive(false);
        }

        _showDuration += Time.deltaTime;
        //todo: Remove outcommented code
        //Vector3 lookPos = Camera.main.transform.position - transform.position;
        //Quaternion lookRot = Quaternion.LookRotation(lookPos, Vector3.up);
        //float eulerY = lookRot.eulerAngles.y;
        //Quaternion rotation = Quaternion.Euler(0, eulerY, 0);
        //transform.rotation = rotation;

        //transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _showDuration = 0;
    }

    

}

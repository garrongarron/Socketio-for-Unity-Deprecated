using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;


/**
https://github.com/fpanettieri/unity-socket.io-DEPRECATED
*/
public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;
    public SocketIOComponent socket;
    public Text text;
    void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}
    void Start () {
		socket.On("new message", OnMessage);
	}

    public void JoinGame()
	{
		StartCoroutine(ConnectToServer());
	}

    IEnumerator ConnectToServer()
	{
        socket.Emit("connect");
		yield return new WaitForSeconds(0.5f);
        socket.Emit("add user", JSONObject.CreateStringObject("MyName"));
		yield return new WaitForSeconds(0.5f);
		socket.Emit("new message", JSONObject.CreateStringObject("MyMessage"));
	}

    void OnMessage(SocketIOEvent socketIOEvent)
	{
		Debug.Log(socketIOEvent.data.ToString());
        text.text += socketIOEvent.data.ToString()+ "\n"; 
	}
}

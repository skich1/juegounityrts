using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    public void CreateUser(string nickname, string nombre, string primerApellido, string segundoApellido, string correo, string pass, Action<Response> response)
    {
        StartCoroutine(CO_CreateUser(nickname, nombre, primerApellido, segundoApellido, correo, pass, response));
    }

    private IEnumerator CO_CreateUser(string nickname, string nombre, string primerApellido, string segundoApellido, string correo, string pass, Action<Response> response)
    {
        WWWForm form = new WWWForm();
        form.AddField("nickname", nickname);
        form.AddField("nombre", nombre);
        form.AddField("primerApellido", primerApellido);
        form.AddField("segundoApellido", segundoApellido);
        form.AddField("correo", correo);
        form.AddField("pass", pass);

        string url = "http://localhost/game/createuser.php";

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError("Error de red o HTTP: " + www.error);
                response(new Response { hecho = false, message = "Error de conexión" });
                yield break;
            }

            Debug.Log("JSON recibido: " + www.downloadHandler.text);

            try
            {
                Response responseObj = JsonUtility.FromJson<Response>(www.downloadHandler.text);
                response(responseObj);
            }
            catch (Exception e)
            {
                Debug.LogError("Error al deserializar JSON: " + e.Message);
                response(new Response { hecho = false, message = "Error en el formato de respuesta JSON" });
            }
        }
    }
    public void CheckUser(string nickname, string pass, Action<Response> response)
    {
        StartCoroutine(CO_CheckUser(nickname, pass, response));
    }

    private IEnumerator CO_CheckUser(string nickname, string pass, Action<Response> response)
    {
        WWWForm form = new WWWForm();
        form.AddField("nickname", nickname);
        form.AddField("pass", pass);

        string url = "http://localhost/game/checkuser.php";

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError("Error de red o HTTP: " + www.error);
                response(new Response { hecho = false, message = "Error de conexión" });
                yield break;
            }

            Debug.Log("JSON recibido: " + www.downloadHandler.text);

            try
            {
                Response responseObj = JsonUtility.FromJson<Response>(www.downloadHandler.text);
                response(responseObj);
            }
            catch (Exception e)
            {
                Debug.LogError("Error al deserializar JSON: " + e.Message);
                response(new Response { hecho = false, message = "Error en el formato de respuesta JSON" });
            }
        }
    }
    private void Start()
    {
        EventManager.TriggerEvent("LoadedScene");

    }
}

[Serializable]
public class Response
{
    public bool hecho = false;
    public string message = "";
}


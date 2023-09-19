using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class adminSQL : MonoBehaviour
{
    public string servidorBasededatos;
    public string servidorName;
    public string usuariobasededatos;
    public string contrabasededatos;

    private string datosconexion;
    public MySqlConnection conexion;

    void Start()
    {
        datosconexion = "Server=" + servidorBasededatos + ";database=" + servidorName + ";uid=" + usuariobasededatos + ";pwd=" + contrabasededatos + ";"; // Corrección en la cadena
        Conectarservidorbasededatos(); // Llama a la función para establecer la conexión
    }

    private void Conectarservidorbasededatos()
    {
        conexion = new MySqlConnection(datosconexion);
        try
        {
            conexion.Open();
            Debug.Log("Conexión establecida correctamente");
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error al conectar: " + ex.Message);
        }
    }
    public MySqlDataReader Select(string _select)
    {
        MySqlCommand cmd = conexion.CreateCommand();
        cmd.CommandText = "SELECT * FROM " + _select;
        MySqlDataReader Resultado = cmd.ExecuteReader();
        return Resultado;
    }

}

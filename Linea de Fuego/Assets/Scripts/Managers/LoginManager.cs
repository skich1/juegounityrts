using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using TMPro;
using UnityEngine.SceneManagement;


public class LoginManager : MonoBehaviour
{
    [SerializeField] private GameObject m_registrerUI = null;
    [SerializeField] private GameObject m_loginUI = null;
    [SerializeField] private TMP_Text m_text = null;
    [SerializeField] private TMP_InputField m_usernameInput = null;
    [SerializeField] private TMP_InputField m_nombreInput = null;
    [SerializeField] private TMP_InputField m_primerApellidoInput = null;
    [SerializeField] private TMP_InputField m_segundoApellidoInput = null;
    [SerializeField] private TMP_InputField m_emailInput = null;
    [SerializeField] private TMP_InputField m_password = null;
    [SerializeField] private TMP_InputField m_reenterpassword = null;

    private NetworkManager m_networkManager = null;
    public void Awake()
    {
        m_networkManager = GameObject.FindObjectOfType<NetworkManager>();
    }
    public void SubmitLogin()
    {
        if (string.IsNullOrWhiteSpace(m_usernameInput.text) ||
            string.IsNullOrWhiteSpace(m_password.text))
        {
            m_text.text = "Por favor llena todos los campos";
            return;
        }

        m_text.text = "Procesando ......";
        m_networkManager.CheckUser(m_usernameInput.text, m_password.text, delegate (Response response)
        {
            m_text.text = response.message;
            CoreBooter.instance.LoadMenu();
        });

    }
    public void SubmitRegistrer()
    {
        if (string.IsNullOrWhiteSpace(m_usernameInput.text) ||
            string.IsNullOrWhiteSpace(m_nombreInput.text) ||
            string.IsNullOrWhiteSpace(m_primerApellidoInput.text) ||
            string.IsNullOrWhiteSpace(m_emailInput.text) ||
            string.IsNullOrWhiteSpace(m_password.text) ||
            string.IsNullOrWhiteSpace(m_reenterpassword.text))
        {
            m_text.text = "Por favor llena todos los campos";
            return;
        }
        if (m_password.text == m_reenterpassword.text)
        {
            m_text.text = "Procesando ......";
            m_networkManager.CreateUser(m_usernameInput.text, m_nombreInput.text, m_primerApellidoInput.text, m_segundoApellidoInput.text, m_emailInput.text, m_password.text, delegate (Response response)
            {
                m_text.text = response.message;
                ShowLogin();
            });
        }
        else
        {
            m_text.text = "Contraseñas no son igual por favor verificar";
        }
    }
    public void ShowLogin()
    {
        m_loginUI.SetActive(true);
        m_registrerUI.SetActive(false);
    }
    public void ShowRegistrer()
    {
        m_loginUI.SetActive(false);
        m_registrerUI.SetActive(true);
    }


}


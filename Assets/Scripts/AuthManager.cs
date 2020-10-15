using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour
{
    public FirebaseAuth auth;

    public InputField emailForm;
    public InputField passwordForm;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    // Email ile Kayıt Olma
    public void SignUp()
    {
        string email = emailForm.text;
        string password = passwordForm.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("Canceled");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("Faulted");
                return;
            }

            // Başarılı işlem
            FirebaseUser newUser = task.Result;
            Debug.LogFormat("Yeni kullanıcı oluşturuldu: {0} {1}", newUser.DisplayName, newUser.UserId);
        });
    }

    // Anonim Kayıt Olma
    public void AnonymousSignUp()
    {
        auth.SignInAnonymouslyAsync().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("Canceled");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("Faulted");
                return;
            }

            FirebaseUser newUser = task.Result;
            Debug.LogFormat("Anonim kullanıcı oluşturuldu {0} {1}", newUser.DisplayName, newUser.UserId);
        });
    }

    // Login Giriş Yapma Fonksiyonu
    public void DoLogin()
    {
        string email = emailForm.text;
        string password = passwordForm.text;

        if(email != "" && password != "")
        {
            auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.Log("Canceled");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.Log("Faulted");
                    return;
                }

                FirebaseUser newUser = task.Result;
                Debug.LogFormat("Giriş Yapıldı! {0} {1}",
                    newUser.DisplayName,
                    newUser.UserId
                    );
            });
        }
    }

}

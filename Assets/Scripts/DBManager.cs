using Firebase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : MonoBehaviour
{
    void Start()
    {
        Initialization();
    }

    void Initialization()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Bağlantı kurulduktan sonra gerekli işlemler
                Debug.Log("DB Bağlantısı Kuruldu.");
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format("Hata {0}", dependencyStatus));
            }
        });
    }
}

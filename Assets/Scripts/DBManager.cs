using Firebase;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : MonoBehaviour
{
    void Start()
    {
        Initialization();
    }

    // Databese bağlantı işlemi
    void Initialization()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                // Bağlantı kurulduktan sonra gerekli işlemler
                Debug.Log("DB Bağlantısı Kuruldu.");
            }
            else
            {
                Debug.LogError(System.String.Format("Hata {0}", dependencyStatus));
            }
        });
    }
}

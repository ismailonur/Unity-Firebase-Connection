using Firebase;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : MonoBehaviour
{
    public DatabaseReference usersReference;

    string rnd;

    void Start()
    {
        rnd = Random.Range(0, 1000).ToString();
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
                usersReference = FirebaseDatabase.DefaultInstance.RootReference;

                // Bağlantı kurulduktan sonra gerekli işlemler
                Debug.Log("DB Bağlantısı Kuruldu.");

                SaveData("Onur enes", 15, true);

                // UpdateData("-MJktgR-g6aLW_z7cfJB", "İsmail Onur", 8, false);
            }
            else
            {
                Debug.LogError(System.String.Format("Hata {0}", dependencyStatus));
            }
        });
    }

    void UpdateData(string userID, string username, int level, bool loginStatus)
    {
        Dictionary<string, object> children = new Dictionary<string, object>();
        children["username"] = username;
        children["level"] = level;
        children["loginStatus"] = loginStatus;

        usersReference.Child(userID).UpdateChildrenAsync(children);
    }

    // Yeni kullanıcı eklerken çalışan fonksiyon
    void SaveData(string username, int level, bool loginStatus)
    {
        Users user = new Users(username, level, loginStatus);

        // user json'a dönüştürülür.
        string json = JsonUtility.ToJson(user);

        // string userID = usersReference.Push().Key;

        usersReference.Child("Users/"+rnd).SetRawJsonValueAsync(json);
    }
}

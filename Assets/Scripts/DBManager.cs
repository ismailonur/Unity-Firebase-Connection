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
                usersReference = FirebaseDatabase.DefaultInstance.GetReference("Users");

                // Bağlantı kurulduktan sonra gerekli işlemler
                Debug.Log("DB Bağlantısı Kuruldu.");

                // SaveData("Onur", 15, true);
                // UpdateData("-MJktgR-g6aLW_z7cfJB", "İsmail Onur", 8, false);
                // GetUserList();
                // DeleteUser("-MJlE6HNMKc0tmzid5Hp");

                // Listener ile odayı dinleme
                string userId = "-MJlG5U-5YZuElbUzqka";
                usersReference.Child(userId).ValueChanged += GetUserDetails;
            }
            else
            {
                Debug.LogError(System.String.Format("Hata {0}", dependencyStatus));
            }
        });
    }

    // Yeni kullanıcı eklerken çalışan fonksiyon
    void SaveData(string username, int level, bool loginStatus)
    {
        Users user = new Users(username, level, loginStatus);

        // user json'a dönüştürülür.
        string json = JsonUtility.ToJson(user);

        string userID = usersReference.Push().Key;

        usersReference.Child(userID).SetRawJsonValueAsync(json);
    }

    // Kullanıcı update ederken çalışan fonksiyon
    void UpdateData(string userID, string username, int level, bool loginStatus)
    {
        Dictionary<string, object> children = new Dictionary<string, object>();
        children["username"] = username;
        children["level"] = level;
        children["loginStatus"] = loginStatus;

        usersReference.Child(userID).UpdateChildrenAsync(children);
    }

    // Database'den veri çekemede kullanılan fonksiyon
    void GetUserList()
    {
        usersReference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Faulted");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                foreach (DataSnapshot userID in snapshot.Children)
                {
                    string _userId = userID.Key;

                    string username = snapshot.Child(userID.Key).Child("username").Value.ToString();

                    Debug.Log(_userId + " Kullanıcısının username: " + username);
                }
            }
        });
    }

    // Database'den veri silme fonksiyonu
    void DeleteUser(string userId)
    {
        usersReference.Child(userId).RemoveValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Faulted");
            }
            else if (task.IsCompleted)
            {
                Debug.Log(userId + " kullanıcısı silindi");
            }
        });
    }

    // Odayı dinleme işlemi
    void GetUserDetails(object sender, ValueChangedEventArgs args)
    {
        if(args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        // Başarılı sonuç olduktan sonra
        Debug.Log("Değişiklik algılandı");

        string username = args.Snapshot.Child("username").Value.ToString();

        Debug.Log(username);
    }
}

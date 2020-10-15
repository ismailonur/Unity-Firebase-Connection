using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Process : MonoBehaviour
{
    AuthManager authManager;

    private void Awake()
    {
        authManager = FindObjectOfType<AuthManager>();
    }

    void Start()
    {

    }

    public void AutoAuthentication()
    {
        var users = new Dictionary<string, string>();
        users.Add("email1@test.com", "123456");
        users.Add("email2@test.com", "123456");
        users.Add("email3@test.com", "123456");
        users.Add("email4@test.com", "123456");
        users.Add("email5@test.com", "123456");
        users.Add("email6@test.com", "123456");
        users.Add("email7@test.com", "123456");
        users.Add("email8@test.com", "123456");
        users.Add("email9@test.com", "123456");
        users.Add("email10@test.com", "123456");

        foreach(var usersAdd in users)
        {
            // authManager.SignUp(usersAdd.Key.ToString(), usersAdd.Value.ToString());
            // authManager.AnonymousSignUp();
        }
    }
}

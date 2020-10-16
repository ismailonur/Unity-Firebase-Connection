using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Users
{
    public string username;
    public int level;
    public bool loginStatus;
     
    // Constructor
    public Users(string username, int level, bool loginStatus)
    {
        this.username = username;
        this.level = level;
        this.loginStatus = loginStatus;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DBManager
{

    public static string username;

    public static int levelEN;

    public static int levelFR;

    public static string language;

    public static bool Loggedin { get { return username != null; } }    

    public static void Logout() { 
        username = null; 
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;

namespace JolieStoreApp.Conexiones
{
    public class Conexion
    {
        public static FirebaseClient firebase = new FirebaseClient("https://jolieapp-d2d82-default-rtdb.firebaseio.com/");
    }
}

<?php

    $connection = mysqli_connect("localhost","root","root","unitydatabase");

    //check connection
    if (mysqli_connect_errno()) 
    {
        echo "Error 1: Conexion fallida."; // Error code number '1' = Connection failed
        exit();
    }

    $email = mysqli_real_escape_string($connection, $_POST["email"]);
    $token = mysqli_real_escape_string($connection, $_POST["token"]);
    $new_password = $_POST["new_password"];
   
    //check if email exists in usuarios table
    $emailcheckquery_usuarios = "SELECT username, hash, salt, reset_token FROM usuarios WHERE email='" . $email . "';";
    $emailcheck_usuarios = mysqli_query($connection, $emailcheckquery_usuarios);

    //check if email exists in profesores table
    $emailcheckquery_profesores = "SELECT username, hash, salt, reset_token FROM profesores WHERE email='" . $email . "';";
    $emailcheck_profesores = mysqli_query($connection, $emailcheckquery_profesores);

    if (mysqli_num_rows($emailcheck_usuarios) == 0 && mysqli_num_rows($emailcheck_profesores) == 0) 
    {
        echo "Error 11: El email ingresado no existe."; // Error code number '11' = Email does not exist
        exit();
    }

    //get the token from the database
    if (mysqli_num_rows($emailcheck_usuarios) > 0) 
    {
        $info = mysqli_fetch_assoc($emailcheck_usuarios);
    }
    else 
    {
        $info = mysqli_fetch_assoc($emailcheck_profesores);
    } 

    //Update variables with the database values
    $stored_token = $info["reset_token"];
    $salt = $info["salt"];
    $username = $info["username"];

    //extract the creation time from the token and check if it has expired
    list($stored_token, $token_creation_time) = explode('_', $stored_token);
    $token_expiry_time_seconds = 10 * 60; 
    $current_time = time();

    if ($current_time - $token_creation_time > $token_expiry_time_seconds) 
    {
        echo "Error 9: El token ingresado ha expirado."; //Error code number 9: Expired token
        exit();
    }

    //check if the token matches
    if ($token != $stored_token) 
    {
        echo "Error 10: El token ingresado es invalido."; //Error code number 10: Invalid token
        exit();
    }

    //update the user's password in the correct table
    $salt = "\$5\$rounds=5000\$" . "12345seguridad12345" . $username . "\$";
    $hash = crypt($new_password, $salt);
    if (mysqli_num_rows($emailcheck_usuarios) > 0) 
    {
        $updatepasswordquery = "UPDATE usuarios SET hash='" . $hash . "', salt='" . $salt . "', reset_token=NULL WHERE email='" . $email . "';";
    } 
    else 
    {
        $updatepasswordquery = "UPDATE profesores SET hash='" . $hash . "', salt='" . $salt . "', reset_token=NULL WHERE email='" . $email . "';";
    }
    mysqli_query($connection, $updatepasswordquery) or die("Error 4: Consulta para actualizar la clave fallida."); // Error code number '4' = Update password query failed

    echo "0\t". "Clave restablecida correctamente.";
?>

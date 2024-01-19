<?php

    $connection = mysqli_connect("localhost","root","root","unitydatabase");

    //check connection
    if (mysqli_connect_errno()) 
    {
        echo "1: Conexion fallida."; // Error code number '1' = Connection failed
        exit();
    }

    $email = mysqli_real_escape_string($connection, $_POST["email"]);
    $token = mysqli_real_escape_string($connection, $_POST["token"]);
    $new_password = $_POST["new_password"];

    //check if email exists
    $emailcheckquery = "SELECT username, salt, hash, reset_token FROM usuarios WHERE email='" . $email . "';";

    $emailcheck = mysqli_query($connection, $emailcheckquery) or die("2: No se ha encontrado el email."); // Error code number '2' = Email check query failed

    if (mysqli_num_rows($emailcheck) == 0) 
    {
        echo "3: El email ingresado no existe."; // Error code number '3' = Email does not exist
        exit();
    }

    //get the token from the database
    $info = mysqli_fetch_assoc($emailcheck);
    $stored_token = $info["reset_token"];
    $salt = $info["salt"];
    $username = $info["username"];

    //extract the creation time from the token and check if it has expired
    list($stored_token, $token_creation_time) = explode('_', $stored_token);
    $token_expiry_time_seconds = 10 * 60; // Set this to the number of seconds a token is valid for
    $current_time = time();

    if ($current_time - $token_creation_time > $token_expiry_time_seconds) {
        echo "Token has expired.";
        exit();
    }

    //check if the token matches
    if ($token != $stored_token) {
        echo "Invalid token.";
        exit();
    }

    //update the user's password
    $salt = "\$5\$rounds=5000\$" . "12345seguridad12345" . $username . "\$";
    $hash = crypt($new_password, $salt);
    $updatepasswordquery = "UPDATE usuarios SET hash='" . $hash . "', salt='" . $salt . "', reset_token=NULL WHERE email='" . $email . "';";
    mysqli_query($connection, $updatepasswordquery) or die("4: Consulta para actualizar la clave fallida."); // Error code number '4' = Update password query failed

    echo("0");

?>

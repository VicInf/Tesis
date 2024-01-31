<?php

    $connection = mysqli_connect("localhost","root","root","unitydatabase");

    //check connection
    if (mysqli_connect_errno()) 
    {
        echo "Error 1: Conexion fallida."; // Error code number '1' = Connection failed
        exit();
    }

    $username = mysqli_real_escape_string($connection, $_POST["name"]);
    $usernameclean = filter_var($username, FILTER_SANITIZE_ENCODED, FILTER_FLAG_STRIP_LOW | FILTER_FLAG_STRIP_HIGH);

    if ($username != $usernameclean) 
    {
        echo "Error 7: Se han utilizado caracteres no soportados para el nombre de usuario."; //Error code number '7' = ASCII unsoported characters 
    }
    $password = $_POST["password"];
    $email = mysqli_real_escape_string($connection, $_POST["email"]); 

    if (!filter_var($email, FILTER_VALIDATE_EMAIL)) 
    {
        echo "Error 12: El formato del email es invalido."; // Error code number '8' = Invalid email format
        exit();
    }

    //check if username already exists
    $namecheckquery = "SELECT username, levelFR, levelEN FROM usuarios WHERE username='" . $username . "';";

    $namecheck = mysqli_query($connection, $namecheckquery) or die("Error 2: El nombre ingresado no se ha podido registrar."); // Error code number '2' = Name check query failed

    if (mysqli_num_rows($namecheck) > 0) 
    {
        echo "Error 3: El nombre ingresado no es valido."; // Error code number '3' = Name already exists so cannot register
        exit();
    }

    //add user to the table
    $salt = "\$5\$rounds=5000\$" . "12345seguridad12345" . $username . "\$";
    $hash = crypt($password, $salt);

    $insertuserquery = "INSERT INTO usuarios (username, hash, salt, email) VALUES ('" . $username . "', '" . $hash . "', '" . $salt . "', '" . $email . "');"; 
    mysqli_query($connection, $insertuserquery) or die("Error 4: Consulta para crear usuario fallida."); // Error code number '4' = Insert query failed

    echo "0\t". "Se ha registrado exitosamente.";

?>

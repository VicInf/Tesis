<?php

    $connection = mysqli_connect("localhost","root","root","unitydatabase");

    //check connection
    if (mysqli_connect_errno()) {
        echo "1: Conexion fallida."; // Error code number '1' = Connection failed
            exit();
    }

    $username = mysqli_real_escape_string($connection, $_POST["name"]);
    $usernameclean = filter_var($username, FILTER_SANITIZE_ENCODED, FILTER_FLAG_STRIP_LOW | FILTER_FLAG_STRIP_HIGH);

    if ($username != $usernameclean) {
        echo "7: Se han utilizado no soportados para el nombre de usuario"; //Error code number '7' = ASCII unsoported characters 
    }
    $password = $_POST["password"];

    //check if username already exists
    $namecheckquery = "SELECT username, salt, hash, level FROM usuarios WHERE username='" . $username . "';";

    $namecheck = mysqli_query($connection, $namecheckquery) or die("2: No se ha encontrado el nombre."); // Error code number '2' = Name check query failed

    if (mysqli_num_rows($namecheck) != 1) {
        echo "5: No hay usuarios con el nombre o hay mas de uno"; // Error code number '5' = Either there is no user or more than 1
        exit();
    }

    //get login info from query
    $info = mysqli_fetch_assoc($namecheck); //Variable that holds information from the database
    $salt = $info["salt"];
    $hash = $info["hash"];
    

    $loginhash = crypt($password, $salt);
    if ($hash != $loginhash) {
        echo "6: Clave Incorrecta"; //Error code number '6' = Password does not match with the one at database
    }
    else{
        echo "0\t". $info["level"];
    }
    


?>
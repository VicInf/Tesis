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
    $newlevel = $_POST["level"];

    // Double check there is only one user with the provide username
    $namecheckquery = "SELECT username FROM usuarios WHERE username ='" . $username . "';";

    $namecheck = mysqli_query($connection, $namecheckquery) or die("2: No se ha encontrado el nombre"); //Rrror code '2' = name check query failed
    if (mysqli_num_rows($namecheck) != 1)
    {
        echo "5: No hay usuarios con el nombre o hay mas de uno"; // Error code number '5' = Either there is no user or more than 1
        exit();
    }

    $updatequery = "UPDATE usuarios SET level = " . $newlevel . " WHERE username ='" . $username . "';";
    mysqli_query($connection, $updatequery) or die("7: Fallo de consulta"); // Error code number '7' = UPDATE query failed

    echo "0";

?>
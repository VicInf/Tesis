<?php

    $connection = mysqli_connect("localhost","root","root","unitydatabase");

    //check connection
    if (mysqli_connect_errno()) {
        echo "1: Conexion fallida."; // Error code number '1' = Connection failed
        exit();
    }

    $username = $_POST["name"];
    $password = $_POST["password"];

    //check if username already exists
    $namecheckquery = "SELECT username FROM profesores WHERE username='" . $username . "';";

    $namecheck = mysqli_query($connection, $namecheckquery) or die("2: No se ha encontrado el nombre."); // Error code number '2' = Name check query failed

    if (mysqli_num_rows($namecheck) > 0) {
        echo "3: El nombre ingresado ya existe."; // Error code number '3' = Name already exists so cannot register
        exit();
    }

    //add user to the table
    $salt = "\$5\$rounds=5000\$" . "tangamandapio" . $username . "\$";
    $hash = crypt($password, $salt);

    $insertuserquery = "INSERT INTO profesores (username, hash, salt) VALUES ('" . $username . "', '" . $hash . "', '" . $salt . "');";
    mysqli_query($connection, $insertuserquery) or die("4: Consulta para crear usuario fallida."); // Error code number '4' = Insert query failed

    echo("0");


?>
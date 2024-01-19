<?php
    $connection = mysqli_connect("localhost","root","root","unitydatabase");

    //check connection
    if (mysqli_connect_errno()) 
    {
        echo "1: Conexion fallida."; // Error code number '1' = Connection failed
            exit();
    }

    $username = mysqli_real_escape_string($connection, $_POST["name"]);
    $usernameclean = filter_var($username, FILTER_SANITIZE_ENCODED, FILTER_FLAG_STRIP_LOW | FILTER_FLAG_STRIP_HIGH);

    if ($username != $usernameclean) 
    {
        echo "7: Se han utilizado no soportados para el nombre de usuario"; //Error code number '7' = ASCII unsoported characters 
    }

    $query = "SELECT id_profesor FROM profesores WHERE username = ?";

    $stmt = $connection->prepare($query);

    //Binding position parameters
    $stmt->bind_param('s', $usernameclean);

    //Console Execute
    $stmt->execute();

    // Obtain results
    $result = $stmt->get_result();
    $row = $result->fetch_assoc();

    $profesor_id = $row['id_profesor'];

    $query = "
    SELECT u.username, u.levelEN, u.levelFR
    FROM usuarios AS u
    INNER JOIN profesores_usuarios AS pu ON u.id_usuario = pu.id_usuario
    WHERE pu.id_profesor = ?
    ";

    $stmt = $connection->prepare($query);

    //Binding position parameters
    $stmt->bind_param('i', $profesor_id);

    //Console Execute
    $stmt->execute();

    $result = $stmt->get_result();
    $usuarios = array();

    while ($row = $result->fetch_assoc()) {
        $usuarios[] = $row;
    }

    // Print each user on a new line
    foreach ($usuarios as $usuario) {
        echo implode(", ", $usuario);
        echo "\n";
    }


    $stmt->close();
?>


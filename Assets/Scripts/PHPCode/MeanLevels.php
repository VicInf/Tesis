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

    $levelEN_sum = 0;
    $levelFR_sum = 0;
    $count = 0;

    while ($row = $result->fetch_assoc()) {
        $levelEN_sum += $row['levelEN'];
        $levelFR_sum += $row['levelFR'];
        $count++;
    }

    if ($count > 0) {
        $levelEN_avg = floor($levelEN_sum / $count);
        $levelFR_avg = floor($levelFR_sum / $count);

        echo "Media de Nivel Inglés: " . $levelEN_avg . "\n";
        echo "Media de Nivel Francés: " . $levelFR_avg . "\n";
    } else {
        echo "No hay usuarios para calcular la media.\n";
    }

    $stmt->close();
?>

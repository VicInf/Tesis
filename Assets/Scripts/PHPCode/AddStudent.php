<?php
    $connection = mysqli_connect("localhost","root","root","unitydatabase");

    //check connection
    if (mysqli_connect_errno()) 
    {
        echo "1: Conexion fallida."; // Error code number '1' = Connection failed
            exit();
    }

    $username = mysqli_real_escape_string($connection, $_POST["username"]);
    $usernameclean = filter_var($username, FILTER_SANITIZE_ENCODED, FILTER_FLAG_STRIP_LOW | FILTER_FLAG_STRIP_HIGH);

    
    $teacherUsername = mysqli_real_escape_string($connection, $_POST["teacherUsername"]);
    $teacherUsernameclean = filter_var($teacherUsername, FILTER_SANITIZE_ENCODED, FILTER_FLAG_STRIP_LOW | FILTER_FLAG_STRIP_HIGH);

    // OObtain user id
    $query = "SELECT id_usuario FROM usuarios WHERE username = ?";
    $stmt = $connection->prepare($query);
    $stmt->bind_param('s', $usernameclean);
    $stmt->execute();
    $result = $stmt->get_result();
    $row = $result->fetch_assoc();
    $usuario_id = $row['id_usuario'];

    //Obtain teacher id
    $query = "Select id_profesor FROM profesores WHERE username = ?";
    $stmt = $connection->prepare($query);
    $stmt->bind_param('s', $teacherUsernameclean);
    $stmt->execute();
    $result = $stmt->get_result();
    $row = $result->fetch_assoc();
    $profesor_id = $row['id_profesor'];

    // Enlaza el usuario con el profesor
    $query = "INSERT INTO profesores_usuarios (id_profesor, id_usuario) VALUES (?, ?)";
    $stmt = $connection->prepare($query);
    $stmt->bind_param('ii', $profesor_id, $usuario_id);
    $stmt->execute();

    $stmt->close();
?>
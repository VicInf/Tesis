<?php
    $connection = mysqli_connect("localhost","root","root","unitydatabase");

    //check connection
    if (mysqli_connect_errno()) 
    {
        echo "Error 1: Conexion fallida."; // Error code number '1' = Connection failed
        exit();
    }

    $username = mysqli_real_escape_string($connection, $_POST["username"]);
    $usernameclean = filter_var($username, FILTER_SANITIZE_ENCODED, FILTER_FLAG_STRIP_LOW | FILTER_FLAG_STRIP_HIGH);

    $teacherUsername = mysqli_real_escape_string($connection, $_POST["teacherUsername"]);
    $teacherUsernameclean = filter_var($teacherUsername, FILTER_SANITIZE_ENCODED, FILTER_FLAG_STRIP_LOW | FILTER_FLAG_STRIP_HIGH);

    // Obtain user id
    $query = "SELECT id_usuario FROM usuarios WHERE username = ?";
    $stmt = $connection->prepare($query);
    $stmt->bind_param('s', $usernameclean);
    $stmt->execute();
    $result = $stmt->get_result();
    $row = $result->fetch_assoc();
    $usuario_id = $row['id_usuario'];

    // Check if the student is in the database
    if (!$usuario_id){
        echo "Error 2: El nombre ingresado no se ha podido registrar."; // Error code number '2' = Student not found
        exit();
    }

    // Obtain teacher id
    $query = "SELECT id_profesor FROM profesores WHERE username = ?";
    $stmt = $connection->prepare($query);
    $stmt->bind_param('s', $teacherUsernameclean);
    $stmt->execute();
    $result = $stmt->get_result();
    $row = $result->fetch_assoc();
    $profesor_id = $row['id_profesor'];

    // Check if the student is already in a class with that professor
    $query = "SELECT * FROM profesores_usuarios WHERE id_profesor = ? AND id_usuario = ?";
    $stmt = $connection->prepare($query);
    $stmt->bind_param('ii', $profesor_id, $usuario_id);
    $stmt->execute();
    $result = $stmt->get_result();
    if ($result->num_rows > 0) {
        echo "Error 3: El nombre ingresado no es valido."; // Error code number '3' = Student is already in a class with that professor
        exit();
    }

    // Link the user with the professor
    $query = "INSERT INTO profesores_usuarios (id_profesor, id_usuario) VALUES (?, ?)";
    $stmt = $connection->prepare($query);
    $stmt->bind_param('ii', $profesor_id, $usuario_id);
    $stmt->execute();

    echo "0\t". "Se ha agregado exitosamente al estudiante.";
    $stmt->close();
?>

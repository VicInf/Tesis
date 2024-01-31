<?php
    $connection = mysqli_connect("localhost","root","root","unitydatabase");

    //check connection
    if (mysqli_connect_errno()) 
    {
        echo "Error 1: Conexion fallida."; // Error code number '1' = Connection failed
        exit();
    }

    $teacherUsername = mysqli_real_escape_string($connection, $_POST["teacherUsername"]);
    $teacherUsernameclean = filter_var($teacherUsername, FILTER_SANITIZE_ENCODED, FILTER_FLAG_STRIP_LOW | FILTER_FLAG_STRIP_HIGH);

    // Obtain teacher id
    $query = "SELECT id_profesor FROM profesores WHERE username = ?";
    $stmt = $connection->prepare($query);
    $stmt->bind_param('s', $teacherUsernameclean);
    $stmt->execute();
    $result = $stmt->get_result();
    $row = $result->fetch_assoc();
    $profesor_id = $row['id_profesor'];

    // Delete all students from the class of that professor
    $query = "DELETE FROM profesores_usuarios WHERE id_profesor = ?";
    $stmt = $connection->prepare($query);
    $stmt->bind_param('i', $profesor_id);
    $stmt->execute();

    echo "Se han eliminado todos los estudiantes."; 

    $stmt->close();
?>

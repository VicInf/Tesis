<?php
    $connection = mysqli_connect("localhost","root","root","unitydatabase");

    //check connection
    if (mysqli_connect_errno())
    {
        echo "1: Conexion fallida."; // Error code number '1' = Connection failed
        exit();
    }

    $playerToken = mysqli_real_escape_string($connection, $_POST["playerToken"]);
    $lastScenePlayed = mysqli_real_escape_string($connection, $_POST["lastScenePlayed"]);

    // Fetch the current ReachedIndex and level from the database
    $levelQuery = "SELECT reachindexEN, levelEN FROM usuarios WHERE username='" . $playerToken . "';";
    $levelResult = mysqli_query($connection, $levelQuery) or die("2: No se ha encontrado el nombre."); // Error code number '2' = Name check query failed

    
    if ($row = mysqli_fetch_assoc($levelResult)) 
    {
        $reachedIndexEN = $row['reachindexEN'];
        $levelEN = $row['levelEN'];

        // Check if the last scene played is greater than or equal to the ReachedIndex
        if ($lastScenePlayed >= $reachedIndexEN) 
        {
            // If so, increment both ReachedIndex and level
            $reachedIndexEN = $lastScenePlayed + 1;
            $levelEN = $levelEN + 1;   
            
            // Save the new values back to the database
            $updateQuery = "UPDATE usuarios SET reachindexEN = " . $reachedIndexEN . ", levelEN = " . $levelEN .  " WHERE username = '" . $playerToken . "';";
            mysqli_query($connection, $updateQuery) or die("4: Consulta para actualizar usuario fallida."); // Error code number '4' = Update query failed

            // Fetch the updated level from the database
            $levelResult = mysqli_query($connection, $levelQuery) or die("5: Failed to fetch updated level."); // Error code number '5' = Failed to fetch updated level
            if ($updatedRow = mysqli_fetch_assoc($levelResult)) 
            {
                $levelEN = $updatedRow['levelEN'];
            }
        }
    }

    else
    {
        echo "3: El nombre ingresado ya existe."; // Error code number '3' = Name already exists so cannot register
        exit();
    }

    echo "0\t". $levelEN;
    exit(); 
?>
<?php
 
    use PHPMailer\PHPMailer\PHPMailer;
    use PHPMailer\PHPMailer\Exception;
    use PHPMailer\PHPMailer\SMTP;
    
    require 'PHPMailer/Exception.php';
    require 'PHPMailer/PHPMailer.php';
    require 'PHPMailer/SMTP.php';

   

    $connection = mysqli_connect("localhost","root","root","unitydatabase");

    //check connection
    if (mysqli_connect_errno()) 
    {
        echo "Error 1: Conexion fallida."; // Error code number '1' = Connection failed
        exit();
    }

    $email = mysqli_real_escape_string($connection, $_POST["email"]);
    
    //check if email exists in usuarios table
    $emailcheckquery_usuarios = "SELECT username, email FROM usuarios WHERE email='" . $email . "';";
    $emailcheck_usuarios = mysqli_query($connection, $emailcheckquery_usuarios);

    //check if email exists in profesores table
    $emailcheckquery_profesores = "SELECT username, email FROM profesores WHERE email='" . $email . "';";
    $emailcheck_profesores = mysqli_query($connection, $emailcheckquery_profesores);

    if (mysqli_num_rows($emailcheck_usuarios) == 0 && mysqli_num_rows($emailcheck_profesores) == 0) 
    {
        echo "Error 11: El email ingresado no existe."; // Error code number '11' = Email does not exist
        exit();
    }

    //generate a unique token and append the current timestamp to it
    $token = substr(str_shuffle('0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ'), 0, 6) . '_' . time();

    //store the token in the correct table
    if (mysqli_num_rows($emailcheck_usuarios) > 0) 
    {
        $storetokenquery = "UPDATE usuarios SET reset_token='" . $token . "' WHERE email='" . $email . "';";
    } 
    else 
    {
        $storetokenquery = "UPDATE profesores SET reset_token='" . $token . "' WHERE email='" . $email . "';";
    }
    mysqli_query($connection, $storetokenquery) or die("Error 4: Consulta para almacenar el token fallida."); // Error code number '4' = Store token query failed

    //create a new PHPMailer instance
    $mail = new PHPMailer(true);
    
    try 
    {
        //Server settings
        $mail->isSMTP();                                      
        $mail->Host = 'smtp-mail.outlook.com';                    
        $mail->SMTPAuth = true;                              
        $mail->Username = 'aprenderinglesfrances@hotmail.com';                 
        $mail->Password = 'Z12345678.';                         
        $mail->Port = 587;                                    

        //Recipients
        $mail->setFrom('AprenderInglesFrances@hotmail.com', 'UnityGame');
        $mail->addAddress($email);                         

        //Content
        $mail->Subject = 'Recuperar clave';
        $mail->Body    = 'Tu token para recuperar su clave es: ' . substr($token, 0 , 6) . '. El token expirara en 10 minutos.';

        $mail->send();
        echo "0\t". "Se le ha enviado un email con el token para restablecer la clave.";
    } 
    catch (Exception $e) 
    {
        echo 'Error 8: El Email no pudo ser enviado. ', $mail->ErrorInfo; // Error Code number '8' = Email cannot be send
    }
?>

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
        echo "1: Conexion fallida."; // Error code number '1' = Connection failed
        exit();
    }

    $email = mysqli_real_escape_string($connection, $_POST["email"]);
    
    //check if email exists
    $emailcheckquery = "SELECT username, email FROM usuarios WHERE email='" . $email . "';";

    $emailcheck = mysqli_query($connection, $emailcheckquery) or die("2: No se ha encontrado el email."); // Error code number '2' = Email check query failed
    
    if (mysqli_num_rows($emailcheck) == 0) 
    {
        echo "3: El email ingresado no existe."; // Error code number '3' = Email does not exist
        exit();
    }

    //generate a unique token and append the current timestamp to it
    $token = substr(str_shuffle('0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ'), 0, 6) . '_' . time();
    
    //store the token in the database
    $storetokenquery = "UPDATE usuarios SET reset_token='" . $token . "' WHERE email='" . $email . "';";
    mysqli_query($connection, $storetokenquery) or die("4: Consulta para almacenar el token fallida."); // Error code number '4' = Store token query failed

    //create a new PHPMailer instance
    $mail = new PHPMailer(true);
    
    try {
        
        //Server settings
        $mail->isSMTP();                                      // Set mailer to use SMTP
        $mail->Host = 'smtp-mail.outlook.com';                     // Specify main and backup SMTP servers
        $mail->SMTPAuth = true;                               // Enable SMTP authentication
        $mail->Username = 'aprenderinglesfrances@hotmail.com';                 // SMTP username
        $mail->Password = 'Z12345678.';                         // SMTP password
        $mail->Port = 587;                                    

        //Recipients
        $mail->setFrom('AprenderInglesFrances@hotmail.com', 'UnityGame');
        $mail->addAddress($email);                            // Add a recipient

        //Content
        $mail->Subject = 'Recuperar clave';
        $mail->Body    = 'Tu token para recuperar su clave es: ' . substr($token, 0 , 6) . '. El token expirara en 10 minutos.';

        $mail->send();
        echo '0';
    } catch (Exception $e) {
        echo 'Message could not be sent. Mailer Error: ', $mail->ErrorInfo;
    }
?>

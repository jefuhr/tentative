<?php
  include("../env/account.php");
  include("../func/jsonfns.php");
  include("../func/dbfns.php");

  header('Content-type: application/json');

  $db = mysqli_connect($hostname, $username, $password, $project);
  if (mysqli_connect_errno()) {
    echo creturn_json("503", "Unable to connect to database");
    exit();
  }

?>
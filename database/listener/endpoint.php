<?php
  include("../env/account.php");
  include("../func/jsonfns.php");
  include("../func/dbfns.php");

  header('Content-type: application/json');

  $db = mysqli_connect($hostname, $username, $password, $project);
  if (mysqli_connect_errno()) {
    echo convert_to_json("503", "Unable to connect to database");
    exit();
  }

  $req = $_POST["req"];
  $json = json_decode($req);
  
  $action = $json->$action;

  if(strcmp($action, "register_user") == 0) {
    // register user
  } else if (strcmp($action, "login") == 0) {
    // login user
  } else if (strcmp($action, "update_user") == 0) {
    // update player resources
  } else if (strcmp($action, "get_currency_data") == 0) {
    // get individual currency data
  } else if (strcmp($action, "get_all_currency_data") == 0) {
    // get all currency data
  } else if (strcmp($action, "get_all_player_trades") == 0) {
    // get all player trades
  } else if (strcmp($action, "add_new_trade") == 0) {
    // add trade
  } else if (strcmp($action, "delete_trade") == 0) {
    // delete trade
  }



?>
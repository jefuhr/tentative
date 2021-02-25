#!/usr/bin/php
<?php
  header('Content-type: application/json');

  include("../env/account.php");
  include("../func/jsonfns.php");
  include("../func/dbfns.php");

  

  $db = mysqli_connect($hostname, $username, $password, $project);
  if (mysqli_connect_errno()) {
    echo return_json("503", "Unable to connect to database.");
    exit();
  }

  // $req = $_POST["req"];
  // $req = "{
  //   \"action\" : \"login_user\",
  //   \"contents\" : {
  //     \"username\" : \"test\",
  //     \"password\" : \"hashed_password\"
  //   }
  // }";
  $req = "{
    \"action\" : \"get_currency_data\",
    \"contents\" : {
      \"currencyType\" : \"0\"
    }
  }";
  $json = json_decode($req);
  
  $action = $json->action;
  $contents = $json->contents;

  if(strcmp($action, "register_user") == 0) {
    $username = $contents->username;
    $password = $contents->password;
    echo register($username, $password);
    
  } else if (strcmp($action, "login_user") == 0) {
    $username = $contents->username;
    $password = $contents->password;
    echo login($username, $password);

  } else if (strcmp($action, "update_user") == 0) {
    // update player resources

  } else if (strcmp($action, "get_currency_data") == 0) {
    $currencyType = $contents->currencyType;
    echo get_currency_data($currencyType);

  } else if (strcmp($action, "update_currency") == 0) {
    $currencyType = $contents->currencyType;
    $currentValue = $contents->currentValue;
    echo update($currencyType, $currentValue);
  
  } else if (strcmp($action, "get_all_currency_data") == 0) {
    echo get_all_currency_data();

  } else if (strcmp($action, "get_all_player_trades") == 0) {
    echo get_all_player_trades();

  } else if (strcmp($action, "add_new_trade") == 0) {
    // add trade

  } else if (strcmp($action, "delete_trade") == 0) {
    $tradeID = $contents->tradeID;
    echo delete_trade($tradeID);

  } else {
    echo return_json( "400", "Invalid action." );
  }

  echo "\n";
  exit();
?>
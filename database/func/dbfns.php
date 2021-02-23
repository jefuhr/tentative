<?php
  function login( $username, $passwordword ) { // Returns a JSON object based on if user info is valid
    global $db;
    global $userData;

    $s = "SELECT * FROM `$userData` WHERE username='$username' AND password='$password'"; 
    ( $t = mysqli_query($db, $s) ) or die ( return_json( "503", "Database error occured while authenticating login." ) );
    $num = mysqli_num_rows ( $t );

    if( $num >= 1 ) {
      while ( $r = mysqli_fetch_array ( $t, MYSQLI_ASSOC) ) {
        return return_json( "200", convert_user_to_json( $r )); 
      }
    } else {
      return return_json( "403", "Invalid username or password." );
    }
  }

  function get_username( $uuid ) { // Returns a username for a given uuid
    global $db;
    global $userData;

    $s = "SELECT * FROM `$userData` WHERE uuid='$uuid'"; 
    ( $t = mysqli_query($db, $s) ) or die ( return_json( "503", "Database error occured while fetching username." ) );
    $num = mysqli_num_rows ( $t );

    if( $num >= 1 ) {
      while ( $r = mysqli_fetch_array ( $t, MYSQLI_ASSOC) ) {
        return $r["username"]; 
      }
    } else {
      return return_json( "403", "Invalid uuid" );
    }
  }

  function get_currency_data( $currencyType ) { // Returns a JSON object of currencyType
    global $db;
    global $currencyData;

    $s = "SELECT * FROM `$currencyData` WHERE currencyType='$currencyType'"; 
    ( $t = mysqli_query($db, $s) ) or die ( return_json( "503", "Database error occured while fetching currency data." ) );
    $num = mysqli_num_rows ( $t );

    if( $num >= 1 ) {
      while ( $r = mysqli_fetch_array ( $t, MYSQLI_ASSOC) ) {
        return return_json( "200", convert_currency_to_json( $r )); 
      }
    } else {
      return return_json( "403", "Invalid currency type." );
    }
  }

  function get_all_currency_data() { // Returns a JSON object of all currency
    global $db;
    global $currencyData;

    $s = "SELECT * FROM `$currencyData`"; 
    ( $t = mysqli_query($db, $s) ) or die ( return_json( "503", "Database error occured while fetching all currency data." ) );
    $num = mysqli_num_rows ( $t );

    $all = [];
    $id = 0;
    while ( $r = mysqli_fetch_array ( $t, MYSQLI_ASSOC) ) {
      $all += ["$id" => convert_currency_to_json( $r )];
      $id += 1;
    }

    return return_json( "200", $all );
  }

  function get_all_player_trades() { // Returns a JSON of all trades in marketplace
    global $db;
    global $tradingData;

    $s = "SELECT * FROM `$currencyData`"; 
    ( $t = mysqli_query($db, $s) ) or die ( return_json( "503", "Database error occured while fetching all trade data." ) );
    $num = mysqli_num_rows ( $t );

    $all = [];
    $id = 0;
    while ( $r = mysqli_fetch_array ( $t, MYSQLI_ASSOC) ) {
      $tradeData = convert_trade_to_json( $r );
      $tradeData += ["username" => get_username($r["uuid"])];
      $all += ["$id" => $tradeData];
      $id += 1;
    }
    $all += ["count" => $id];

    return return_json( "200", $all );
  }

  function add_trade( $uuid, $itemType, $itemQuant, $requestType, $requestQuant ) { // add new trade marketplace
    global $db;
    global $tradingData;
    $s = "INSERT INTO `$tradingData`(`uuid`, `itemType`, `itemQuant`, `requestType`, `requestQuant`) VALUES ('$uuid', '$itemType', '$itemQuant', '$requestType', '$requestQuant')";
    ( $t = mysqli_query($db, $s) ) or die ( return_json( "503", "Database error occured while submitting trade for Player: $uuid." ) );
    return return_json( "200", "Successfully submitted trade for Player: $uuid to marketplace." );
  }

  function delete_trade( $tradeID ) { // delete trade from marketplace
    global $db;
    global $tradingData;
    $s = "DELETE FROM `$tradingData` WHERE tradeID='$tradeID'";
    ( $t = mysqli_query($db, $s) ) or die ( return_json( "503", "Database error occured while deleting Trade: $tradeID." ) );
    return return_json( "200", "Successfully deleted Trade: $tradeID from marketplace." ); 
  }

  function update_player_resources( $uuid, $food, $wood, $stone, $leather, $iron, $gold, $currency0, $currency1, $currency2 ) { // Update player resource values
    global $db;
    global $userData;
    $s = "UPDATE `$userData` SET `food`='$food', `wood`='$wood', `stone`='$stone', `leather`='$leather', `iron`='$iron', `gold`='$gold', `currency0`='$currency0', `currency1`='$currency1', `currency2`='$currency2' WHERE uuid='$uuid'";
    ( $t = mysqli_query($db, $s) ) or die ( return_json( "503", "Database error occured while updating resources for Player: $uuid." ) );
    return return_json("200", "Successfully updated resources for Player: $uuid.");
  }

?>
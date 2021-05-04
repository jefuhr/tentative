<?php
  mysqli_report(MYSQLI_REPORT_ERROR | MYSQLI_REPORT_STRICT);

  function simple_query( $s, $error, $success ) {
    global $db;

    try {
      ( $t = mysqli_query($db, $s) ); 
    } catch (Exception $e)  {
      return return_json( "502", $error );
    }
    return return_json("200", $success);
  }

  function generate_forum_json( $s, $error ) {
    global $db;
    
    try {
      ( $t = mysqli_query($db, $s) );
    } catch (Exception $e)  {
      return return_json( "502", $error );
    }

    $all = [];
    $id = 0;
    while ( $r = mysqli_fetch_array ( $t, MYSQLI_ASSOC) ) {
      $forum = convert_forum_to_json( $r );
      $forum += ["username" => get_username($r["uuid"])];
      $all += ["$id" => $forum];
      $id += 1;
    }
    $all += ["count" => $id];

    return return_json( "200", $all );
  }

  function login( $username, $password ) { // Returns a JSON object based on if user info is valid
    global $db;
    global $userData;

    $error1 = "Database error occured while authenticating login for user: {$username}.";
    $error2 = "Invalid username or password.";
    $s = "SELECT * FROM `$userData` WHERE `username`='$username' and `password`='$password'"; 

    try {
      ( $t = mysqli_query($db, $s) ); 
    } catch (Exception $e) {
      return return_json( "502", $error1 );
    }

    $num = mysqli_num_rows ( $t );
    if( $num >= 1 ) {
      while ( $r = mysqli_fetch_array ( $t, MYSQLI_ASSOC) ) {
        // if (password_verify($password, $r["password"])) {
        //   return return_json( "200", convert_user_to_json( $r )); 
        // } else {
        //   return return_json( "403", "Invalid username or password." );
        // }
        return return_json( "200", convert_user_to_json( $r )); 
      }
    } else {
      return return_json( "403", $error2 );
    }
  }

  function register( $username, $password ) {
    global $userData;

    $error = "Database error occured while registering new user: {$username}.";
    $success = "Sucessfully registered new user: {$username}.";
    $s = "INSERT INTO `$userData` (`username`, `password`) VALUES ('$username', '$password')"; 

    return simple_query($s, $error, $success);
  }

  function get_username( $uuid ) { // Returns a username for a given uuid
    global $db;
    global $userData;

    $s = "SELECT * FROM `$userData` WHERE uuid='$uuid'"; 
    try {
      ( $t = mysqli_query($db, $s) ); 
    } catch (Exception $e)  { 
      return "NULL";
    }

    $num = mysqli_num_rows ( $t );
    if( $num >= 1 ) {
      while ( $r = mysqli_fetch_array ( $t, MYSQLI_ASSOC) ) {
        return $r["username"]; 
      }
    } else {
      return "NULL"; // UUID doesnt exist (in theory should never run)
    }
  }

  function get_currency_data( $currencyType ) { // Returns a JSON object of currencyType
    global $db;
    global $currencyData;

    $error1 = "Database error occured while fetching currency data for currencyType: {$currencyType}.";
    $error2 = "Invalid currencyType: {$currencyType}.";
    $s = "SELECT * FROM `$currencyData` WHERE currencyType='$currencyType'"; 

    try {
      ( $t = mysqli_query($db, $s) ); 
    } catch (Exception $e)  {
      return return_json( "502", $error1 );
    }

    $num = mysqli_num_rows ( $t );
    if( $num >= 1 ) {
      while ( $r = mysqli_fetch_array ( $t, MYSQLI_ASSOC) ) {
        return return_json( "200", convert_currency_to_json( $r )); 
      }
    } else {
      return return_json( "404", $error2 );
    }
  }

  function update_currency_data( $currencyType, $currentValue ) { // Update currentValue for currencyType
    global $currencyData;

    $error = "Database error occured while updating currencyValue for currencyType: {$currencyType}.";
    $success = "Successfully updated currentValue for currencyType: {$currencyType}.";
    $s = "UPDATE `$currencyData` SET `currentValue`='$currentValue' WHERE `currencyType`='$currencyType'";

    return simple_query($s, $error, $success);
  }

  function get_all_currency_data() { // Returns a JSON object of all currency
    global $db;
    global $currencyData;

    $error = "Database error occured while fetching all currency data.";
    $s = "SELECT * FROM `$currencyData`"; 

    try {
      ( $t = mysqli_query($db, $s) ); 
    } catch (Exception $e)  {
      return return_json( "502", $error );
    }

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

    $error = "Database error occured while fetching all trade data.";
    $s = "SELECT * FROM `$tradingData`"; 

    try {
      ( $t = mysqli_query($db, $s) ); 
    } catch (Exception $e)  {
      return return_json( "502", $error );
    }

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
    global $tradingData;

    $error = "Database error occured while submitting trade for Player: {$uuid}.";
    $success = "Successfully submitted trade for Player: {$uuid} to marketplace.";
    $s = "INSERT INTO `$tradingData`(`uuid`, `itemType`, `itemQuant`, `requestType`, `requestQuant`) VALUES ('$uuid', '$itemType', '$itemQuant', '$requestType', '$requestQuant')";

    return simple_query($s, $error, $success);
  }

  function accept_trade( $tradeID, $uuid ){
    global $db;
    global $tradingData;

    $error = "Database error occured while accepting trade: {$tradeID} for Player: {$uuid}.";
    $success = "Successfully acccepted trade: {$tradeID} for Player: {$uuid}.";
    $s = "UPDATE `$tradingData` SET `buyerID`='$uuid' WHERE `tradeID`='$tradeID' and `buyerID` IS NULL";
    echo "$s" . "\n";
    $num = 0;
    try {
      ( $t = mysqli_query($db, $s) ); 
      $num = mysqli_affected_rows($db);
    } catch (Exception $e)  {
      return return_json( "502", $error );
    }

    if ( $num ) {
      return return_json( "200", $success );
    }
    return return_json( "502", $error );
  }

  function delete_trade( $tradeID ) { // delete trade from marketplace
    global $tradingData;

    $error = "Database error occured while deleting Trade: {$tradeID}.";
    $success = "Successfully deleted Trade: {$tradeID} from marketplace.";
    $s = "DELETE FROM `$tradingData` WHERE tradeID='$tradeID'";

    return simple_query($s, $error, $success);
  }

  function update_player_resources( $uuid, $food, $wood, $stone, $leather, $iron, $gold, $currency0, $currency1, $currency2, $tilejson ) { // Update player resource values
    global $userData;

    $error = "Database error occured while updating resources for Player: {$uuid}.";
    $success = "Successfully updated resources for Player: {$uuid}.";
    $s = "UPDATE `$userData` SET `food`='$food', `wood`='$wood', `stone`='$stone', `leather`='$leather', `iron`='$iron', `gold`='$gold', `currency0`='$currency0', `currency1`='$currency1', `currency2`='$currency2', `tilejson`='$tilejson' WHERE `uuid`='$uuid'";
    
    return simple_query($s, $error, $success);
  }

  function add_forum_post( $uuid, $topic, $message ){
    global $db;
    global $forumData;

    $error = "Database error occured while submitting forum post for Player: {$uuid}.";
    $success = "Successfully submitted forum post for Player: {$uuid} to forum.";
    $s = "SELECT * FROM `$forumData` WHERE `topic` IS NOT NULL"; 

    try {
      ( $t = mysqli_query($db, $s) ); 
    } catch (Exception $e)  {
      return return_json( "502", $error );
    }
    $num = mysqli_num_rows ( $t );

    $s = "INSERT INTO `$forumData` (`uuid`, `topicID`, `topic`, `message`) VALUES ('$uuid', '$num', '$topic', '$message')";

    return simple_query($s, $error, $success);
  }

  function add_forum_reply( $uuid, $topicID, $message ){
    global $forumData;

    $error = "Database error occured while submitting forum reply for Player: {$uuid}.";
    $success = "Successfully submitted forum reply for Player: {$uuid} to forum.";
    $s = "INSERT INTO `$forumData` (`uuid`, `topicID`, `topic`, `message`) VALUES ('$uuid', '$topicID', NULL, '$message')";

    return simple_query($s, $error, $success);
  }

  function get_all_forum_topics() {
    global $forumData;

    $s = "SELECT * FROM `$forumData` WHERE `topic` IS NOT NULL"; 

    return generate_forum_json($s, "Database error occured while fetching forum topics." );
  }

  function get_all_forum_replies($topicID) {
    global $forumData;

    $s = "SELECT * FROM `$forumData` WHERE `topicID`=$topicID and `topic` IS NULL"; 

    return generate_forum_json($s, "Database error occured while fetching forum replies." );
  }

  function get_mission($missionID) {
    global $db;
    global $missionData;

    $error = "Database error occured while fetching data for missionID: {$missionID}.";
    $s = "SELECT * FROM `$missionData` WHERE `missionID`='$missionID'"; 
    try {
      ( $t = mysqli_query($db, $s) ); 
    } catch (Exception $e)  {
      return return_json( "502", $error );
    }

  
    $num = mysqli_num_rows ( $t );
    if( $num >= 1 ) {
      while ( $r = mysqli_fetch_array ( $t, MYSQLI_ASSOC) ) {
        return return_json( "200", convert_mission_to_json( $r )); 
      }
    } else {
      return return_json( "502", $error );
    }
  }
?>
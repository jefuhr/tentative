#!/usr/bin/php
<?php
  header('Content-type: application/json');

  require_once('path.inc');
  require_once('get_host_info.inc');
  require_once('rabbitMQLib.inc');
  require_once("../env/account.php");
  require_once("../func/jsonfns.php");
  require_once("../func/dbfns.php");

  $db = mysqli_connect($hostname, $username, $password, $project);
  if (mysqli_connect_errno()) {
    echo return_json("503", "Unable to connect to database.");
    exit();
  }

  function doAction($json) {
    global $db;
    $action = $json->action;
    $response = array();
    $response["type"] = "database_response";
  
    switch ($action) {
      case "register_user":
        $contents = $json->contents;
        $username = mysqli_real_escape_string($db, $contents->username);
        $password = mysqli_real_escape_string($db, $contents->password);
        $response["message"] = register($username, $password);
        break;
  
      case "login_user":
        $contents = $json->contents;
        $username = mysqli_real_escape_string($db, $contents->username);
        $password = mysqli_real_escape_string($db, $contents->password);
        $response["message"] = login($username, $password);
        break;
  
      case "update_user":
        $contents = $json->contents;
        // var_dump($contents);
        $uuid = $contents->uuid;
        $food = $contents->food;
        $wood = $contents->wood;
        $stone = $contents->stone;
        $leather = $contents->leather;
        $iron = $contents->iron;
        $gold = $contents->gold;
        $currency0 = $contents->currency0;
        $currency1 = $contents->currency1;
        $currency2 = $contents->currency2;
        $tileJson = mysqli_real_escape_string($db, json_encode($contents->tilejson));
        $response["message"] = update_player_resources($uuid, $food, $wood, $stone, $leather, $iron, $gold, $currency0, $currency1, $currency2, $tileJson);
        break;
  
      case "get_currency_data":
        $contents = $json->contents;
        $currencyType = $contents->currencyType;
        $response["message"] = get_currency_data($currencyType);
        break;
  
      case "update_currency":
        $contents = $json->contents;
        $currencyType = $contents->currencyType;
        $currentValue = $contents->currentValue;
        $response["message"] = update_currency_data($currencyType, $currentValue);
        break;
  
      case "get_all_currency_data": 
        $response["message"] = get_all_currency_data();
        break;
  
      case "get_all_player_trades":
        $response["message"] = get_all_player_trades(); 
        break;
  
      case "add_new_trade":
        $contents = $json->contents;
        $uuid = $contents->uuid;
        $itemType = $contents->itemType;
        $itemQuant = $contents->itemQuant;
        $requestType = $contents->requestType;
        $requestQuant = $contents->requestQuant;
        $response["message"] = add_trade($uuid, $itemType, $itemQuant, $requestType, $requestQuant);
        break;
        
      case "accept_trade":
        $contents = $json->contents;
        $tradeID = $contents->tradeID;
        $uuid = $contents->uuid;
        $response["message"] = accept_trade($tradeID, $uuid);
        break;

      case "delete_trade":
        $contents = $json->contents;
        $tradeID = $contents->tradeID;
        $response["message"] = delete_trade($tradeID);
        break;

      case "add_new_forum":
        $contents = $json->contents;
        $uuid = $contents->uuid;
        $topic = mysqli_real_escape_string($db, $contents->topic);
        $message = mysqli_real_escape_string($db, $contents->message);
        $response["message"] = add_forum_post($uuid, $topic, $message);
        break;

      case "add_new_reply":
        $contents = $json->contents;
        $uuid = $contents->uuid;
        $topicID = mysqli_real_escape_string($db, $contents->topicID);
        $message = mysqli_real_escape_string($db, $contents->message);
        $response["message"] = add_forum_reply($uuid, $topicID, $message);
        break;

      case "get_all_forum_topics":
        $response["message"] = get_all_forum_topics();
        break;

      case "get_all_forum_replies":
        $contents = $json->contents;
        $topicID = $contents->topicID;
        $response["message"] = get_all_forum_replies($topicID);
        break;
  
      default:
        $response["message"] = return_json( "400", "Invalid action." );
        break;
    }
    return $response;
  }

  function requestProcessor($request) {
    echo "received response".PHP_EOL;
    var_dump($request);
    if(!isset($request['type'])) {
      return "ERROR: unsupported message type";
    }

    switch ($request["type"]) {
      case "database_request":
        $json = json_decode($request["message"]);
        $response = doAction($json);
        var_dump($response);
        return $response;

      default:
        return "ERROR: unsupported message type";
    }
  }

  $server = new rabbitMQServer("dbRabbitMQ.ini","testServer");
  
  echo "dbRMQServer BEGIN".PHP_EOL;
  $server->process_requests('requestProcessor');
  echo "dbRMQServer END".PHP_EOL;
  exit();
?>
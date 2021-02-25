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
    global $client;

    $action = $json->action;
    $contents = $json->contents;
    $request = array();
    $request["type"] = "database_response";
  
    switch ($action) {
      case "register_user":
        $username = $contents->username;
        $password = $contents->password;
        $request["message"] = register($username, $password);
        break;
  
      case "login_user":
        $username = $contents->username;
        $password = $contents->password;
        $request["message"] = login($username, $password);
        break;
  
      case "update_user":
        break;
  
      case "get_currency_data":
        $currencyType = $contents->currencyType;
        $request["message"] = get_currency_data($currencyType);
        break;
  
      case "update_currency":
        $currencyType = $contents->currencyType;
        $currentValue = $contents->currentValue;
        $request["message"] = update($currencyType, $currentValue);
        break;
  
      case "get_all_currency_data": 
        $request["message"] = get_all_currency_data();
        break;
  
      case "get_all_player_trades":
        $request["message"] = get_all_player_trades(); 
        break;
  
      case "add_new_trade":
        break;
  
      case "delete_trade":
        $tradeID = $contents->tradeID;
        $request["message"] = delete_trade($tradeID);
        break;
  
      default:
        $request["message"] = return_json( "400", "Invalid action." );
        break;
    }
  
    $response = $client->send_request($request);
  
    echo "client received response: ".PHP_EOL;
    print_r($response);
    echo "\n\n";
    return $response;
  }


  function requestProcessor($request) {
    echo "received request".PHP_EOL;
    var_dump($request);
    if(!isset($request['type'])) {
      return "ERROR: unsupported message type";
    }

    switch ($request["type"]) {
      case "database_request":
        $json = json_decode($request["message"]);
        echo "Received Database request";
        return doAction($json);

      default:
        return array("returnCode"=>0,"message"=>"Server received request and provessed.");
    }
  }

  $server = new rabbitMQServer("dbRabbitMQ.ini","testServer");
  $client = new rabbitMQClient("brokerRabbitMQ.ini","testServer");
  
  echo "dbRMQServer BEGIN".PHP_EOL;
  $server->process_requests('requestProcessor');
  echo "dbRMQServer END".PHP_EOL;
  exit();
?>
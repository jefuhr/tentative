#!/usr/bin/php
<?php

session_start();

require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');

function doLogin($username,$password)
{
    // lookup username in databas
    // check password
    return true;
    //return false if not valid
}

function doDBRequest($request)
{
  $out = $request["message"];
  $msg = shell_exec("./brokerToDBClient.php ".escapeshellarg("$out"));
  $response = array();
  $response['type'] = "database_response";
  $response['message'] = trim($msg);
  return $response;
}

function requestProcessor($request)
{
  echo "received request".PHP_EOL;
  //global $client;
  var_dump($request);
  if(!isset($request['type']))
  {
    return "ERROR: unsupported message type";
  }
  switch ($request['type'])
  {
    case "login":
      return doLogin($request['username'],$request['password']);
    case "database_request":
      $response = doDBRequest($request);
      //$client2 = new rabbitMQClient("dbRabbitMQ.ini","testServer");
      //$response = $client2->send_request($request);
      var_dump($response);
      return $response;
    case "validate_session":
      return doValidate($request['sessionId']);
  }
  return array("returnCode" => '0', 'message'=>"Server received request and processed");
}

$server = new rabbitMQServer("brokerRabbitMQ.ini","testServer");

echo "testRabbitMQServer BEGIN".PHP_EOL;
//$client = new rabbitMQClient("dbRabbitMQ.ini","testServer");

$server->process_requests('requestProcessor');
echo "testRabbitMQServer END".PHP_EOL;
exit();
?>


<?php
  require_once('path.inc');
  require_once('get_host_info.inc');
  require_once('rabbitMQLib.inc');
  
  $client = new rabbitMQClient("dbRabbitMQ.ini","testServer");
  $client2 = new rabbitMQClient("dbRabbitMQ2.ini", "testServer");

  $msg = $_POST['message'];

  if(strpos($msg, 'register_user') !== false || strpos($msg, 'login_user') !== false)
  {
  	$json = json_decode($msg);
  	$contents = $json->contents;
  	$pass = $contents->password;
  	$startpass = ':"';
  	$hash = sha1(strval($pass));
  	$msg = substr_replace($msg, $hash, strpos($msg, $startpass.$pass) + 2, strlen($pass));
  	echo $msg;
  }
  else {echo $msg;}
  
  $request = array();
  $request['type'] = "database_request";
  $request['message'] = $msg;
  $response = $client->send_request($request);
  //$response = $client->publish($request);

  echo "client received response: ".PHP_EOL;
  print_r($response);
  echo "\n\n";

  echo $argv[0]." END".PHP_EOL;
  exit();
?>

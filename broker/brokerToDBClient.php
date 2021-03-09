#!/usr/bin/php
<?php

session_start();

require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');

$request = $_SESSION['request'];
$client = new rabbitMQClient("dbRabbitMQ.ini","testServer");
//$response = $client->send_request($request);
$_SESSION['response'] = $client->send_request($request);
echo '<br /><a href="brokerRabbitMQServer.php">page 1</a>';
exit();
?>

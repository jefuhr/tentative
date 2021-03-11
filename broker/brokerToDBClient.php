#!/usr/bin/php
<?php

require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');

$msg = $argv[1];
$request = array();
$request['type'] = "database_request";
$request['message'] = $msg;

$client = new rabbitMQClient("dbRabbitMQ.ini","testServer");
$response = $client->send_request($request);
echo $response['message'];
exit();

?>

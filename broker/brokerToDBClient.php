#!/usr/bin/php
<?php

require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');

$request = $argv[1];
$client = new rabbitMQClient("dbRabbitMQ.ini","testServer");
$response = $client->send_request($request);
echo "$response";
exit();
?>

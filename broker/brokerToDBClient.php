#!/usr/bin/php
<?php

require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');

function DBRequest($request)
{
  $client = new rabbitMQClient("dbRabbitMQ.ini","testServer");
  $response = $client->send_request($request);
  return $response;
  exit();
}
?>

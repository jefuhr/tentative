#!/usr/bin/php
<?php

require_once('path.inc');
require_once('get_host_info.inc');
require_once('rabbitMQLib.inc');

function display($id){

$base_URL = 'https://api.coinlore.net/api/ticker/?id=';
$URL = $base_URL . $id;

$raw_json = substr(file_get_contents($URL), 1, -1); // substr here shaves off square brackets that the API adds for some reason
$json = json_decode($raw_json);
echo 'Name: ' . $json->name;
echo '<br>Price: $' . $json->price_usd;
echo '<br>Daily Change: ' . $json->percent_change_24h . '%';
echo '<br>Weekly Change: ' . $json->percent_change_7d . '%';

}

function get_currency_data($index){
  global $idx;
  global $ids;
  $sym = $idx[$index];
  $id = $ids[$sym];
  //display($id);
  $base_URL = 'https://api.coinlore.net/api/ticker/?id=';
  $URL = $base_URL . $id;

  $raw_json = substr(file_get_contents($URL), 1, -1);
  $json = json_decode($raw_json);
  //echo $json->name.": ".$json->percent_change_24h."%";
  //echo $index.": ".$json->percent_change_24h."%\n";
  
  $msg = "{
    \"action\" : \"get_currency_data\",
    \"contents\" : {
      \"currencyType\" : \"$index\"
    }
  }";

  $client = new rabbitMQClient("brokerRabbitMQ.ini","testServer");
  $request = array();
  $request['type'] = "database_request";
  $request['message'] = $msg;
  $response = $client->send_request($request);
  /*$response = [
    "type" => "database_response",
    "message" => "{\"status\":\"200\",\"contents\":{\"currencyType\":\"0\",\"currentValue\":\"1\",\"food\":\"100\"}}"
  ];*/ //test code when db not available
  $current = json_decode($response['message']);
  $curr_value = floatval($current->contents->currentValue);
  $new_value = $curr_value * (1 + floatval($json->percent_change_24h)/100);
  //echo $curr_value."->".$new_value;
  
  $msg = "{
    \"action\" : \"update_currency_data\",
    \"contents\" : {
      \"currencyType\" : \"$index\",
      \"currentValue\" : \"$new_value\"
    }
  }";
  
  //echo $msg;
  

}

function get_all_currency_data(){
  global $idx;
  foreach ($idx as $index => $sym){
    get_currency_data($index);
  }
}

$idx = [
  '0' => 'BTC',
  '1' => 'DOGE',
  '2' => 'THC'
];

$ids = [
  'BTC' => '90',
  'DOGE' => '2',
  'THC' => '23'
];

get_all_currency_data();

?>

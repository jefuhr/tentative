#!/usr/bin/php
<?php
  require_once('path.inc');
  require_once('get_host_info.inc');
  require_once('rabbitMQLib.inc');

  $client = new rabbitMQClient("dbRabbitMQ.ini","testServer");

  // $msg = "{
  //   \"action\" : \"login_user\",
  //   \"contents\" : {
  //     \"username\" : \"test\",
  //     \"password\" : \"hashed_password\"
  //   }
  // }";
  
  // $msg = "{
  //   \"action\" : \"get_currency_data\",
  //   \"contents\" : {
  //     \"currencyType\" : 0
  //   }
  // }";
  
  $msg = "{
    \"action\" : \"get_all_currency_data\"
  }";

  // $msg = "{
  //   \"action\" : \"get_all_forum_topics\"
  // }";

  // $msg = "{
  //   \"action\" : \"get_all_forum_replies\",
  //   \"contents\" : {
  //     \"topicID\" : 0
  //   }
  // }";

  $request = array();
  $request['type'] = "database_request";
  $request['message'] = $msg;
  $response = $client->send_request($request);
  // $response = $client->publish($request);
  echo "client received response: ".PHP_EOL;
  print_r($response);
  echo "\n\n";

  echo $argv[0]." END".PHP_EOL;
  exit();
?>

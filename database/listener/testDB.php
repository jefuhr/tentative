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
  
  // $msg = "{
  //   \"action\" : \"get_all_currency_data\"
  // }";

  // $msg = "{
  //   \"action\" : \"get_all_forum_topics\"
  // }";

  // $msg = "{
  //   \"action\" : \"get_all_forum_replies\",
  //   \"contents\" : {
  //     \"topicID\" : 0
  //   }
  // }";

  // $msg = '{
  //   "action": "update_user",
  //   "contents": {
  //       "uuid": 1,
  //       "username": "unityuser",
  //       "food": 13,
  //       "wood": 3,
  //       "stone": 14,
  //       "leather": 9,
  //       "iron": 8,
  //       "gold": 9,
  //       "currency0": 0,
  //       "currency1": 0,
  //       "currency2": 0,
  //       "workerCount": 0,
  //       "tilejson": [
  //           {
  //               "id": 0,
  //               "type": 1,
  //               "level": 1
  //           },
  //           {
  //               "id": 1,
  //               "type": 1,
  //               "level": 1
  //           },
  //           {
  //               "id": 2,
  //               "type": 1,
  //               "level": 1
  //           },
  //           {
  //               "id": 3,
  //               "type": 1,
  //               "level": 1
  //           },
  //           {
  //               "id": 4,
  //               "type": 1,
  //               "level": 1
  //           },
  //           {
  //               "id": 5,
  //               "type": 1,
  //               "level": 1
  //           },
  //           {
  //               "id": 6,
  //               "type": 1,
  //               "level": 1
  //           },
  //           {
  //               "id": 7,
  //               "type": 1,
  //               "level": 1
  //           },
  //           {
  //               "id": 8,
  //               "type": 1,
  //               "level": 1
  //           },
  //           {
  //               "id": 9,
  //               "type": 1,
  //               "level": 1
  //           },
  //           {
  //               "id": 10,
  //               "type": 1,
  //               "level": 1
  //           },
  //           {
  //               "id": 11,
  //               "type": 1,
  //               "level": 1
  //           },
  //           {
  //               "id": 12,
  //               "type": 1,
  //               "level": 1
  //           },
  //           {
  //               "id": 13,
  //               "type": 1,
  //               "level": 1
  //           },
  //           {
  //               "id": 14,
  //               "type": 1,
  //               "level": 1
  //           },
  //           {
  //               "id": 15,
  //               "type": 1,
  //               "level": 1
  //           },
  //           {
  //               "id": 16,
  //               "type": 1,
  //               "level": 1
  //           },
  //           {
  //               "id": 17,
  //               "type": 1,
  //               "level": 1
  //           },
  //           {
  //               "id": 18,
  //               "type": 1,
  //               "level": 1
  //           },
  //           {
  //               "id": 19,
  //               "type": 1,
  //               "level": 1
  //           },
  //           {
  //               "id": 20,
  //               "type": 1,
  //               "level": 1
  //           },
  //           {
  //               "id": 21,
  //               "type": 1,
  //               "level": 1
  //           },
  //           {
  //               "id": 22,
  //               "type": 1,
  //               "level": 1
  //           },
  //           {
  //               "id": 23,
  //               "type": 1,
  //               "level": 1
  //           },
  //           {
  //               "id": 24,
  //               "type": 1,
  //               "level": 1
  //           }
  //       ]
  //   }
  // }';

  $msg = "{
    \"action\": \"get_mission\",
    \"contents\": {
      \"missionID\": 1
    }
  }";

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

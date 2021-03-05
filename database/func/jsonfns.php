<?php
  function return_json( $status, $message ) {
    return json_encode( [ 
      "status" => "$status", 
      "contents" => $message
    ] );
  }

  function convert_user_to_json( $r ) {
    unset($r["password"]); // Do not send password back to client
    return $r;
  }

  function convert_currency_to_json( $r ) {
    unset($r["baseValue"]); // Do not send baseValue (used for debugging) back to client
    return $r;
  }

  function convert_trade_to_json( $r ) { // Placeholder in case array needs to be modified
    return $r;
  }

  function convert_forum_to_json( $r ) { // Placeholder in case array needs to be modified
    return $r;
  }

?>


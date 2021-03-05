<?php


session_start();
include ("config.php");
$json = file_get_contents("login_user.json");
$json_data = json_decode($json, true);
$db= //insert database
$username= $json_data["username"];
$password= $json_data["password"];

function auth ($username, $password, $db) {
	global $query;
	$temp = "select * from playerData where username = '$username' ";

	($query = mysqli_query ($db, $temp)) or die (mysqli_error ($db));
	$hash = $r['pass'];
	if (password_verify($password, $hash)){
		echo "Good Password";
		return true;
	} else { 
		echo "Bad Password";
		return false;
	}
}	
?> 

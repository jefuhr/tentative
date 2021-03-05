<?php


session_start();
include ("config.php");
$json = file_get_contents("register_user.json");
$json_data = json_decode($json, true);
$db= //insert database
$username= $json_data["username"];
$password= $json_data["password"];


function register ($username, $password, $db) {
	global $query;
	$temp = "insert into 'playerData'(username, password) values ($username, $password)"; 
	($query = mysqli_query ($db, $temp)) or die (mysqli_error ($db));
	
?> 

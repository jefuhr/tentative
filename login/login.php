<?php


session_start();
include ("config.php");

$db= ; //insert database
$username=$_GET["username"];
$password=$_GET["password"];

function auth ($username, $password, $db)
	global $query;
	$temp = "select * from playerData where username = '$username' ";



?> 
